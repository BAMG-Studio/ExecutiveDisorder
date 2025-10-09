# Security Baseline

## Code
- Validate all external inputs; cap sizes
- No secrets in code; load via environment/secure config
- Pin package versions; lock dependencies

## Build
- `.gitignore` excludes secrets/keys/certs
- Pre-merge scan for secrets patterns

## Testing
- Input validation tests; save integrity checks
- Ensure secrets not present in build artifacts

