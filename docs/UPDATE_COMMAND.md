# GitHub Copilot "update" Command Reference

## 🎯 Purpose
When you type **"update"** in chat, GitHub Copilot will assist Codex CLI with EVERYTHING.

---

## 🤖 What Happens When You Say "update"

GitHub Copilot will automatically:

### 1. **Check Build Status** ✅
- Verify Unity project integrity
- Check for compilation errors
- Validate asset references
- Ensure scenes and data are scaffolded (runs `scripts/codex-init.sh` if needed)

### 2. **Create/Update Build Scripts** 📝
- Generate new automation scripts as needed
- Update existing build configurations
- Optimize build parameters

### 3. **Run Unity Build** 🎮
- Execute WebGL build process
- Monitor build progress
- Handle errors automatically

### 4. **Commit & Version Control** 💾
- Stage build artifacts
- Create descriptive commit messages
- Include build metadata

### 5. **Push to BAMG-Studio** 🚀
- Push to codex-cli remote
- Update build documentation
- Tag releases if applicable

### 6. **Generate Documentation** 📚
- Update build logs
- Create changelog entries
- Document any issues

---

## 💬 Usage Examples

### Basic Update
```
You: update
```
GitHub Copilot will run the full workflow.

### Update with Context
```
You: update - fixed character selection bug
```
GitHub Copilot will include your note in the commit message.

### Build-Only Update
```
You: update build only
```
GitHub Copilot will only build, not commit/push.

### Documentation Update
```
You: update docs
```
GitHub Copilot will regenerate all documentation.

---

## 🔧 Behind the Scenes

When you say "update", GitHub Copilot executes:

```bash
# 1. Pre-build checks
bash scripts/codex-workflow.sh --check

# 2. Unity build
bash scripts/codex-build-webgl.sh

# 3. Commit
git add unity/Builds/
git commit -m "🤖 Codex CLI: WebGL Build [timestamp]"

# 4. Push
git push codex-cli codex-cli:main

# 5. Documentation
# Auto-generate build reports
```

---

## 🧱 Week 1 Bootstrap

Initialize scenes, data, and settings:

```
bash scripts/codex-init.sh
```

---

## 📊 What Gets Updated

### Code & Scripts
- ✅ Unity C# scripts
- ✅ Build automation scripts
- ✅ Editor tools
- ✅ Configuration files

### Build Artifacts
- ✅ WebGL builds
- ✅ Asset bundles
- ✅ Build logs
- ✅ Performance reports

### Documentation
- ✅ README updates
- ✅ Changelog entries
- ✅ API documentation
- ✅ Build guides

### Version Control
- ✅ Commits to codex-cli branch
- ✅ Push to BAMG-Studio repo
- ✅ Tag releases
- ✅ Update branch protection

---

## 🚨 Error Handling

If something fails during "update":

### Build Errors
GitHub Copilot will:
1. Analyze Unity logs
2. Identify the issue
3. Suggest fixes
4. Optionally auto-fix if possible

### Git Conflicts
GitHub Copilot will:
1. Detect merge conflicts
2. Show conflicting files
3. Help resolve manually
4. Re-run update after resolution

### Remote Push Failures
GitHub Copilot will:
1. Check authentication
2. Verify remote configuration
3. Retry with correct credentials
4. Report if manual intervention needed

---

## 🎯 Success Indicators

After "update" completes, you'll see:

```
✅ Build completed successfully
✅ Committed: 🤖 Codex CLI: WebGL Build 20251008_143022
✅ Pushed to BAMG-Studio/ExecutiveDisorder
✅ Documentation updated

📦 Build available at:
   https://github.com/BAMG-Studio/ExecutiveDisorder/tree/main/unity/Builds/WebGL
```

---

## 🔄 Update Frequency

### Recommended Schedule
- **After each feature**: `update` to checkpoint
- **Before switching tasks**: `update` to save state
- **Daily**: `update` for continuous integration
- **Before testing**: `update` to ensure latest build

---

## 🛡️ Safety Features

GitHub Copilot ensures:
- ✅ **Never pushes to `origin`** (protected papaert-cloud repo)
- ✅ **Only pushes to `codex-cli`** (BAMG-Studio repo)
- ✅ **Validates builds before commit**
- ✅ **Creates backup branches automatically**
- ✅ **Rollback capability if needed**

---

## 📞 Quick Reference

| Command | Action |
|---------|--------|
| `update` | Full workflow: build + commit + push |
| `update build` | Build only |
| `update docs` | Documentation only |
| `update check` | Verify setup |
| `update fix` | Analyze and fix last build error |

---

## 🎉 That's It!

Just type **"update"** and GitHub Copilot handles everything for Codex CLI!

**Next**: Open chat and try it:
```
update
```

GitHub Copilot will take care of the rest! 🚀
