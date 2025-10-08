#!/bin/bash
# Terraform Setup and Apply Script
set -e

echo "🏗️  Executive Disorder - Infrastructure Setup"
echo "=============================================="

# Check if terraform is installed
if ! command -v terraform &> /dev/null; then
    echo "❌ Terraform is not installed"
    echo "Install from: https://www.terraform.io/downloads"
    exit 1
fi

echo "✅ Terraform $(terraform version | head -n1)"

# Change to terraform directory
cd "$(dirname "$0")/../terraform"

# Initialize Terraform
echo ""
echo "📦 Initializing Terraform..."
terraform init

# Validate configuration
echo ""
echo "🔍 Validating Terraform configuration..."
terraform validate

# Check if terraform.tfvars exists
if [ ! -f "terraform.tfvars" ]; then
    echo ""
    echo "⚠️  terraform.tfvars not found!"
    echo "Creating from example..."
    cp terraform.tfvars.example terraform.tfvars
    echo ""
    echo "📝 Please edit terraform.tfvars with your actual values:"
    echo "   - github_token: Your GitHub Personal Access Token"
    echo "   - github_owner: BAMG-Studio"
    echo "   - unity_license, unity_email, unity_password (optional)"
    echo "   - openai_api_key (optional)"
    echo ""
    read -p "Press Enter when ready to continue..."
fi

# Plan
echo ""
echo "📋 Planning infrastructure changes..."
terraform plan -out=tfplan

# Ask for confirmation
echo ""
read -p "🚀 Apply these changes? (yes/no): " confirm

if [ "$confirm" = "yes" ]; then
    echo ""
    echo "🚀 Applying infrastructure changes..."
    terraform apply tfplan
    
    echo ""
    echo "✅ Infrastructure setup complete!"
    echo ""
    echo "📊 Outputs:"
    terraform output
    
    echo ""
    echo "🎯 Next Steps:"
    echo "1. Go to your repository settings"
    echo "2. Enable GitHub Pages (should be auto-configured)"
    echo "3. Add required secrets if not done via Terraform:"
    echo "   - UNITY_LICENSE"
    echo "   - UNITY_EMAIL"
    echo "   - UNITY_PASSWORD"
    echo "   - OPENAI_API_KEY"
    echo "4. Push code to trigger the first build!"
else
    echo "❌ Cancelled. Run this script again when ready."
    rm tfplan
    exit 1
fi
