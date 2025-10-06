#!/bin/bash
# Quick training start script for Executive Disorder ML-Agents

echo "Starting ML-Agents Training for Executive Disorder"
echo "=================================================="
echo ""

# Check if virtual environment exists
if [ ! -d "mlagents-env" ]; then
    echo "❌ Virtual environment not found!"
    echo "Please run setup-mlagents.sh first"
    exit 1
fi

# Activate virtual environment
echo "Activating Python environment..."
source mlagents-env/bin/activate

# Default parameters
CONFIG_FILE="ml-agents-config.yaml"
RUN_ID="ExecutiveDisorder_$(date +%Y%m%d_%H%M%S)"
RESUME=false

# Parse command line arguments
while getopts "r:c:h" opt; do
    case $opt in
        r) RUN_ID="$OPTARG";;
        c) CONFIG_FILE="$OPTARG";;
        h)
            echo "Usage: ./train.sh [OPTIONS]"
            echo ""
            echo "Options:"
            echo "  -r RUN_ID       Set custom run ID (default: ExecutiveDisorder_TIMESTAMP)"
            echo "  -c CONFIG       Set config file (default: ml-agents-config.yaml)"
            echo "  -h              Show this help message"
            echo ""
            echo "Examples:"
            echo "  ./train.sh                          # Start new training"
            echo "  ./train.sh -r MyTraining_v1         # Start with custom run ID"
            echo "  ./train.sh -c custom-config.yaml    # Use custom config"
            exit 0
            ;;
        \?)
            echo "Invalid option: -$OPTARG"
            exit 1
            ;;
    esac
done

# Check if config file exists
if [ ! -f "$CONFIG_FILE" ]; then
    echo "❌ Config file not found: $CONFIG_FILE"
    exit 1
fi

echo "Configuration:"
echo "  Config file: $CONFIG_FILE"
echo "  Run ID: $RUN_ID"
echo ""
echo "Starting training..."
echo "When Unity logo appears, press PLAY in Unity Editor"
echo ""
echo "To monitor training progress:"
echo "  Open new terminal and run: tensorboard --logdir results"
echo "  Then visit: http://localhost:6006"
echo ""
echo "To stop training: Press Ctrl+C"
echo ""
echo "=================================================="
echo ""

# Start ML-Agents training
mlagents-learn "$CONFIG_FILE" --run-id="$RUN_ID" --force

echo ""
echo "Training session ended."
echo "Results saved to: results/$RUN_ID"
