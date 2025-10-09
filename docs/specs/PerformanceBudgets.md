# Performance Budgets

## Hardware Baseline (WebGL)
- CPU: 2 cores @ 2.5 GHz
- RAM: 8 GB
- Browser: Chrome 120+

## Frame Budgets
- Total: ≤ 5 ms/frame
- Disorder: ≤ 2 ms
- Actions: ≤ 1 ms
- Time: ≤ 0.5 ms

## Asset Budgets
- Download size: ≤ 25 MB
- First load: ≤ 10 s on baseline
- Textures: ETC2/ASTC, compressed
- Audio: Vorbis ~128 kbps

## Instrumentation
- Budget warnings in hot paths
- Editor profiling recipes for WebGL parity

