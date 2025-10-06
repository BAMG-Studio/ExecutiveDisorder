# Technology Stack

## Programming Languages
- **C# (.NET 9.0)**: Core game logic, Unity scripts
- **Python 3.x**: Backend API service
- **SQL**: Database schema
- **YAML**: Kubernetes and CI/CD configuration
- **HCL**: Terraform infrastructure definitions
- **Shell Script**: Automation scripts

## Game Engine
- **Unity 6.2**: Primary game engine with AI/ML capabilities
  - Universal Render Pipeline (URP)
  - TextMesh Pro for text rendering
  - Input System (new input system)
  - WebGL build support
  - Unity Sentis for neural network inference
  - ML-Agents for reinforcement learning
  - Unity Muse for AI-assisted content generation
  - Addressables for dynamic asset management

## Build Systems
- **MSBuild**: C# project compilation via .NET SDK
- **Unity Build Pipeline**: Game builds for multiple platforms
- **Docker**: Container builds for backend service

## Key Dependencies

### C# Projects
- **.NET 9.0 SDK**: Core framework
- Target frameworks: net9.0
- Nullable reference types enabled
- Implicit usings enabled

### Unity Packages
- Universal RP
- TextMesh Pro
- Input System
- Unity Collaborate Proxy
- Unity Sentis (AI inference engine)
- ML-Agents (reinforcement learning)
- Addressables (dynamic asset loading)
- Unity Muse (AI content generation)

### Backend
- Flask (Python web framework)
- Requirements defined in `backend/app/requirements.txt`

## Development Commands

### C# Projects
```bash
# Build solution
dotnet build ExecutiveDisorder.sln

# Run game application
dotnet run --project ExecutiveDisorder.Game/ExecutiveDisorder.Game.csproj

# Run tests
dotnet test ExecutiveDisorder.Tests/ExecutiveDisorder.Tests.csproj

# Clean build artifacts
dotnet clean
```

### Unity
```bash
# Open in Unity Hub (GUI required)
# Build via Unity Editor: File > Build Settings

# WebGL build considerations:
# - Disable Data Caching in Player Settings
# - Ensure all scenes added to Build Settings
```

### Backend
```bash
# Run Flask server
cd backend/app
python app.py

# Docker build
docker build -t executive-disorder-backend backend/

# Docker Compose
docker-compose -f infrastructure/docker-compose.yml up
```

### Infrastructure
```bash
# Bootstrap environment
bash scripts/bootstrap.sh

# Sync repositories
bash scripts/sync-repos.sh

# Terraform (from terraform/ directory)
terraform init
terraform plan
terraform apply
```

## Configuration Files
- `.editorconfig`: Code style enforcement
- `.vsconfig`: Visual Studio workload configuration
- `.gitignore`: Git exclusions for Unity and .NET
- `ExecutiveDisorder.sln`: Visual Studio solution
- `unity/Packages/manifest.json`: Unity package dependencies
