using System;
using System.Collections.Generic;
using Core.Systems;
using Messages;
using PBUdpTransport.Utils;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.PlayerProvider;
using UnityEngine;
using Zenject;

namespace Services.GameState.Impl
{
    public class GameStateProvider : IGameStateProvider, 
        IInitializable, IDisposable
    {
        private readonly INetworkServerManager _networkServerManager;
        private readonly IPlayerProvider _playerProvider;
        private readonly Dictionary<EGameState, HashSet<IGameStateListener>> _gameStateListeners = new();
        private readonly Queue<EGameState> _gameStatesQueue = new();

        public GameStateProvider(
            INetworkServerManager networkServerManager, 
            IPlayerProvider playerProvider
        )
        {
            _networkServerManager = networkServerManager;
            _playerProvider = playerProvider;
        }
        
        public event Action<EGameState> GameStateChanged;
        public EGameState CurrentState { get; private set; }
        
        public void SetState(EGameState gameState)
        {
            foreach (var client in _networkServerManager.Clients)
            {
                _networkServerManager.SendMessage(client.Id, new ServerGameState
                {
                    gameStateId = (int)gameState
                }, ESendMode.Reliable);
            }
       
            
            Debug.Log($"server SetState to queue {gameState}");
            _gameStatesQueue.Enqueue(gameState);
        }

        public void AddGameStateListener(IGameStateListener gameStateListener)
        {
            if(!_gameStateListeners.ContainsKey(gameStateListener.GameState))
                _gameStateListeners.Add(gameStateListener.GameState, new HashSet<IGameStateListener>());

            var listeners = _gameStateListeners[gameStateListener.GameState];
            
            listeners.Add(gameStateListener);
        }

        public void RemoveGameStateListener(IGameStateListener gameStateListener)
        {
            var hasListener = _gameStateListeners.TryGetValue(gameStateListener.GameState, 
                out var listeners);
            
            if(hasListener)
            {
                listeners.Remove(gameStateListener);
            }
        }

        public void Initialize()
        {
            _networkServerManager.ServerStarted += OnServerStart;
        }

        private void OnServerStart()
        {
            _networkServerManager.RegisterMessageHandler<ClientStateReadyMessage>(OnClientStateChanged);
        }

        private void OnClientStateChanged(ClientStateReadyMessage message)
        {
            
            var hasClient = _playerProvider.TryGet(message.clientId, out var player);
            
            if(!hasClient)
                return;

            var state = (EGameState)message.stateId;
            
            Debug.Log($"server OnClientStateChanged state to {state}, client id {message.clientId}");

            player.GameState = state;

            var nextState = _gameStatesQueue.Peek();
            
            var allClientsReady = ClientHasState(nextState);

            if (allClientsReady)
            {
                nextState = _gameStatesQueue.Dequeue();
                Debug.Log($"server switch state to {nextState}");
                SwitchState(nextState);
            }
        }

        private void SwitchState(EGameState state)
        {
            CurrentState = state;
            
            GameStateChanged?.Invoke(state);

            var hasListeners = _gameStateListeners.TryGetValue(state, out var listeners);

            if (hasListeners)
            {
                foreach (var listener in listeners)
                {
                    listener.OnGameStateChanged();
                }
            }
            
            //Debug.Log($"[{nameof(GameStateProvider)}] set game state {state}");
        }

        private bool ClientHasState(EGameState gameState)
        {
            foreach (var player in _playerProvider.Players)
            {
                if (player.GameState != gameState)
                    return false;
            }

            return true;
        }

        public void Dispose()
        {
            _networkServerManager.ServerStarted -= OnServerStart;
        }
    }
}