namespace ExecutiveDisorder.Core
{
    using UnityEngine;
    using System;

    public class TimeManager : MonoBehaviour, IClock
    {
        public float TimeScale { get; private set; } = 1f;
        public bool IsPaused { get; private set; }
        public event Action<float> OnTick;

        [SerializeField] private float _fixedStep = 0.02f; // 50 Hz
        private float _accumulator;
        private float _time;

        public void SetTimeScale(float scale)
        {
            TimeScale = Mathf.Clamp(scale, 0f, 4f);
        }

        public void Pause() => IsPaused = true;
        public void Resume() => IsPaused = false;

        private void Update()
        {
            var dt = IsPaused ? 0f : Mathf.Min(UnityEngine.Time.deltaTime * TimeScale, 0.1f);
            _accumulator += dt;
            while (_accumulator >= _fixedStep)
            {
                _accumulator -= _fixedStep;
                _time += _fixedStep;
                OnTick?.Invoke(_fixedStep);
            }
        }

        float IClock.DeltaTime => IsPaused ? 0f : Mathf.Min(UnityEngine.Time.deltaTime * TimeScale, 0.1f);
        float IClock.Time => _time;
    }
}

