# ✅ Codex CLI - Ready for Use!

## 🎉 Installation Complete

**Codex CLI v0.45.0** is now installed and accessible from WSL!

```bash
codex --version
# codex-cli 0.45.0
```

---

## 🔧 How It Works

- **Windows Installation**: `C:\Users\POK28\AppData\Roaming\npm\node_modules\@openai\codex`
- **WSL Access**: `/usr/local/bin/codex` → `scripts/codex-wrapper.sh`
- **Wrapper Script**: Directly calls Node.js with correct Windows paths

---

## 🚀 Available Commands

### **Interactive Mode**
```bash
codex "build the Unity project"
codex "generate new character cards"
codex "review the code and suggest improvements"
```

### **Non-Interactive Execution**
```bash
codex exec "create a new enemy type"
codex apply  # Apply latest diff
```

### **Resume Previous Sessions**
```bash
codex resume --last    # Continue most recent session
codex resume           # Pick from history
```

### **MCP Server Mode (Experimental)**
```bash
codex mcp             # Manage MCP servers
codex mcp-server      # Run as MCP server
```

### **Cloud Integration (Experimental)**
```bash
codex cloud           # Browse tasks from Codex Cloud
```

---

## 🎯 Integration with Your Workflow

### **Option 1: Use Codex Directly**
```bash
# Instead of manual builds
codex "build the WebGL project and commit changes"

# Instead of manual content generation
codex "generate 5 new crisis cards with cyberpunk theme"
```

### **Option 2: Integrate with Scripts**

Update `scripts/codex-workflow.sh`:

```bash
#!/bin/bash
set -e

echo "🤖 Running Codex-driven workflow..."

# Let Codex handle the entire pipeline
codex exec "
1. Generate content from data/themes/current_theme.json
2. Build Unity WebGL project
3. Commit changes with descriptive message
4. Push to codex-cli branch
"
```

### **Option 3: Hybrid Approach**

Use Codex for complex tasks, manual scripts for simple ones:

```bash
# Use Codex for AI-powered tasks
codex "analyze the codebase and suggest architecture improvements"

# Use manual scripts for deterministic tasks
bash scripts/codex-build-webgl.sh
```

---

## 📝 Configuration

### **Model Selection**
```bash
# Use specific model
codex -m o3 "complex task"

# Use local OSS model (requires Ollama)
codex --oss "task"
```

### **Sandbox Policies**
```bash
# Read-only sandbox (safest)
codex -s read-only "analyze code"

# Workspace write access (recommended for builds)
codex -s workspace-write "build project"

# Full access (dangerous!)
codex -s danger-full-access "system modifications"
```

### **Approval Policies**
```bash
# Ask for approval on untrusted commands
codex -a untrusted "run build"

# Full auto mode (low friction)
codex --full-auto "automated workflow"

# Never ask (DANGEROUS)
codex --dangerously-bypass-approvals-and-sandbox "trusted environment only"
```

---

## 🧪 Test Commands

```bash
# Test basic functionality
codex --version
codex --help

# Test execution
codex exec "echo 'Hello from Codex!'"

# Test with your project
codex "show me the Unity project structure"
codex "what build scripts are available?"
```

---

## 🎮 Game Development Workflows

### **Content Generation**
```bash
codex "generate 3 new political crisis cards with themes: corruption, environmental, technological"
```

### **Build Automation**
```bash
codex "build Unity WebGL project, verify artifacts, and commit if successful"
```

### **Code Review**
```bash
codex "review Assets/Scripts/GameManager.cs and suggest improvements"
```

### **Debugging**
```bash
codex "analyze Unity build errors in Logs/build.log and suggest fixes"
```

---

## 🔗 Integration with GitHub

### **Automatic Commits**
```bash
codex "implement feature X, test it, and create a commit with detailed message"
```

### **PR Workflows**
```bash
codex cloud               # Browse tasks from GitHub
codex apply               # Apply changes locally
git push origin codex-cli # Push to remote
```

---

## ⚠️ Important Notes

1. **Sandbox Mode**: Use `-s workspace-write` for builds to allow file modifications
2. **Approval Policy**: Use `-a on-failure` for automation with safety
3. **Working Directory**: Use `-C /path/to/project` to specify project root
4. **Web Search**: Enable with `--search` for documentation lookups

---

## 🚀 Recommended Workflow

For your "update" command automation:

```bash
#!/bin/bash
# scripts/codex-workflow.sh

echo "🤖 Starting Codex-driven update workflow..."

codex \
  -s workspace-write \
  -a on-failure \
  -C /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit \
  "
Execute the following workflow:

1. **Content Generation**
   - Read data/themes/current_theme.json
   - Generate new cards/leaders/crises using Python tools
   - Create placeholder assets using gen_images.py and gen_audio.py

2. **Unity Build**
   - Build WebGL project using Unity 6000.2.6f2
   - Verify build artifacts in unity/WebGLBuild/

3. **Git Operations**
   - Stage all generated content and build artifacts
   - Create descriptive commit message
   - Push to codex-cli branch

4. **Verification**
   - Check build logs for errors
   - Verify Git LFS tracked files
   - Confirm successful push
"
```

---

## 📚 Resources

- **Codex CLI Docs**: (check official documentation)
- **Model Context Protocol**: For MCP server integration
- **Configuration**: `~/.codex/config.toml`

---

## ✅ Next Actions

1. **Test Codex with your project**: `codex "show me the project structure"`
2. **Update workflow script**: Integrate Codex into `scripts/codex-workflow.sh`
3. **Configure approval policy**: Set safe defaults in `~/.codex/config.toml`
4. **Run first automated build**: `codex "build Unity WebGL project"`

**Codex is ready! Start automating! 🚀**
