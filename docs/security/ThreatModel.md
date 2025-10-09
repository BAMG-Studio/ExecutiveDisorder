# Threat Model

## Assets
- Player data; game integrity; platform credentials; IP

## Actors
- Malicious players; web attackers; supply chain; accidental exposure

## Vectors & Mitigations
- Client save tampering → server validation/encryption (future); hash checks
- Dependency vulns → SBOM, scans, pinned versions
- Secrets in build → secrets mgmt, build-time injection
- XSS (if user input) → sanitize, CSP

