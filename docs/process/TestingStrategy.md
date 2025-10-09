# Testing Strategy

## Types
- Unit (EditMode): core logic, boundaries, transitions
- Integration (PlayMode): core loop interactions
- Performance checks: editor proxies for hot paths

## Coverage
- Core systems: ≥ 80% line/branch where practical
- Critical paths must have tests before merge

## Data & Determinism
- Seeded RNG for reproducible simulations
- Avoid time.now; use `IClock`

