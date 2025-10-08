# Git LFS Setup Guide for Executive Disorder

## ⚠️ Git LFS Not Currently Installed

Git LFS (Large File Storage) is recommended for managing Unity build artifacts, but is not currently installed on this system.

## 📦 What is Git LFS?

Git LFS is a Git extension that handles large files efficiently by storing file contents on a remote server and keeping only pointers in the Git repository.

### Benefits for Unity Projects:
- ✅ Faster cloning (large files downloaded on-demand)
- ✅ Smaller repository size
- ✅ Better performance with binary files
- ✅ Essential for WebGL builds (`.wasm`, `.data` files)

## 🔧 Installation Options

### Option 1: Windows (Recommended)
1. Download from: https://git-lfs.github.com/
2. Run installer
3. Open Git Bash or WSL
4. Run: `git lfs install`

### Option 2: WSL/Linux
```bash
# Ubuntu/Debian
sudo apt-get install git-lfs

# Or download binary
curl -s https://packagecloud.io/install/repositories/github/git-lfs/script.deb.sh | sudo bash
sudo apt-get install git-lfs
```

### Option 3: Manual (if admin rights unavailable)
```bash
# Download standalone binary
wget https://github.com/git-lfs/git-lfs/releases/download/v3.4.0/git-lfs-linux-amd64-v3.4.0.tar.gz
tar -xzf git-lfs-linux-amd64-v3.4.0.tar.gz
sudo ./git-lfs-linux-amd64-v3.4.0/install.sh
```

## 🚀 Setup (After Installation)

Once Git LFS is installed, run:

```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
bash scripts/setup-git-lfs.sh
```

Or manually:

```bash
# Initialize LFS
git lfs install

# Track large files (already configured in .gitattributes)
git lfs track "*.wasm"
git lfs track "*.data"
git lfs track "*.mp3"
# ... etc

# Add .gitattributes
git add .gitattributes

# Commit
git commit -m "chore: configure Git LFS for Unity artifacts"

# Push
git push origin codex-cli
```

## 📁 Files Currently Configured for LFS

See `.gitattributes` file. Currently tracking:

### Unity Build Artifacts
- `*.wasm` - WebAssembly binaries
- `*.data` - Unity data files
- `*.framework.js` - Unity framework
- `*.unity3d` - Asset bundles

### Audio
- `*.mp3`, `*.wav`, `*.ogg`, `*.aiff`

### Video
- `*.mp4`, `*.mov`, `*.avi`

### Large Textures
- `*.psd`, `*.tga`, `*.exr`, `*.hdr`

### Mobile Builds
- `*.apk`, `*.aab`, `*.ipa`

### Archives
- `*.zip`, `*.7z`, `*.tar.gz`

## 🎯 Current Status

**Git LFS:** ❌ Not installed  
**`.gitattributes`:** ✅ Created (ready for LFS)  
**Setup Script:** ✅ Available (`scripts/setup-git-lfs.sh`)

## ⚙️ Workflow After LFS Installation

1. **Install Git LFS** (see above)
2. **Run setup script:**
   ```bash
   bash scripts/setup-git-lfs.sh
   ```
3. **Verify:**
   ```bash
   git lfs track
   git lfs ls-files
   ```
4. **Commit & push:**
   ```bash
   git add .gitattributes
   git commit -m "chore: enable Git LFS"
   git push origin codex-cli
   ```

## 🚧 Working Without LFS (Current State)

The `.gitattributes` file is configured but **not active** until Git LFS is installed.

**This means:**
- ✅ Configuration is ready for future LFS setup
- ⚠️ Large files will be stored directly in Git (not ideal)
- ⚠️ Repository size may grow quickly with WebGL builds
- ⚠️ Clone times may increase

**Recommendation:**
- Install Git LFS before committing large WebGL builds
- Or exclude build artifacts from Git until LFS is ready

## 📊 File Size Expectations

Without LFS:
- WebGL build: ~50-200 MB
- With assets: ~500 MB - 2 GB
- Multiple builds: Repository becomes very large

With LFS:
- Repository stays small (~100 MB)
- Large files download on-demand
- Fast clones and pulls

## 🔍 Check If LFS Is Working

After installation:

```bash
# Should show LFS version
git lfs version

# Should show tracked patterns
git lfs track

# Should show LFS-tracked files
git lfs ls-files
```

## 📝 Next Steps

1. **Install Git LFS** (when possible)
2. **Run `bash scripts/setup-git-lfs.sh`**
3. **Verify with `git lfs track`**
4. **Commit and push**
5. **Build Unity WebGL** (files will be tracked by LFS)

## 🆘 Troubleshooting

### "git-lfs command not found"
→ Git LFS not installed. See installation section above.

### "filter 'lfs' is not available"
→ Run `git lfs install` in the repository.

### Files not tracked by LFS
→ Check `.gitattributes` and run `git lfs track`.

### LFS quota exceeded (GitHub)
→ GitHub free tier: 1 GB storage, 1 GB/month bandwidth.
→ Consider GitHub LFS data packs or alternative hosting.

## 🔗 Resources

- Git LFS: https://git-lfs.github.com/
- GitHub LFS: https://docs.github.com/en/repositories/working-with-files/managing-large-files
- Unity Best Practices: https://unity.com/how-to/version-control-systems

---

**Status:** `.gitattributes` created ✅  
**Next:** Install Git LFS to activate tracking
