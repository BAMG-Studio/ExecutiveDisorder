#!/bin/bash
# ML-Agents Python Environment Setup Script
# For Executive Disorder Unity Project

echo "========================================="
echo "ML-Agents Setup for Executive Disorder"
echo "========================================="
echo ""

# Check Python version
echo "Checking Python version..."
python_version=$(python3 --version 2>&1 | awk '{print $2}')
echo "Python version: $python_version"

# Create virtual environment
echo ""
echo "Creating Python virtual environment..."
python3 -m venv mlagents-env

# Activate virtual environment
echo "Activating virtual environment..."
source mlagents-env/bin/activate

# Upgrade pip
echo ""
echo "Upgrading pip..."
pip install --upgrade pip

# Install ML-Agents
echo ""
echo "Installing ML-Agents..."
pip install mlagents==1.0.0

# Install PyTorch (CPU version)
echo ""
echo "Installing PyTorch..."
pip install torch torchvision torchaudio --index-url https://download.pytorch.org/whl/cpu

# Install additional dependencies
echo ""
echo "Installing additional dependencies..."
pip install tensorboard
pip install numpy
pip install protobuf

# Verify installation
echo ""
echo "Verifying installation..."
mlagents-learn --help

if [ $? -eq 0 ]; then
    echo ""
    echo "========================================="
    echo "✅ ML-Agents setup completed successfully!"
    echo "========================================="
    echo ""
    echo "To start training:"
    echo "1. Activate the environment: source mlagents-env/bin/activate"
    echo "2. Run: mlagents-learn ML-Agents/ml-agents-config.yaml --run-id=ExecutiveDisorder_v1"
    echo "3. Press Play in Unity when prompted"
    echo ""
    echo "To view training progress:"
    echo "  tensorboard --logdir results"
    echo ""
else
    echo ""
    echo "❌ Installation verification failed!"
    echo "Please check the error messages above."
fi

# Save installed packages list
pip freeze > mlagents-requirements.txt
echo "Installed packages saved to: mlagents-requirements.txt"
