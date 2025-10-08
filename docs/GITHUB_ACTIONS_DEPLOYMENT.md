# GitHub Actions & Deployment Strategy
## Recommendations for Executive Disorder

---

## 🎯 **Recommended GitHub Actions Workflows**

### **1. Automated Build on Push (Essential)**

Create `.github/workflows/unity-build.yml`:

```yaml
name: Unity WebGL Build

on:
  push:
    branches: [ main, codex-cli ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    name: Build WebGL
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout Repository
        uses: actions/checkout@v4
        with:
          lfs: true
      
      - name: Cache Unity Library
        uses: actions/cache@v3
        with:
          path: unity/Library
          key: Library-${{ hashFiles('unity/Assets/**', 'unity/Packages/**', 'unity/ProjectSettings/**') }}
          restore-keys: Library-
      
      - name: Build Unity Project
        uses: game-ci/unity-builder@v4
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
          UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
          UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
        with:
          projectPath: unity
          targetPlatform: WebGL
          versioning: Semantic
          buildName: ExecutiveDisorder
      
      - name: Upload Build Artifacts
        uses: actions/upload-artifact@v3
        with:
          name: WebGL-Build
          path: build/WebGL
          retention-days: 7
      
      - name: Deploy to Itch.io (on main branch)
        if: github.ref == 'refs/heads/main'
        uses: manleydev/butler-publish-itchio-action@v1.0.3
        env:
          BUTLER_CREDENTIALS: ${{ secrets.BUTLER_API_KEY }}
          CHANNEL: html5
          ITCH_GAME: executive-disorder
          ITCH_USER: ${{ secrets.ITCH_USER }}
          PACKAGE: build/WebGL
```

---

### **2. Codex CLI Automation (Your Custom Workflow)**

Create `.github/workflows/codex-automation.yml`:

```yaml
name: Codex CLI Automation

on:
  workflow_dispatch:
    inputs:
      theme:
        description: 'Content theme to generate'
        required: false
        default: 'political-satire'
  schedule:
    - cron: '0 2 * * 1'  # Weekly on Monday at 2 AM

jobs:
  generate-and-build:
    name: Generate Content & Build
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout Repository
        uses: actions/checkout@v4
        with:
          token: ${{ secrets.GITHUB_TOKEN }}
          lfs: true
      
      - name: Setup Python
        uses: actions/setup-python@v4
        with:
          python-version: '3.12'
          cache: 'pip'
      
      - name: Install Python Dependencies
        run: |
          pip install openai PyYAML pydantic
      
      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '18'
      
      - name: Install Codex CLI
        run: |
          npm install -g @openai/codex
        env:
          OPENAI_API_KEY: ${{ secrets.OPENAI_API_KEY }}
      
      - name: Generate Content
        run: |
          python tools/gen_content.py
          python tools/gen_images.py
          python tools/gen_audio.py
        env:
          OPENAI_API_KEY: ${{ secrets.OPENAI_API_KEY }}
      
      - name: Import Data to Unity
        run: |
          bash scripts/codex-import-data.sh
      
      - name: Build Unity WebGL
        uses: game-ci/unity-builder@v4
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
        with:
          projectPath: unity
          targetPlatform: WebGL
      
      - name: Commit Generated Content
        run: |
          git config --local user.email "github-actions[bot]@users.noreply.github.com"
          git config --local user.name "github-actions[bot]"
          git add data/ unity/Assets/Game/ unity/Assets/Art/Generated/
          git commit -m "🤖 Auto-generate content via Codex CLI" || echo "No changes"
          git push
```

---

### **3. Pull Request Validation**

Create `.github/workflows/pr-validation.yml`:

```yaml
name: PR Validation

on:
  pull_request:
    branches: [ main ]

jobs:
  lint-and-test:
    name: Lint & Test
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup Python
        uses: actions/setup-python@v4
        with:
          python-version: '3.12'
      
      - name: Lint Python Files
        run: |
          pip install pylint black
          black --check tools/
          pylint tools/*.py
      
      - name: Check C# Formatting
        uses: dotnet/format@v3
        with:
          workspace: ExecutiveDisorder.sln
      
      - name: Validate YAML Schemas
        run: |
          pip install yamllint
          yamllint data/
      
      - name: Check for Duplicate Classes
        run: |
          bash scripts/check-duplicates.sh
```

---

### **4. Automated Release & Deployment**

Create `.github/workflows/release.yml`:

```yaml
name: Release & Deploy

on:
  push:
    tags:
      - 'v*'

jobs:
  build-and-release:
    name: Build All Platforms
    runs-on: ubuntu-latest
    strategy:
      matrix:
        platform: [WebGL, StandaloneWindows64, StandaloneLinux64]
    
    steps:
      - uses: actions/checkout@v4
        with:
          lfs: true
      
      - name: Build for ${{ matrix.platform }}
        uses: game-ci/unity-builder@v4
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
        with:
          projectPath: unity
          targetPlatform: ${{ matrix.platform }}
          versioning: Tag
      
      - name: Upload to GitHub Release
        uses: softprops/action-gh-release@v1
        with:
          files: build/${{ matrix.platform }}/**/*
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
      
      - name: Deploy WebGL to Itch.io
        if: matrix.platform == 'WebGL'
        uses: manleydev/butler-publish-itchio-action@v1.0.3
        env:
          BUTLER_CREDENTIALS: ${{ secrets.BUTLER_API_KEY }}
          CHANNEL: html5
          PACKAGE: build/WebGL
      
      - name: Deploy to Steam
        if: matrix.platform == 'StandaloneWindows64'
        uses: game-ci/steam-deploy@v3
        with:
          username: ${{ secrets.STEAM_USERNAME }}
          configVdf: ${{ secrets.STEAM_CONFIG_VDF }}
          appId: ${{ secrets.STEAM_APP_ID }}
          buildDescription: ${{ github.ref_name }}
          rootPath: build/StandaloneWindows64
          depot1Path: ExecutiveDisorder
```

---

## 🚀 **Deployment Strategies**

### **Option 1: Itch.io (Recommended for Indie Games)**

**Pros:**
- Easy setup, great for web games
- Built-in analytics and community features
- Pay-what-you-want pricing model

**Setup:**
```bash
# Install Butler (Itch.io CLI)
curl -L -o butler.zip https://broth.itch.ovh/butler/linux-amd64/LATEST/archive/default
unzip butler.zip
./butler login

# Deploy
./butler push unity/WebGLBuild bamg-studio/executive-disorder:html5
```

**GitHub Secret needed:** `BUTLER_API_KEY`

---

### **Option 2: GitHub Pages (Free WebGL Hosting)**

Create `.github/workflows/deploy-pages.yml`:

```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    permissions:
      contents: read
      pages: write
      id-token: write
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Build Unity WebGL
        uses: game-ci/unity-builder@v4
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
        with:
          projectPath: unity
          targetPlatform: WebGL
      
      - name: Setup Pages
        uses: actions/configure-pages@v3
      
      - name: Upload artifact
        uses: actions/upload-pages-artifact@v2
        with:
          path: build/WebGL
      
      - name: Deploy to GitHub Pages
        uses: actions/deploy-pages@v2
```

**Access at:** `https://bamg-studio.github.io/ExecutiveDisorder/`

---

### **Option 3: Vercel/Netlify (Professional WebGL)**

**Vercel Setup:**
```json
// vercel.json
{
  "buildCommand": "bash scripts/codex-build-webgl.sh",
  "outputDirectory": "unity/WebGLBuild",
  "framework": null,
  "headers": [
    {
      "source": "/(.*)",
      "headers": [
        {
          "key": "Cross-Origin-Embedder-Policy",
          "value": "require-corp"
        },
        {
          "key": "Cross-Origin-Opener-Policy",
          "value": "same-origin"
        }
      ]
    }
  ]
}
```

---

### **Option 4: Steam (Full Commercial Release)**

**Requirements:**
- Steam Partner account ($100 fee)
- Steamworks SDK integration
- Steam AppID

**GitHub Action:**
```yaml
- name: Deploy to Steam
  uses: game-ci/steam-deploy@v3
  with:
    username: ${{ secrets.STEAM_USERNAME }}
    configVdf: ${{ secrets.STEAM_CONFIG_VDF }}
    appId: 480  # Your Steam AppID
    buildDescription: v${{ github.ref_name }}
    rootPath: build
    depot1Path: StandaloneWindows64
    depot2Path: StandaloneLinux64
```

---

## 🔐 **Required GitHub Secrets**

Set these in: `Settings → Secrets and variables → Actions`

### **Unity Licensing:**
```bash
UNITY_LICENSE      # Your Unity license file content
UNITY_EMAIL        # Unity account email
UNITY_PASSWORD     # Unity account password
```

### **OpenAI API:**
```bash
OPENAI_API_KEY     # For Codex CLI and content generation
```

### **Itch.io Deployment:**
```bash
BUTLER_API_KEY     # From https://itch.io/user/settings/api-keys
ITCH_USER          # Your itch.io username
```

### **Steam Deployment:**
```bash
STEAM_USERNAME     # Steam account username
STEAM_CONFIG_VDF   # Steam config file (from steamcmd)
STEAM_APP_ID       # Your Steam AppID
```

---

## 📋 **My Top Recommendations**

### **Phase 1: Immediate Setup (This Week)**

1. ✅ **Enable GitHub Actions**
   - Create `.github/workflows/unity-build.yml`
   - Add Unity license to secrets
   - Test with a push to `codex-cli`

2. ✅ **Setup Itch.io Deployment**
   - Create itch.io page
   - Add Butler API key to secrets
   - Auto-deploy on main branch merges

3. ✅ **Add PR Validation**
   - Lint Python/C# code
   - Check for duplicate classes
   - Validate YAML schemas

### **Phase 2: Automation (Next Week)**

4. 📅 **Codex CLI Scheduled Workflow**
   - Weekly content generation
   - Auto-commit and build
   - Deploy previews

5. 📅 **GitHub Pages Hosting**
   - Free WebGL deployment
   - Automatic on every main push
   - Good for playtesting

### **Phase 3: Production (When Ready)**

6. 🚀 **Steam Integration**
   - Setup Steamworks
   - Add Steam deployment workflow
   - Multi-platform builds

7. 🚀 **Release Automation**
   - Tag-based releases
   - Automated changelogs
   - Multi-platform builds

---

## 🛠️ **Quick Start Commands**

### **Create Workflow Files:**
```bash
mkdir -p .github/workflows
# I'll create these files for you if you approve
```

### **Test Locally:**
```bash
# Install act (GitHub Actions local runner)
curl https://raw.githubusercontent.com/nektos/act/master/install.sh | sudo bash

# Test workflow locally
act -W .github/workflows/unity-build.yml
```

### **Setup Secrets:**
```bash
# Using GitHub CLI
gh secret set UNITY_LICENSE < unity-license.ulf
gh secret set OPENAI_API_KEY --body "sk-..."
gh secret set BUTLER_API_KEY --body "..."
```

---

## 🎯 **Best Practice: Recommended Flow**

```
Developer Push → GitHub
         ↓
    Run Tests & Validation (PR checks)
         ↓
    Merge to Main
         ↓
    Build Unity WebGL
         ↓
    Deploy to Itch.io (dev channel)
         ↓
    [Manual QA Testing]
         ↓
    Tag Release (v1.0.0)
         ↓
    Build All Platforms
         ↓
    Deploy to Steam + Itch.io (prod)
```

---

## 📊 **Cost Analysis**

| Service | Cost | Best For |
|---------|------|----------|
| GitHub Actions | 2,000 min/month free | All projects |
| GitHub Pages | Free | WebGL hosting |
| Itch.io | Free (optional revenue share) | Indie distribution |
| Vercel | Free tier available | Professional WebGL |
| Steam | $100 one-time + 30% revenue | Commercial release |

---

## ✅ **What Should We Set Up First?**

**My Recommendation:** Start with this minimal setup:

1. **Unity Build Workflow** - Auto-build on push
2. **Itch.io Deployment** - Easy web distribution
3. **PR Validation** - Code quality checks

This gives you:
- ✅ Automated builds
- ✅ Public playable builds
- ✅ Code quality enforcement
- ✅ Zero cost

**Would you like me to create these workflow files now?**
