# Codex CLI Integration Guide for Executive Disorder

## 🎯 Repository Strategy

### **Dual Remote Setup**

**Protected Repository (GitHub Copilot):**
- Remote name: `origin`
- URL: `https://github.com/papaert-cloud/ExecutiveDisorder.git`
- Usage: GitHub Copilot assists here, protected from Codex CLI

**Codex CLI Repository:**
- Remote name: `codex-cli`
- URL: `https://github.com/BAMG-Studio/ExecutiveDisorder.git`
- Usage: Codex CLI builds and version controls here

---

## 🔧 **Setup Instructions**

### **Step 1: Run Setup Script**

```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
bash scripts/setup-codex-remote.sh
```

This will:
- Add `codex-cli` remote pointing to BAMG-Studio repo
- Keep `origin` pointing to your protected papaert-cloud repo
- Show you the remote configuration

### **Step 2: Verify Configuration**

```bash
git remote -v
```

**Expected output:**
```
codex-cli    https://ghp_...@github.com/BAMG-Studio/ExecutiveDisorder.git (fetch)
codex-cli    https://ghp_...@github.com/BAMG-Studio/ExecutiveDisorder.git (push)
origin       https://github.com/papaert-cloud/ExecutiveDisorder.git (fetch)
origin       https://github.com/papaert-cloud/ExecutiveDisorder.git (push)
```

---

## 🤖 **Usage Patterns**

### **For Codex CLI (AI builds/pushes to BAMG-Studio):**

```bash
# Push builds to BAMG-Studio repo
git push codex-cli main

# Push specific branch
git push codex-cli build/webgl-v1.0

# Pull from BAMG-Studio
git pull codex-cli main
```

### **For GitHub Copilot (protected workflow):**

```bash
# Business as usual - protected repo
git push origin main
git pull origin main
```

---

## 📦 **Codex CLI Build Workflow**

### **Phase 1: Initial Commit to BAMG-Studio**

```bash
# Ensure you're on main branch
git checkout main

# Add all Unity project files
git add unity/
git add ExecutiveDisorder_Unity6_Complete/
git add scripts/
git add docs/

# Commit
git commit -m "Initial Unity 6 project - Executive Disorder"

# Push to Codex CLI repo
git push codex-cli main
```

### **Phase 2: Codex CLI Can Now Build**

Codex CLI will:
1. Clone from `BAMG-Studio/ExecutiveDisorder`
2. Run data import (auto during build) or manually via `scripts/codex-import-data.sh`
3. Build WebGL using Unity command-line
4. Commit build artifacts
5. Push back to `codex-cli` remote

---

## 🛡️ **Protection Strategy**

### **What's Protected:**
- ✅ Your original `papaert-cloud/ExecutiveDisorder` repo
- ✅ GitHub Copilot only touches `origin` remote
- ✅ Codex CLI isolated to `codex-cli` remote

### **Separation of Concerns:**

| Tool | Remote | Repository | Purpose |
|------|--------|------------|---------|
| GitHub Copilot | `origin` | papaert-cloud/ExecutiveDisorder | Code assistance, protected workspace |
| Codex CLI | `codex-cli` | BAMG-Studio/ExecutiveDisorder | Automated builds, version control |

### **Conflict Prevention:**

```bash
# GitHub Copilot workflow
git fetch origin
git push origin feature-branch

# Codex CLI workflow  
git fetch codex-cli
git push codex-cli build/webgl
```

They never interfere with each other!

---

## 🚀 **GitHub Copilot Assistance Plan**

### **What I Will Do:**

#### **1. Code Assistance (Protected Repo)**
- ✅ Review and improve C# scripts
- ✅ Debug Unity components
- ✅ Optimize game logic
- ✅ Create documentation
- ✅ Generate build automation scripts
- ✅ **Never push to `codex-cli` remote**

#### **2. Create Build Scripts for Codex CLI**
- ✅ Unity command-line build scripts
- ✅ Asset optimization scripts
- ✅ CI/CD configurations
- ✅ Automated testing scripts

#### **3. Documentation**
- ✅ Build guides
- ✅ API documentation
- ✅ Architecture diagrams
- ✅ Deployment guides

#### **4. Code Review**
- ✅ Check Codex CLI commits
- ✅ Suggest improvements
- ✅ Identify bugs
- ✅ Security audits

---

## 🤖 **Codex CLI Assistance Plan**

### **What Codex CLI Will Do:**

#### **1. Automated Builds**
```bash
# Codex CLI executes Unity builds
unity-editor -batchmode -nographics -projectPath ./unity \
  -executeMethod BuildScript.BuildWebGL \
  -quit
```

#### **2. Version Control (BAMG-Studio Repo)**
```bash
# Codex commits and pushes builds
git add Builds/WebGL/
git commit -m "Build: WebGL v1.0.0 - [timestamp]"
git push codex-cli main
```

#### **3. CI/CD Automation**
- Automated build on push
- Asset bundling
- Build artifact management
- Deployment to hosting platforms

---

## 📋 **Detailed Action Plan**

### **Phase 1: Setup (NOW)**

**You do:**
1. ✅ Run `bash scripts/setup-codex-remote.sh`
2. ✅ Verify dual remotes with `git remote -v`
3. ✅ Confirm setup complete

**I provide:**
- ✅ Setup script (done)
- ✅ Documentation (done)
- ✅ Verification commands (done)

---

### **Phase 2: Initial Codex CLI Sync (NEXT)**

**You do:**
```bash
# Push current state to Codex CLI repo
git push codex-cli main --force  # Force only for first time
```

**I provide:**
- Build automation scripts
- Unity Editor command-line wrappers
- CI/CD configuration files

---

### **Phase 3: Build Script Creation**

**I will create:**

1. **`scripts/codex-build-webgl.sh`** - Codex CLI build script
2. **`unity/Assets/Editor/CodexDataImporter.cs`** - JSON→ScriptableObjects importer (auto-run before build)
3. **`.github/workflows/codex-build.yml`** - GitHub Actions (if needed)
4. **`scripts/codex-deploy.sh`** - Deployment automation

---

### **Phase 4: Continuous Integration**

**Workflow:**

```
Developer makes changes
       ↓
GitHub Copilot assists (origin repo)
       ↓
Push to origin (protected)
       ↓
Manually trigger: git push codex-cli main
       ↓
Codex CLI builds automatically (imports data first)
       ↓
Build pushed to BAMG-Studio repo
       ↓
Deployed to web hosting
```

---

## 🔒 **Security Notes**

### **Token Protection:**
- ✅ Token embedded in remote URL (local only)
- ✅ Never committed to repository
- ✅ Scoped to BAMG-Studio repo only

### **Best Practices:**
```bash
# Check which remote you're pushing to
git remote -v

# Always specify remote explicitly
git push codex-cli main  # Intentional
git push origin main     # Intentional

# Avoid accidental pushes
git config push.default nothing  # Require explicit remote
```

---

## 📊 **Monitoring & Verification**

### **Check Codex CLI Status:**
```bash
# What's on Codex CLI repo?
git fetch codex-cli
git log codex-cli/main

# Compare with protected origin
git log origin/main..codex-cli/main
```

### **Verify Separation:**
```bash
# Show all remotes
git remote -v

# Show branches per remote
git branch -r
```

---

## 🆘 **Troubleshooting**

### **Codex CLI push fails:**
```bash
# Check authentication
git remote get-url codex-cli

# Re-add with token
git remote set-url codex-cli https://ghp_TOKEN@github.com/BAMG-Studio/ExecutiveDisorder.git
```

### **Accidentally pushed to wrong remote:**
```bash
# Undo last push to wrong remote
git push origin +HEAD~1:main  # Rollback origin
# or
git push codex-cli +HEAD~1:main  # Rollback codex-cli
```

---

## 🎯 **Summary**

### **Repository Isolation:**
- 🔐 **origin** → papaert-cloud (protected, GitHub Copilot)
- 🤖 **codex-cli** → BAMG-Studio (builds, Codex CLI)

### **Tool Responsibilities:**
- 👨‍💻 **GitHub Copilot** → Code, docs, scripts (origin only)
- 🤖 **Codex CLI** → Builds, CI/CD (codex-cli only)

### **Safety Guarantees:**
- ✅ No cross-contamination
- ✅ Independent workflows
- ✅ Protected repositories
- ✅ Clear separation of concerns

---

## 📞 **Next Steps**

**Awaiting your instruction to:**
1. Run the setup script (`bash scripts/setup-codex-remote.sh`)
2. Verify dual remote configuration
3. Create Codex CLI build automation scripts
4. Push initial state to BAMG-Studio repo

**Ready when you are!** 🚀
