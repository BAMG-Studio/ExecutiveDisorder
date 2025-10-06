# ML-Agents Python Environment Setup Script
# For Executive Disorder Unity Project (Windows)

Write-Host "=========================================" -ForegroundColor Cyan
Write-Host "ML-Agents Setup for Executive Disorder" -ForegroundColor Cyan
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host ""

# Check Python version
Write-Host "Checking Python version..." -ForegroundColor Yellow
$pythonVersion = python --version 2>&1
Write-Host "Python version: $pythonVersion" -ForegroundColor Green

# Create virtual environment
Write-Host ""
Write-Host "Creating Python virtual environment..." -ForegroundColor Yellow
python -m venv mlagents-env

# Activate virtual environment
Write-Host "Activating virtual environment..." -ForegroundColor Yellow
& .\mlagents-env\Scripts\Activate.ps1

# Upgrade pip
Write-Host ""
Write-Host "Upgrading pip..." -ForegroundColor Yellow
python -m pip install --upgrade pip

# Install ML-Agents
Write-Host ""
Write-Host "Installing ML-Agents..." -ForegroundColor Yellow
pip install mlagents==1.0.0

# Install PyTorch (CPU version)
Write-Host ""
Write-Host "Installing PyTorch..." -ForegroundColor Yellow
pip install torch torchvision torchaudio

# Install additional dependencies
Write-Host ""
Write-Host "Installing additional dependencies..." -ForegroundColor Yellow
pip install tensorboard
pip install numpy
pip install protobuf

# Verify installation
Write-Host ""
Write-Host "Verifying installation..." -ForegroundColor Yellow
mlagents-learn --help

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "=========================================" -ForegroundColor Green
    Write-Host "✅ ML-Agents setup completed successfully!" -ForegroundColor Green
    Write-Host "=========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "To start training:" -ForegroundColor Cyan
    Write-Host "1. Activate the environment: .\mlagents-env\Scripts\Activate.ps1" -ForegroundColor White
    Write-Host "2. Run: mlagents-learn ML-Agents\ml-agents-config.yaml --run-id=ExecutiveDisorder_v1" -ForegroundColor White
    Write-Host "3. Press Play in Unity when prompted" -ForegroundColor White
    Write-Host ""
    Write-Host "To view training progress:" -ForegroundColor Cyan
    Write-Host "  tensorboard --logdir results" -ForegroundColor White
    Write-Host ""
} else {
    Write-Host ""
    Write-Host "❌ Installation verification failed!" -ForegroundColor Red
    Write-Host "Please check the error messages above." -ForegroundColor Red
}

# Save installed packages list
pip freeze > mlagents-requirements.txt
Write-Host "Installed packages saved to: mlagents-requirements.txt" -ForegroundColor Green
