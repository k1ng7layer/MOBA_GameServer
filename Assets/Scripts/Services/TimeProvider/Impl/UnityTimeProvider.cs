using Core.Systems;
using UnityEngine;

namespace Services.TimeProvider.Impl
{
    public class UnityTimeProvider : ITimeProvider
    {
        private float _fixedDeltaTime;

        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
    }
}