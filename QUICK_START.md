# 🚀 Quick Reference - Codex CLI Setup

## ⚡ TL;DR - Run These Commands

### **1. Setup Codex CLI Remote** (1 minute)
```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
bash scripts/setup-codex-remote.sh
```

### **2. Push to BAMG-Studio Repo** (1 minute)
```bash
git push codex-cli main
```

### **3. Build WebGL** (15-30 minutes)
```bash
bash scripts/codex-build-webgl.sh
```

### **4. Or Run Complete Workflow** (15-30 minutes)
```bash
bash scripts/codex-workflow.sh
```

---

## 📁 Files Created

| File | Purpose |
|------|---------|
| `scripts/setup-codex-remote.sh` | Configure Codex CLI remote |
| `scripts/codex-build-webgl.sh` | Build WebGL via Unity CLI |
| `scripts/codex-workflow.sh` | Complete automated workflow |
| `unity/Assets/Editor/CodexBuildScript.cs` | Unity build automation |
| `docs/CODEX_CLI_INTEGRATION.md` | Full integration guide |
| `COLLABORATION_PLAN.md` | Detailed action plan |

---

## 🔄 Git Remotes

| Remote | URL | Purpose | Used By |
|--------|-----|---------|---------|
| `origin` | papaert-cloud/ExecutiveDisorder | Protected development | GitHub Copilot |
| `codex-cli` | BAMG-Studio/ExecutiveDisorder | Builds & deployment | Codex CLI |

---

## 📝 Common Commands

### **Check Remotes**
```bash
git remote -v
```

### **Push to Protected Repo**
```bash
git push origin main
```

### **Push to Codex CLI Repo**
```bash
git push codex-cli main
```

### **Check Build Output**
```bash
ls -lh unity/Builds/WebGL/
```

### **Test Build Locally**
```bash
cd unity/Builds/WebGL/[latest-folder]
python -m http.server 8000
# Open: http://localhost:8000
```

---

## ✅ Verification Checklist

- [ ] Run `bash scripts/setup-codex-remote.sh`
- [ ] Verify `git remote -v` shows both remotes
- [ ] Push to Codex CLI: `git push codex-cli main`
- [ ] Test build: `bash scripts/codex-build-webgl.sh`
- [ ] Check build output exists
- [ ] Verify build pushed to BAMG-Studio

---

## 🆘 Quick Troubleshooting

**Setup script fails?**
```bash
chmod +x scripts/setup-codex-remote.sh
bash scripts/setup-codex-remote.sh
```

**Build fails?**
```bash
# Check Unity version
ls "/mnt/c/Program Files/Unity/Hub/Editor/"

# Update version in script
nano scripts/codex-build-webgl.sh
```

**Wrong Unity path?**
Edit `scripts/codex-build-webgl.sh`:
```bash
UNITY_VERSION="YOUR_VERSION_HERE"
```

---

## 📊 Next Steps

1. ✅ Setup remote → `bash scripts/setup-codex-remote.sh`
2. ✅ Push to Codex CLI → `git push codex-cli main`
3. ✅ Build → `bash scripts/codex-build-webgl.sh`
4. ✅ Verify → Check `unity/Builds/WebGL/`

---

**Ready? Start with Step 1!** 🚀
