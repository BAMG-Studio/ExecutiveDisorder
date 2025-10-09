# Core Systems Spec

## DisorderStateManager
- Responsibilities: update disorder level, manage state, maintain active events, propagate consequences.
- Methods:
  - `void UpdateDisorderLevel(float dt)`
  - `void ProcessActiveEvents(float dt)`
  - `void EvaluateStateTransitions()`
  - `bool TryTriggerEvent(string eventType, float severity, float duration = 5f)`
  - `void ApplyDelta(float amount, string reason)`
  - `bool SuppressEvent(string eventId, float strength)`
  - `float GetNormalizedDisorder()`
- Performance: ≤ 2 ms/frame; no per-frame allocations in loops.
- Acceptance: transitions fire correctly; cascading effects; unit tests for boundaries.

## ExecutiveActionSystem
- Responsibilities: validate/queue actions, rate limit, spend action points, execute effects.
- Methods:
  - `bool TryQueueAction(ExecutiveAction action)`
  - `void RefillTokens(float dt)`
  - `bool TryConsumeToken()`
  - `void AddActionPoints(float amount)`
  - `void Tick(float dt)` (if used) / processed in `Update`
- Performance: ≤ 1 ms per action; execute at most 1 per frame to avoid spikes.
- Security: whitelist action types; cap queue; validate inputs.

## TimeManager
- Responsibilities: pause/resume, time scaling, fixed-step tick (50 Hz), scheduler (future sprint).
- Methods:
  - `void SetTimeScale(float scale)`
  - `void Pause()` / `void Resume()`
  - `event Action<float> OnTick`
  - `float DeltaTime` and `float Time` via `IClock`
- Performance: ≤ 0.5 ms/frame; clamp dt ≤ 100 ms.

## Data Contracts
- `DisorderConfig`: base rate, thresholds, `AnimationCurve` escalation.
- `DisorderEvent`: id, type, severity, duration, consequences, isResolved.
- `ExecutiveAction`: id, type, cost, effectivenessModifier, targetedEvents.

## Integration
- Actions call `IDisorderController` to apply deltas and suppress events.
- Systems communicate via `EventBus` events.

