# ISSUE-001: Implement DisorderStateManager

## Summary
Complete `DisorderStateManager` per spec to drive the core simulation and state transitions.

## Acceptance Criteria
- Transitions fire correctly at thresholds with hysteresis
- Disorder updates smoothly via escalation curve
- Event lifecycle managed (trigger, update, resolve)
- Perf ≤ 2 ms/frame on baseline
- Unit tests for boundaries and transitions

## Dependencies
None

## Estimate
8–12 hours

## Checklist
- [ ] Methods implemented
- [ ] Tests added
- [ ] Budget warnings in place

