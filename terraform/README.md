# Terraform Infrastructure

This directory contains Infrastructure as Code (IaC) for the Executive Disorder project.

## 📁 Files

- `main.tf` - Main Terraform configuration (GitHub repository, Pages, secrets)
- `variables.tf` - Input variables definition
- `backend.tf` - Terraform state backend configuration
- `terraform.tfvars.example` - Example variables file
- `terraform.tfvars` - **Your actual values (DO NOT COMMIT)**

## 🚀 Quick Start

### 1. Install Terraform

```bash
# Ubuntu/WSL
wget -O- https://apt.releases.hashicorp.com/gpg | sudo gpg --dearmor -o /usr/share/keyrings/hashicorp-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/hashicorp-archive-keyring.gpg] https://apt.releases.hashicorp.com $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/hashicorp.list
sudo apt update && sudo apt install terraform
```

### 2. Configure Variables

```bash
cp terraform.tfvars.example terraform.tfvars
# Edit terraform.tfvars with your values
```

### 3. Initialize & Apply

```bash
terraform init
terraform plan
terraform apply
```

Or use the helper script:

```bash
bash ../scripts/setup-infrastructure.sh
```

## 🔐 Required Variables

### Mandatory
- `github_token` - GitHub Personal Access Token with `repo` and `workflow` scopes

### Optional (can be set via GitHub UI)
- `unity_license` - Unity license file (base64 encoded)
- `unity_email` - Unity account email
- `unity_password` - Unity account password
- `openai_api_key` - OpenAI API key for Codex CLI

## 📊 What This Creates

### Repository Configuration
- ✅ GitHub Pages enabled (gh-pages branch)
- ✅ Branch protection rules (main, codex-cli)
- ✅ Repository topics/tags
- ✅ Security scanning

### Secrets & Variables
- Secrets: UNITY_LICENSE, UNITY_EMAIL, UNITY_PASSWORD, OPENAI_API_KEY
- Variables: UNITY_VERSION, BUILD_TARGET

### Branches
- `gh-pages` - Deployment branch for GitHub Pages

## 🛠️ Commands

### Initialize
```bash
terraform init
```

### Plan Changes
```bash
terraform plan
```

### Apply Changes
```bash
terraform apply
```

### Show Current State
```bash
terraform show
```

### List Resources
```bash
terraform state list
```

### Destroy Everything (⚠️ DANGER)
```bash
terraform destroy
```

## 🔄 Updating Infrastructure

1. Edit `.tf` files or `terraform.tfvars`
2. Run `terraform plan` to preview changes
3. Run `terraform apply` to apply changes

## 📚 Resources Created

- `github_repository.executive_disorder` - Repository configuration
- `github_branch_protection.main` - Main branch protection
- `github_branch_protection.codex_cli` - Codex-cli branch protection
- `github_branch.gh_pages` - GitHub Pages deployment branch
- `github_actions_secret.*` - GitHub Actions secrets
- `github_actions_variable.*` - GitHub Actions variables

## 🔒 Security

- ✅ `terraform.tfvars` is in `.gitignore` - never commit it!
- ✅ Secrets are marked as sensitive
- ✅ Use environment variables for CI/CD
- ✅ Rotate tokens regularly

## 🐛 Troubleshooting

### "Error: Invalid GitHub token"
- Ensure token has `repo` and `workflow` scopes
- Check token hasn't expired

### "Error: Repository already exists"
- Terraform will import existing repository
- Or comment out the repository resource temporarily

### "Error: Branch already exists"
- Normal if repository was previously configured
- Terraform will manage existing branches

## 📖 Documentation

- [Terraform GitHub Provider](https://registry.terraform.io/providers/integrations/github/latest/docs)
- [Terraform CLI](https://www.terraform.io/cli)
- [Infrastructure Setup Guide](../docs/INFRASTRUCTURE_SETUP.md)

## 💡 Tips

- Use `terraform fmt` to format files
- Use `terraform validate` to check syntax
- Use `terraform console` for debugging
- Keep state file secure (use remote backend for teams)

---

**Questions?** Check the [Infrastructure Setup Guide](../docs/INFRASTRUCTURE_SETUP.md)
