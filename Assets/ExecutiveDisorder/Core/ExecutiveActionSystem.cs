namespace ExecutiveDisorder.Core
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    public class ExecutiveActionSystem : MonoBehaviour
    {
        [Header("Resource Management")]
        [SerializeField] private float _maxActionPoints = 100f;
        [SerializeField] private float _regenPerSecond = 5f;
        private float _currentActionPoints;

        [Header("Action Queue")]
        private readonly Queue<ExecutiveAction> _actionQueue = new();
        private const int MAX_QUEUE_SIZE = 10;

        [Header("Rate Limiter")]
        [SerializeField] private int _tokenCapacity = 10;
        [SerializeField] private float _refillPerSecond = 10f;
        private float _tokens;

        public event Action<ExecutiveAction> OnActionExecuted;
        public event Action<float> OnActionPointsChanged;

        [SerializeField] private MonoBehaviour _disorderControllerSource;
        private IDisorderController _disorder;

        private void Awake()
        {
            _currentActionPoints = _maxActionPoints;
            _tokens = _tokenCapacity;
            _disorder = _disorderControllerSource as IDisorderController;
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            RefillTokens(dt);
            RegenerateActionPoints(dt);
            ProcessQueue();
        }

        public bool TryQueueAction(ExecutiveAction action)
        {
            if (action == null || string.IsNullOrEmpty(action.actionId)) return false;
            if (_actionQueue.Count >= MAX_QUEUE_SIZE) return false;
            if (_currentActionPoints < action.cost) return false;
            if (!TryConsumeToken()) return false;

            _currentActionPoints -= action.cost;
            OnActionPointsChanged?.Invoke(_currentActionPoints / Mathf.Max(1f, _maxActionPoints));
            _actionQueue.Enqueue(action);
            return true;
        }

        private void ProcessQueue()
        {
            if (_actionQueue.Count == 0) return;
            var action = _actionQueue.Dequeue();
            Execute(action);
        }

        private void Execute(ExecutiveAction action)
        {
            _disorder?.ApplyDelta(-Mathf.Abs(action.effectivenessModifier), $"Action:{action.actionId}");
            action.onComplete?.Invoke();
            OnActionExecuted?.Invoke(action);
        }

        private void RefillTokens(float dt)
        {
            _tokens = Mathf.Min(_tokenCapacity, _tokens + _refillPerSecond * dt);
        }

        private bool TryConsumeToken()
        {
            if (_tokens >= 1f) { _tokens -= 1f; return true; }
            return false;
        }

        private void RegenerateActionPoints(float dt)
        {
            if (_currentActionPoints >= _maxActionPoints) return;
            _currentActionPoints = Mathf.Min(_maxActionPoints, _currentActionPoints + _regenPerSecond * dt);
            OnActionPointsChanged?.Invoke(_currentActionPoints / Mathf.Max(1f, _maxActionPoints));
        }
    }
}

