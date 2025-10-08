# 🚀 Deployment Setup Complete!

## Infrastructure Overview

Your Executive Disorder project now has a complete CI/CD pipeline with:

### ✅ **Terraform Infrastructure**
```
terraform/
├── main.tf                      # GitHub repository configuration
├── variables.tf                 # Input variables
├── backend.tf                   # State backend
├── terraform.tfvars.example     # Example configuration
└── README.md                    # Terraform docs
```

**Manages:**
- GitHub Pages configuration
- Branch protection rules
- Repository secrets & variables
- Security settings

### ✅ **GitHub Actions Workflows**
```
.github/workflows/
├── deploy-pages.yml             # Main deployment pipeline
├── pr-validation.yml            # PR quality checks
└── codex-automation.yml         # Scheduled content generation
```

**Features:**
- Auto-build Unity WebGL on every push
- Deploy to GitHub Pages on merge to main
- Validate code quality on PRs
- Weekly content generation via Codex CLI

---

## 🎯 The Deployment Flow

```
┌─────────────────────────────────────────────────────────────┐
│  Developer Workflow                                          │
└─────────────────────────────────────────────────────────────┘
              ↓
    git push origin codex-cli
              ↓
┌─────────────────────────────────────────────────────────────┐
│  GitHub Actions: Build & Validate                           │
│  • Build Unity WebGL                                        │
│  • Run code quality checks                                  │
│  • Upload build artifacts                                   │
└─────────────────────────────────────────────────────────────┘
              ↓
    Create Pull Request → main
              ↓
┌─────────────────────────────────────────────────────────────┐
│  PR Validation Workflow                                     │
│  • Python formatting check                                  │
│  • Lint Python & validate YAML                             │
│  • Check for duplicate classes                             │
│  • File size analysis                                       │
└─────────────────────────────────────────────────────────────┘
              ↓
    Merge to main (if checks pass)
              ↓
┌─────────────────────────────────────────────────────────────┐
│  Deploy to GitHub Pages                                     │
│  • Build WebGL (with caching)                              │
│  • Deploy to gh-pages branch                               │
│  • Live at: bamg-studio.github.io/ExecutiveDisorder        │
└─────────────────────────────────────────────────────────────┘
```

---

## 📋 Setup Checklist

### 1. **Install Terraform** (if not already)
```bash
# Ubuntu/WSL
wget -O- https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list
sudo apt update && sudo apt install terraform
```

### 2. **Get GitHub Personal Access Token**
- Go to: https://github.com/settings/tokens
- Generate new token (classic)
- Scopes needed: `repo`, `admin:repo_hook`, `workflow`
- Save token securely (starts with `ghp_`)

### 3. **Configure Terraform**
```bash
cd terraform
cp terraform.tfvars.example terraform.tfvars
# Edit terraform.tfvars with your GitHub token
```

### 4. **Run Quick Deploy Script**
```bash
chmod +x scripts/quick-deploy.sh
bash scripts/quick-deploy.sh
```

Or manually:
```bash
# Initialize Terraform
cd terraform
terraform init
terraform plan
terraform apply

# Commit and push
cd ..
git add .github/ terraform/ scripts/ docs/ .gitignore .yamllint
git commit -m "🏗️ Add infrastructure and CI/CD pipeline"
git push origin main
```

### 5. **Add Unity Secrets** (Optional)
```bash
# Via GitHub CLI
gh secret set UNITY_LICENSE < unity-license.ulf
gh secret set UNITY_EMAIL --body "your@email.com"
gh secret set UNITY_PASSWORD --body "yourpassword"
gh secret set OPENAI_API_KEY --body "sk-your-key"
```

Or via GitHub UI: `Settings → Secrets and variables → Actions`

### 6. **Enable GitHub Pages**
- Go to: `Settings → Pages`
- Source: `Deploy from a branch`
- Branch: `gh-pages` / `/ (root)`
- Save

### 7. **Watch the First Build**
```bash
gh run watch
```

Or visit: https://github.com/BAMG-Studio/ExecutiveDisorder/actions

---

## 🎮 Access Your Game

After successful deployment:

### **Main Deployment**
- URL: https://bamg-studio.github.io/ExecutiveDisorder/
- Updates: On every merge to `main`
- Build time: ~10-15 minutes

### **Preview Builds**
- Artifacts available for every push
- Download from: Actions → Workflow run → Artifacts
- Retention: 14 days

---

## 🔧 Customization Options

### **Change Unity Version**
Edit `.github/workflows/deploy-pages.yml`:
```yaml
unityVersion: '6000.2.6f2'  # Change to your version
```

### **Change Build Target**
```yaml
targetPlatform: WebGL  # Or StandaloneWindows64, etc.
```

### **Add Custom Domain**
Update `terraform/terraform.tfvars`:
```hcl
custom_domain = "play.your-domain.com"
```
Then: `terraform apply`

### **Adjust Codex Schedule**
Edit `.github/workflows/codex-automation.yml`:
```yaml
schedule:
  - cron: '0 2 * * 1'  # Every Monday at 2 AM
```

---

## 📊 Monitoring & Debugging

### **Check Build Status**
```bash
gh run list
gh run view <run-id>
gh run view <run-id> --log
```

### **Check Terraform State**
```bash
cd terraform
terraform show
terraform state list
```

### **View Deployed Site**
```bash
gh browse --settings  # Opens repository settings
```

### **Common Issues**

**Build fails with Unity license error:**
- Ensure UNITY_LICENSE is base64 encoded
- Check Unity version matches in workflow

**GitHub Pages shows 404:**
- Wait 5-10 minutes after first deployment
- Verify gh-pages branch exists
- Check Pages settings enabled

**Terraform apply fails:**
- Verify GitHub token permissions
- Check token hasn't expired
- Try: `terraform refresh`

---

## 🎯 Next Steps

### **Immediate**
1. ✅ Run `bash scripts/quick-deploy.sh`
2. ✅ Watch first build complete
3. ✅ Visit deployed game URL

### **Short Term**
4. 📝 Add Unity duplicate class fix
5. 🎨 Test Codex content generation
6. 🔧 Configure custom domain (optional)

### **Long Term**
7. 🚀 Add Steam deployment workflow
8. 🎮 Add Itch.io deployment
9. 📊 Setup analytics
10. 🌐 Multi-platform builds

---

## 📚 Documentation

- **Setup Guide**: [docs/INFRASTRUCTURE_SETUP.md](docs/INFRASTRUCTURE_SETUP.md)
- **Terraform Docs**: [terraform/README.md](terraform/README.md)
- **GitHub Actions**: [docs/GITHUB_ACTIONS_DEPLOYMENT.md](docs/GITHUB_ACTIONS_DEPLOYMENT.md)
- **Codex Integration**: [docs/CODEX_READY.md](docs/CODEX_READY.md)

---

## 🎉 You're Ready!

Your complete deployment pipeline is set up:
- ✅ Infrastructure as Code (Terraform)
- ✅ Automated builds (GitHub Actions)
- ✅ Free hosting (GitHub Pages)
- ✅ Code quality checks (PR validation)
- ✅ Content automation (Codex CLI)

**Run the deployment:**
```bash
bash scripts/quick-deploy.sh
```

**Questions?** Check the documentation or open an issue!

---

**Happy Deploying! 🚀🎮**
