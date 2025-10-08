#!/bin/bash
# Quick Start - Deploy Executive Disorder to GitHub Pages
set -e

echo "🎮 Executive Disorder - GitHub Pages Deployment"
echo "================================================"
echo ""

# Function to check if command exists
command_exists() {
    command -v "$1" &> /dev/null
}

# Check prerequisites
echo "📋 Checking prerequisites..."
echo ""

if ! command_exists terraform; then
    echo "❌ Terraform not found!"
    echo "Install: https://www.terraform.io/downloads"
    echo ""
    echo "Quick install (Ubuntu/WSL):"
    echo "  wget -O- https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg"
    echo '  echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list'
    echo "  sudo apt update && sudo apt install terraform"
    exit 1
fi

if ! command_exists gh; then
    echo "⚠️  GitHub CLI not found (optional but recommended)"
    echo "Install: https://cli.github.com/"
else
    echo "✅ GitHub CLI installed"
fi

if ! command_exists git; then
    echo "❌ Git not found!"
    exit 1
fi

echo "✅ Terraform installed: $(terraform version | head -n1)"
echo "✅ Git installed: $(git --version)"
echo ""

# Ask for GitHub token
if [ -z "$GITHUB_TOKEN" ]; then
    echo "🔑 GitHub Personal Access Token Required"
    echo ""
    echo "Generate one at: https://github.com/settings/tokens"
    echo "Required scopes: repo, admin:repo_hook, workflow"
    echo ""
    read -sp "Enter your GitHub token (ghp_...): " GITHUB_TOKEN
    echo ""
    export GITHUB_TOKEN
fi

# Create terraform.tfvars
echo ""
echo "📝 Creating Terraform configuration..."

cat > terraform/terraform.tfvars << EOF
github_token = "$GITHUB_TOKEN"
github_owner = "BAMG-Studio"

# Unity credentials (optional - can be set later via GitHub UI)
# unity_license  = ""
# unity_email    = ""
# unity_password = ""

# OpenAI API key (optional - for Codex CLI automation)
# openai_api_key = ""
EOF

echo "✅ terraform.tfvars created"
echo ""

# Run Terraform
echo "🚀 Setting up GitHub infrastructure..."
echo ""

cd terraform

terraform init
terraform validate

echo ""
echo "📋 Terraform will create:"
echo "  - GitHub Pages configuration"
echo "  - Branch protection rules"
echo "  - Repository settings"
echo "  - Action secrets (if provided)"
echo ""

read -p "Continue? (yes/no): " confirm

if [ "$confirm" != "yes" ]; then
    echo "❌ Cancelled"
    exit 1
fi

terraform apply -auto-approve

echo ""
echo "✅ Infrastructure created!"
echo ""

cd ..

# Commit and push
echo "📤 Committing infrastructure code..."
git add .github/ terraform/ scripts/ docs/INFRASTRUCTURE_SETUP.md .gitignore .yamllint
git commit -m "🏗️ Add Terraform infrastructure and GitHub Actions workflows

- GitHub Pages auto-deployment
- Unity WebGL build automation
- PR validation workflows
- Codex CLI content generation
- Infrastructure as Code with Terraform" || echo "Nothing to commit"

echo ""
read -p "Push to GitHub now? (yes/no): " push_confirm

if [ "$push_confirm" = "yes" ]; then
    git push origin main
    echo ""
    echo "✅ Pushed to GitHub!"
    echo ""
    echo "🎯 Next Steps:"
    echo ""
    echo "1. Watch the build: https://github.com/BAMG-Studio/ExecutiveDisorder/actions"
    if command_exists gh; then
        echo "   Or run: gh run watch"
    fi
    echo ""
    echo "2. Your game will be deployed to:"
    terraform -chdir=terraform output -raw pages_url || echo "   https://bamg-studio.github.io/ExecutiveDisorder/"
    echo ""
    echo "3. Add Unity secrets (if not done):"
    echo "   gh secret set UNITY_LICENSE < unity-license.ulf"
    echo "   gh secret set UNITY_EMAIL --body 'your@email.com'"
    echo "   gh secret set UNITY_PASSWORD --body 'yourpassword'"
    echo ""
    echo "4. Enable GitHub Pages:"
    echo "   Settings → Pages → Source: gh-pages branch"
    echo ""
else
    echo ""
    echo "⏸️  Changes committed but not pushed"
    echo "Push manually with: git push origin main"
fi

echo ""
echo "🎉 Setup complete! Check docs/INFRASTRUCTURE_SETUP.md for details"
