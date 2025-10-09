# PR: Implement DisorderStateManager (ISSUE-001)

## Summary
Implements the core simulation engine for disorder with state transitions, event lifecycle, and budget warnings.

## Scope
- Add logic for UpdateDisorderLevel, ProcessActiveEvents, EvaluateStateTransitions
- Implement TryTriggerEvent, ApplyDelta, SuppressEvent
- Wire events OnStateChanged, OnEventTriggered

## Acceptance Criteria
- Transitions fire with hysteresis
- Smooth disorder updates via escalation curve
- Event lifecycle managed
- Perf ≤ 2 ms/frame on baseline
- Unit tests for boundaries/transitions

## Notes
- Deterministic seeds for tests optional

