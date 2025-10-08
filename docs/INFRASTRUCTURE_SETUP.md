# Infrastructure Setup with Terraform & GitHub Actions
## Executive Disorder Deployment Guide

---

## 🎯 Overview

This setup provides:
- ✅ **Automated Unity WebGL builds** on every push
- ✅ **Auto-deployment to GitHub Pages**
- ✅ **Code quality validation** on PRs
- ✅ **Scheduled content generation** via Codex CLI
- ✅ **Infrastructure as Code** with Terraform

---

## 📋 Prerequisites

### 1. Install Terraform
```bash
# Ubuntu/Debian
wget -O- https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list
sudo apt update && sudo apt install terraform

# macOS
brew tap hashicorp/tap
brew install hashicorp/tap/terraform

# Windows (WSL)
curl -fsSL https://apt.releases.hashicorp.com/gpg | sudo apt-key add -
sudo apt-add-repository "deb [arch=amd64] https://apt.releases.hashicorp.com $(lsb_release -cs) main"
sudo apt-get update && sudo apt-get install terraform
```

### 2. Get GitHub Personal Access Token
1. Go to: https://github.com/settings/tokens
2. Click "Generate new token (classic)"
3. Select scopes:
   - ✅ `repo` (Full control of private repositories)
   - ✅ `admin:repo_hook` (Full control of repository hooks)
   - ✅ `workflow` (Update GitHub Action workflows)
4. Copy the token (starts with `ghp_`)

### 3. Get Unity Credentials (Optional)
- Unity Email
- Unity Password  
- Unity License (base64 encoded)

To get Unity license:
```bash
# Run Unity in batch mode to generate license
/path/to/Unity -batchmode -quit -logFile
cat ~/.local/share/unity3d/Unity/Unity_lic.ulf | base64 > unity-license-base64.txt
```

---

## 🚀 Quick Start

### Step 1: Configure Terraform Variables

```bash
# Copy example file
cp terraform/terraform.tfvars.example terraform/terraform.tfvars

# Edit with your values
nano terraform/terraform.tfvars
```

**Required values:**
```hcl
github_token = "ghp_YOUR_GITHUB_TOKEN_HERE"
github_owner = "BAMG-Studio"

# Optional (can be set via GitHub UI later)
# unity_license  = "base64_encoded_license"
# unity_email    = "your@email.com"
# unity_password = "your_password"
# openai_api_key = "sk-your-key"
```

### Step 2: Run Infrastructure Setup

```bash
# Make script executable
chmod +x scripts/setup-infrastructure.sh

# Run setup
bash scripts/setup-infrastructure.sh
```

This will:
1. Initialize Terraform
2. Validate configuration
3. Show planned changes
4. Apply infrastructure (after confirmation)

### Step 3: Configure GitHub Secrets (Manual Method)

If you didn't set secrets via Terraform, add them manually:

```bash
# Using GitHub CLI
gh secret set UNITY_LICENSE < unity-license-base64.txt
gh secret set UNITY_EMAIL --body "your@email.com"
gh secret set UNITY_PASSWORD --body "your_password"
gh secret set OPENAI_API_KEY --body "sk-your-key"
```

Or via GitHub UI:
1. Go to: `Settings → Secrets and variables → Actions`
2. Click "New repository secret"
3. Add each secret

---

## 📦 What Gets Created

### GitHub Repository Configuration
- ✅ GitHub Pages enabled (gh-pages branch)
- ✅ Branch protection on `main` (requires PR reviews)
- ✅ Branch protection on `codex-cli` (requires status checks)
- ✅ Repository topics/tags for discoverability
- ✅ Security scanning enabled

### GitHub Actions Secrets
- `UNITY_LICENSE` - Unity license file
- `UNITY_EMAIL` - Unity account email
- `UNITY_PASSWORD` - Unity account password
- `OPENAI_API_KEY` - OpenAI API key for Codex

### GitHub Actions Variables
- `UNITY_VERSION` - Unity version (6000.2.6f2)
- `BUILD_TARGET` - Build target platform (WebGL)

---

## 🔄 Workflows Explained

### 1. **deploy-pages.yml** - Main Deployment Pipeline

**Triggers:**
- Push to `main` or `codex-cli`
- Pull requests to `main`
- Manual dispatch

**Jobs:**
1. **build-webgl** - Build Unity WebGL project
2. **validate-code** - Lint Python, validate YAML, check duplicates
3. **deploy-pages** - Deploy to GitHub Pages (main branch only)

**Deployment URL:** `https://bamg-studio.github.io/ExecutiveDisorder/`

---

### 2. **pr-validation.yml** - Pull Request Quality Checks

**Triggers:**
- Pull request opened/updated to `main`

**Checks:**
- Python code formatting (Black)
- Python linting (Pylint)
- YAML validation
- Duplicate C# class detection
- Large file detection
- PR size analysis

---

### 3. **codex-automation.yml** - Content Generation

**Triggers:**
- Manual dispatch (with theme selection)
- Schedule: Every Monday at 2 AM UTC

**Process:**
1. Install Codex CLI and dependencies
2. Generate content (cards, images, audio)
3. Import to Unity (if possible)
4. Commit changes
5. Trigger build pipeline

**Manual Trigger:**
```bash
gh workflow run codex-automation.yml -f theme=cyberpunk
```

---

## 🧪 Testing Locally

### Test GitHub Actions Locally (with `act`)

```bash
# Install act
curl https://raw.githubusercontent.com/nektos/act/master/install.sh | sudo bash

# Test build workflow
act -W .github/workflows/deploy-pages.yml -j build-webgl

# Test PR validation
act pull_request -W .github/workflows/pr-validation.yml
```

### Test Terraform Changes

```bash
cd terraform

# Plan changes
terraform plan

# Show current state
terraform show

# List resources
terraform state list
```

---

## 🔧 Maintenance

### Update Infrastructure

```bash
cd terraform

# Make changes to .tf files
# Then apply:
terraform plan
terraform apply
```

### Update Secrets

```bash
# Via GitHub CLI
gh secret set SECRET_NAME --body "new_value"

# Via Terraform (update terraform.tfvars)
terraform apply
```

### Destroy Infrastructure (WARNING!)

```bash
cd terraform
terraform destroy
```

---

## 📊 Monitoring & Debugging

### Check Workflow Status

```bash
# List recent workflow runs
gh run list

# View specific run
gh run view RUN_ID

# View logs
gh run view RUN_ID --log
```

### GitHub Pages Status

Check at: `https://github.com/BAMG-Studio/ExecutiveDisorder/settings/pages`

### Common Issues

**Build fails with Unity license error:**
- Verify UNITY_LICENSE secret is base64 encoded
- Check Unity credentials are correct
- Ensure Unity version matches (6000.2.6f2)

**GitHub Pages 404:**
- Ensure gh-pages branch exists
- Check Pages is enabled in repository settings
- Wait 5-10 minutes for first deployment

**Terraform apply fails:**
- Verify GitHub token has correct permissions
- Check token hasn't expired
- Ensure repository name matches

---

## 🎯 Next Steps

1. ✅ **Commit infrastructure code:**
   ```bash
   git add .github/ terraform/ scripts/setup-infrastructure.sh
   git commit -m "🏗️ Add Terraform infrastructure and GitHub Actions workflows"
   git push origin main
   ```

2. ✅ **Watch the first build:**
   ```bash
   gh run watch
   ```

3. ✅ **Access your deployed game:**
   - https://bamg-studio.github.io/ExecutiveDisorder/

4. ✅ **Setup custom domain (optional):**
   - Add CNAME record: `play.bamg-studio.com` → `bamg-studio.github.io`
   - Update terraform.tfvars: `custom_domain = "play.bamg-studio.com"`
   - Run: `terraform apply`

---

## 🔐 Security Best Practices

1. **Never commit terraform.tfvars** - It's in .gitignore
2. **Rotate tokens regularly** - Update every 90 days
3. **Use least privilege** - Only grant required permissions
4. **Enable 2FA** - On GitHub account
5. **Review workflow runs** - Check for suspicious activity

---

## 📚 Resources

- [Terraform GitHub Provider](https://registry.terraform.io/providers/integrations/github/latest/docs)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Unity Game CI](https://game.ci/)
- [GitHub Pages Documentation](https://docs.github.com/en/pages)

---

**Questions? Issues?**
- Open a GitHub issue
- Check workflow logs: `gh run list`
- Terraform docs: `terraform --help`

**Happy Deploying! 🚀**
