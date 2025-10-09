# Architecture

## Overview
- Core loop: Time -> Disorder Simulation -> Events -> Player Actions -> Effects -> State transitions.
- Patterns: State (disorder), Command (actions), Observer (EventBus), Object Pooling (events), Fixed-step ticking.
- Data: ScriptableObjects for configs; serializable DTOs for runtime state. No remote configs in Sprint 1.

## Components
- `DisorderStateManager` (simulation, transitions, event lifecycle)
- `ExecutiveActionSystem` (queueing, rate limiting, AP economy, effects)
- `TimeManager` (pause/scale, fixed-step ticks, scheduler)
- `EventBus` (lightweight, generic pub/sub)
- Interfaces: `IClock`, `IDisorderController`, `IDirectorFeedback`, `IEventGraph`

## Boundaries
- Actions affect simulation only via `IDisorderController`.
- UI talks to systems via events and serialized references (no global statics).
- Platform services (analytics, storage) are out-of-scope for Sprint 1.

## Budgets
- WebGL total ≤ 5 ms/frame; simulation ≤ 2 ms; actions ≤ 1 ms; time ≤ 0.5 ms.
- Memory baseline ≤ 512 MB; avoid per-frame allocations in hot paths.

## Testing
- EditMode unit tests for logic; PlayMode integration for core loop.
- Performance assertions on hot paths (editor proxies).

## Security
- Input validation on all external/player inputs.
- No secrets in source; `.gitignore` excludes sensitive files.

