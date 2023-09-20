using Messages;
using PBUnityMultiplayer.Runtime.Core.Client;
using PBUnityMultiplayer.Runtime.Core.Server;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Views.Character;
using Views.Character.Impl;
using Zenject;
using Character = Models.Character;

namespace Presenters
{
    public class CharacterPresenter : IInitializable
    {
        private readonly ICharacterView _characterView;
        private readonly Character _character;
        private readonly INetworkServerManager _networkServerManager;

        public CharacterPresenter(
            INetworkServerManager networkServerManager, 
            ICharacterView characterView, 
            Character character)
        {
            _networkServerManager = networkServerManager;
            _characterView = characterView;
            _character = character;
        }


        public void Initialize()
        {
            _networkServerManager.RegisterMessageHandler<PlayerDestinationMessage>(OnPlayerDestinationRequestReceived);
        }
        
        private void OnPlayerDestinationRequestReceived(PlayerDestinationMessage message)
        {
            var destination = new Vector3(message.X, message.Y, message.Z);
            _character.SetDestination(destination);
        }
    }
}