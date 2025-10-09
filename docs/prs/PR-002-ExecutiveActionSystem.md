# PR: Implement ExecutiveActionSystem (ISSUE-002)

## Summary
Implements action validation, FIFO queue with cap, token bucket rate limiting (10/sec), AP economy, and execution via `IDisorderController`.

## Scope
- `TryQueueAction`, token bucket limiter, AP regeneration
- Execute at most one action per frame; emit events
- Input validation and caps on queue/targets

## Acceptance Criteria
- FIFO queue; rate limiting works
- Costs enforced; events emitted
- Perf ≤ 1 ms/action
- Unit tests for queue/limiter/validation

