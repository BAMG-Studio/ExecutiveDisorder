namespace ExecutiveDisorder.Core
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    public class DisorderStateManager : MonoBehaviour, IDisorderController
    {
        [Header("Disorder Configuration")]
        [SerializeField] private DisorderConfig _config;

        [Header("State Tracking")]
        [SerializeField] private DisorderState _currentState = DisorderState.Idle;
        [SerializeField] [Range(0f,100f)] private float _disorderLevel;
        private readonly Dictionary<string, DisorderEvent> _activeEvents = new();

        public event Action<DisorderState> OnStateChanged;
        public event Action<DisorderEvent> OnEventTriggered;

        private const float MAX_FRAME_TIME_MS = 2f;
        private float _lastBudgetWarning;

        private void Update()
        {
            var start = Time.realtimeSinceStartup;

            UpdateDisorderLevel(Time.deltaTime);
            ProcessActiveEvents(Time.deltaTime);
            EvaluateStateTransitions();

            var ms = (Time.realtimeSinceStartup - start) * 1000f;
            if (ms > MAX_FRAME_TIME_MS && Time.unscaledTime - _lastBudgetWarning > 1f)
            {
                _lastBudgetWarning = Time.unscaledTime;
                Debug.LogWarning($"DisorderStateManager exceeded budget: {ms:F2}ms");
            }
        }

        public float GetNormalizedDisorder() => Mathf.Clamp01(_disorderLevel / Mathf.Max(1f, _config?.collapseThreshold ?? 100f));

        public void ApplyDelta(float amount, string reason)
        {
            var prev = _disorderLevel;
            _disorderLevel = Mathf.Clamp(_disorderLevel + amount, 0f, 100f);
            if (Mathf.Abs(_disorderLevel - prev) > 0.001f)
            {
                // could publish change via an event bus if provided
            }
        }

        public bool SuppressEvent(string eventId, float strength)
        {
            if (string.IsNullOrEmpty(eventId)) return false;
            if (_activeEvents.TryGetValue(eventId, out var e))
            {
                e.severity = Mathf.Max(0f, e.severity - Mathf.Abs(strength));
                if (e.severity <= 0.01f) e.isResolved = true;
                _activeEvents[eventId] = e;
                return true;
            }
            return false;
        }

        private void UpdateDisorderLevel(float dt)
        {
            if (_config == null) return;
            float curve = _config.escalationCurve != null ? _config.escalationCurve.Evaluate(GetNormalizedDisorder()) : 1f;
            _disorderLevel = Mathf.Clamp(_disorderLevel + (_config.baseDisorderRate * curve * dt), 0f, 100f);
        }

        private void ProcessActiveEvents(float dt)
        {
            if (_activeEvents.Count == 0) return;
            var keys = new List<string>(_activeEvents.Keys);
            for (int i=0;i<keys.Count;i++)
            {
                var id = keys[i];
                var ev = _activeEvents[id];
                if (ev.isResolved) { _activeEvents.Remove(id); continue; }
                ev.duration -= dt;
                if (ev.duration <= 0f) ev.isResolved = true;
                _activeEvents[id] = ev;
            }
        }

        private void EvaluateStateTransitions()
        {
            var prev = _currentState;
            var lvl = _disorderLevel;
            var c = _config ?? new DisorderConfig();

            switch (_currentState)
            {
                case DisorderState.Idle:
                    if (lvl > 0.1f) _currentState = DisorderState.Building; break;
                case DisorderState.Building:
                    if (lvl >= c.criticalThreshold) _currentState = DisorderState.Critical;
                    else if (lvl <= 0.05f) _currentState = DisorderState.Idle;
                    break;
                case DisorderState.Critical:
                    if (lvl >= c.collapseThreshold) _currentState = DisorderState.Collapse;
                    else if (lvl < c.criticalThreshold * 0.8f) _currentState = DisorderState.Building;
                    break;
                case DisorderState.Collapse:
                    if (lvl < c.criticalThreshold * 0.3f) _currentState = DisorderState.Recovered;
                    break;
                case DisorderState.Recovered:
                    if (lvl <= 0.05f) _currentState = DisorderState.Idle;
                    else if (lvl > c.criticalThreshold * 0.5f) _currentState = DisorderState.Building;
                    break;
            }

            if (prev != _currentState) OnStateChanged?.Invoke(_currentState);
        }

        public bool TryTriggerEvent(string eventType, float severity, float duration = 5f)
        {
            if (string.IsNullOrEmpty(eventType) || severity <= 0f) return false;
            var id = Guid.NewGuid().ToString("N");
            var e = new DisorderEvent { eventId = id, eventType = eventType, severity = severity, duration = duration, isResolved = false };
            _activeEvents[id] = e;
            OnEventTriggered?.Invoke(e);
            return true;
        }
    }
}

