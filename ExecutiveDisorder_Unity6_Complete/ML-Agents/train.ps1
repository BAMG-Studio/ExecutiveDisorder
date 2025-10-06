# Quick training start script for Executive Disorder ML-Agents (Windows)

Write-Host "Starting ML-Agents Training for Executive Disorder" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""

# Check if virtual environment exists
if (-not (Test-Path "mlagents-env")) {
    Write-Host "❌ Virtual environment not found!" -ForegroundColor Red
    Write-Host "Please run setup-mlagents.ps1 first" -ForegroundColor Yellow
    exit 1
}

# Activate virtual environment
Write-Host "Activating Python environment..." -ForegroundColor Yellow
& .\mlagents-env\Scripts\Activate.ps1

# Default parameters
$CONFIG_FILE = "ml-agents-config.yaml"
$RUN_ID = "ExecutiveDisorder_$(Get-Date -Format 'yyyyMMdd_HHmmss')"

# Parse command line arguments
param(
    [string]$RunId = $RUN_ID,
    [string]$Config = $CONFIG_FILE,
    [switch]$Help
)

if ($Help) {
    Write-Host "Usage: .\train.ps1 [OPTIONS]" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Options:" -ForegroundColor Yellow
    Write-Host "  -RunId <ID>       Set custom run ID (default: ExecutiveDisorder_TIMESTAMP)"
    Write-Host "  -Config <FILE>    Set config file (default: ml-agents-config.yaml)"
    Write-Host "  -Help             Show this help message"
    Write-Host ""
    Write-Host "Examples:" -ForegroundColor Yellow
    Write-Host "  .\train.ps1                              # Start new training"
    Write-Host "  .\train.ps1 -RunId MyTraining_v1         # Start with custom run ID"
    Write-Host "  .\train.ps1 -Config custom-config.yaml   # Use custom config"
    exit 0
}

# Use provided parameters or defaults
if ($PSBoundParameters.ContainsKey('RunId')) {
    $RUN_ID = $RunId
}
if ($PSBoundParameters.ContainsKey('Config')) {
    $CONFIG_FILE = $Config
}

# Check if config file exists
if (-not (Test-Path $CONFIG_FILE)) {
    Write-Host "❌ Config file not found: $CONFIG_FILE" -ForegroundColor Red
    exit 1
}

Write-Host "Configuration:" -ForegroundColor Green
Write-Host "  Config file: $CONFIG_FILE" -ForegroundColor White
Write-Host "  Run ID: $RUN_ID" -ForegroundColor White
Write-Host ""
Write-Host "Starting training..." -ForegroundColor Yellow
Write-Host "When Unity logo appears, press PLAY in Unity Editor" -ForegroundColor Cyan
Write-Host ""
Write-Host "To monitor training progress:" -ForegroundColor Yellow
Write-Host "  Open new terminal and run: tensorboard --logdir results" -ForegroundColor White
Write-Host "  Then visit: http://localhost:6006" -ForegroundColor White
Write-Host ""
Write-Host "To stop training: Press Ctrl+C" -ForegroundColor Yellow
Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""

# Start ML-Agents training
mlagents-learn $CONFIG_FILE --run-id=$RUN_ID --force

Write-Host ""
Write-Host "Training session ended." -ForegroundColor Green
Write-Host "Results saved to: results\$RUN_ID" -ForegroundColor White
