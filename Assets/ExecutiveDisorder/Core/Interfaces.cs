namespace ExecutiveDisorder.Core
{
    using System;
    using System.Collections.Generic;

    public struct ConsequenceEdge
    {
        public string targetId;
        public float weight;
        public float delay;
        public float severityScale;
    }

    public interface IEventGraph
    {
        IReadOnlyList<ConsequenceEdge> GetEdges(string eventId);
    }

    public interface IClock
    {
        float DeltaTime { get; }
        float Time { get; }
    }

    public interface IDisorderController
    {
        void ApplyDelta(float amount, string reason);
        bool SuppressEvent(string eventId, float strength);
    }

    public interface IDirectorFeedback
    {
        void Notify(string message, float severity);
    }

    public enum DisorderState
    {
        Idle,
        Building,
        Critical,
        Collapse,
        Recovered
    }

    [Serializable]
    public class DisorderConfig
    {
        public float baseDisorderRate = 1f;
        public float criticalThreshold = 75f;
        public float collapseThreshold = 95f;
        public UnityEngine.AnimationCurve escalationCurve;
    }

    [Serializable]
    public class DisorderEvent
    {
        public string eventId;
        public string eventType;
        public float severity;
        public float duration;
        public List<string> consequences = new List<string>();
        public bool isResolved;
    }

    [Serializable]
    public class ExecutiveAction
    {
        public string actionId;
        public string actionType;
        public float cost;
        public float effectivenessModifier = 1f;
        public List<string> targetedEvents = new List<string>();
        public System.Action onComplete;
    }
}

