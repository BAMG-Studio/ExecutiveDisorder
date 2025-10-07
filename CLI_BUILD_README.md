# 🚀 Command Line Build - Quick Start

## ✅ **Your Unity Installation**
- Unity 6 (6000.0.40f1) ✅ Found
- Unity 6 (6000.1.17f1) ✅ Found  
- Unity 6 (6000.2.6f2) ✅ Found
- Unity 2022.3.59f1 ✅ Found

Build scripts are configured for: **Unity 6.0.40f1**

---

## 🎯 **Build WebGL (No Unity Editor UI)**

### **Option 1: WSL Terminal** (Recommended)

```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
./scripts/build-webgl.sh
```

### **Option 2: Windows Command Prompt**

```batch
cd C:\Users\POK28\source\repos\ExecutiveDisorderReplit
scripts\build-webgl.bat
```

### **Option 3: Manual Command**

```bash
"/mnt/c/Program Files/Unity/Hub/Editor/6000.0.40f1/Editor/Unity.exe" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity" \
  -buildTarget WebGL \
  -executeMethod BuildScript.BuildWebGL \
  -logFile "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/build.log"
```

---

## ⏱️ **What Happens**

1. **Checks** Unity installation (5 seconds)
2. **Verifies** project exists (5 seconds)
3. **Builds** WebGL version (10-30 minutes)
4. **Creates** timestamped output folder
5. **Generates** build log

---

## 📁 **Output Location**

```
Builds/
├── WebGL/
│   └── 20251007_143022/      ← Timestamped
│       ├── index.html
│       ├── Build/
│       └── TemplateData/
└── Logs/
    └── build_20251007_143022.log
```

---

## 🧪 **Test Build Locally**

```bash
# Navigate to build
cd Builds/WebGL/[latest-folder]

# Start server
python3 -m http.server 8000

# Open browser
# http://localhost:8000
```

---

## ⚠️ **Important Notes**

### **Unity Engine IS Required**
- ❌ Cannot use Codex CLI alone
- ❌ Cannot avoid Unity completely
- ✅ CAN build from terminal without opening Unity Editor UI
- ✅ CAN automate builds with scripts
- ✅ CAN integrate with CI/CD

### **First-Time Setup Requirement**
You need to open Unity Editor **ONCE** to:
1. Configure build settings (add scenes)
2. Set up managers in scenes
3. Assign ScriptableObjects

After that, you can build from terminal forever.

---

## 🔧 **First-Time Setup (Unity Editor - One Time Only)**

### **Step 1: Open Project**
```bash
# From Unity Hub, open:
C:\Users\POK28\source\repos\ExecutiveDisorderReplit\unity
```

### **Step 2: Configure Build Settings**
1. File → Build Settings
2. Click "Add Open Scenes" for all 4 scenes:
   - MainMenu
   - GamePlay  
   - LoadingScreen
   - EndingCinematic
3. Select Platform: WebGL
4. Close

### **Step 3: Close Unity Editor**

### **Step 4: Build from Terminal**
```bash
./scripts/build-webgl.sh
```

**That's it!** All future builds are terminal-only.

---

## 📊 **Build Monitoring**

### **Watch Build Progress:**
```bash
# In another terminal
tail -f Builds/Logs/[latest-log].log
```

### **Check Unity Process:**
```bash
ps aux | grep Unity
```

### **Check Build Size:**
```bash
du -sh Builds/WebGL/*
```

---

## 🚀 **Automated Builds**

### **Nightly Build (Cron Job)**
```bash
# Add to crontab
0 2 * * * cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit && ./scripts/build-webgl.sh
```

### **CI/CD (GitHub Actions)**
See: `.github/workflows/build.yml` (coming soon)

---

## 📚 **Documentation**

- **Full CLI Guide**: `docs/CLI_BUILD_GUIDE.md`
- **Build Checklist**: `BUILD_CHECKLIST.md`
- **AI Workflow**: `AI_BUILD_WORKFLOW.md`

---

## 🎯 **Summary**

**Question:** Can I use Codex CLI to avoid Unity?
**Answer:** ❌ No, Unity Engine is required

**Question:** Can I build from terminal without Unity Editor UI?
**Answer:** ✅ Yes! Use `./scripts/build-webgl.sh`

**Question:** Do I ever need to open Unity Editor?
**Answer:** ⚠️ Once, for initial setup. Then never again.

---

## 🔥 **Quick Action**

**To build right now:**

```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
./scripts/build-webgl.sh
```

**Wait 10-30 minutes, then:**

```bash
cd Builds/WebGL/[latest]
python3 -m http.server 8000
```

**Open:** http://localhost:8000

**Done!** 🎉
