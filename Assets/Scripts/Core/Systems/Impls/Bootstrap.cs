using System;
using System.Collections.Generic;
using Zenject;

namespace Core.Systems.Impls
{
    public class Bootstrap : ITickable, 
        IInitializable, 
        ILateTickable, 
        IFixedTickable
    {
        private readonly List<ILateSystem> _late = new();
        private readonly List<IFixedSystem> _fixed = new();
        private readonly List<IUpdateSystem> _update = new();
        private readonly List<IInitializeSystem> _initializeSystems = new();
        
        private bool _isInitialized;
        private bool _isPaused;
        
        public Bootstrap(
            [InjectLocal] List<ISystem> systems)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                var system = systems[i];
                
                if(system is IInitializeSystem initializeSystem)
                    _initializeSystems.Add(initializeSystem);
                
                if(system is IFixedSystem fixedUpdate)
                    _fixed.Add(fixedUpdate);
                
                if(system is ILateSystem late)
                    _late.Add(late);
                
                if(system is IUpdateSystem updateSystem)
                    _update.Add(updateSystem);
            }
        }
        
        public void Initialize()
        {
            if(_isInitialized)
                return;
            
            _isInitialized = true;
        }
        
        public void Tick()
        {
            if (_isPaused)
                return;
            
            foreach (var updateSystem in _update)
            {
                updateSystem.Update();
            }
        }

        public void LateTick()
        {
            if (_isPaused)
                return;
            
            foreach (var lateUpdateSystem in _late)
            {
                lateUpdateSystem.Late();
            }
        }

        public void FixedTick()
        {
            if (_isPaused)
                return;
            
            foreach (var fixedUpdate in _fixed)
            {
                fixedUpdate.Fixed();
            }
        }
    }
}