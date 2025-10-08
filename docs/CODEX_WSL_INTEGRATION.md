# Codex CLI Integration Guide - WSL2 & Windows

## 🎯 **Situation**

- ✅ **Codex CLI installed in Windows** at: `C:\Users\POK28\AppData\Roaming\npm\`
- ✅ **Files found:**
  - `codex` (bash script)
  - `codex.cmd` (Windows batch file)  
  - `codex.ps1` (PowerShell script)
- ❌ **Not accessible from WSL** (different environment)

---

## 🔧 **Solutions to Access Codex from WSL**

### **Option 1: Add Windows npm to WSL PATH (Easiest)**

Add Windows npm global bin to your WSL PATH:

```bash
# Add to ~/.bashrc or ~/.zshrc
echo 'export PATH="$PATH:/mnt/c/Users/POK28/AppData/Roaming/npm"' >> ~/.bashrc
source ~/.bashrc

# Test
codex --version
```

**Pros:** Simple, automatic  
**Cons:** Might conflict with WSL npm packages

---

### **Option 2: Use the Wrapper Script (Recommended)**

Use the wrapper we created:

```bash
# From your project root
bash scripts/codex-wrapper.sh --version

# Or create an alias
alias codex='bash /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/scripts/codex-wrapper.sh'
```

**Current wrapper location:**
`scripts/codex-wrapper.sh`

---

### **Option 3: Direct PowerShell Call**

Call codex directly through PowerShell:

```bash
powershell.exe -Command "& 'C:\Users\POK28\AppData\Roaming\npm\codex.ps1' --version"
```

**Pros:** Most reliable  
**Cons:** Verbose syntax

---

### **Option 4: Install Codex in WSL Too**

Install codex natively in WSL:

```bash
# If you have sudo access
sudo npm install -g @<codex-package-name>

# Or without sudo
npm config set prefix ~/.npm-global
export PATH=~/.npm-global/bin:$PATH
npm install -g @<codex-package-name>
```

**Pros:** Native WSL access  
**Cons:** Duplicate installation

---

## 🚀 **Recommended Setup for Your Project**

### **Step 1: Create Alias (Quick)**

```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit

# Add alias to your shell config
echo "alias codex='powershell.exe -NoProfile -Command codex'" >> ~/.bashrc
source ~/.bashrc

# Test
codex --version
```

### **Step 2: Update Project Scripts**

Update `scripts/codex-workflow.sh` to use codex:

```bash
# Instead of calling codex directly, use:
if command -v codex &> /dev/null; then
    codex build
elif [ -f "scripts/codex-wrapper.sh" ]; then
    bash scripts/codex-wrapper.sh build
else
    echo "⚠️  Codex CLI not available, skipping"
fi
```

### **Step 3: Add to VS Code Settings**

Create `.vscode/settings.json`:

```json
{
  "terminal.integrated.env.linux": {
    "PATH": "${env:PATH}:/mnt/c/Users/POK28/AppData/Roaming/npm"
  }
}
```

---

## 🧪 **Testing Codex Access**

### **From WSL Terminal:**

```bash
# Test 1: Direct PowerShell call
powershell.exe -Command "codex --version"

# Test 2: Via wrapper
bash scripts/codex-wrapper.sh --version

# Test 3: Check if in PATH
which codex
```

### **From PowerShell:**

```powershell
# Open PowerShell terminal in VS Code
codex --version
codex --help
```

---

## 📝 **Integration with Your Workflow**

### **Update codex-workflow.sh**

```bash
# Add Codex integration to your workflow
echo "🤖 Checking for Codex CLI..."

if powershell.exe -Command "Get-Command codex -ErrorAction SilentlyContinue" &> /dev/null; then
    echo "✅ Codex CLI available"
    
    # Use Codex for builds
    powershell.exe -Command "codex build --target webgl"
else
    echo "⚠️  Codex CLI not available, using manual build"
    bash scripts/codex-build-webgl.sh
fi
```

---

## 🎯 **Quick Commands Reference**

### **Call Codex from WSL:**

```bash
# Option A: PowerShell wrapper
powershell.exe -Command "codex <args>"

# Option B: Direct script
"/mnt/c/Users/POK28/AppData/Roaming/npm/codex" <args>

# Option C: Your wrapper
bash scripts/codex-wrapper.sh <args>
```

### **Call Codex from PowerShell Terminal:**

```powershell
codex <args>
```

---

## 🐛 **Troubleshooting**

### **"codex: command not found" in WSL**

**Cause:** Windows npm global not in WSL PATH  
**Fix:**
```bash
export PATH="$PATH:/mnt/c/Users/POK28/AppData/Roaming/npm"
```

### **"The term 'codex' is not recognized" in PowerShell**

**Cause:** PowerShell session doesn't have npm in PATH  
**Fix:**
```powershell
$env:PATH += ";C:\Users\POK28\AppData\Roaming\npm"
```

### **Permission errors**

**Cause:** WSL file permissions  
**Fix:**
```bash
chmod +x scripts/codex-wrapper.sh
```

---

## ✅ **Next Steps**

1. **Choose your preferred method** (I recommend Option 1 - add to PATH)
2. **Test codex access** from both WSL and PowerShell
3. **Update your workflow scripts** to use Codex
4. **Commit the changes** to your repository

---

## 💡 **Pro Tip**

For the best experience, use **PowerShell terminal in VS Code** when working with Codex:

1. Open VS Code
2. Terminal → New Terminal
3. Select "PowerShell" from dropdown
4. Run: `codex --version`

This gives you native Windows access while staying in your IDE!

---

**Need help implementing any of these solutions? Just ask!** 🚀
