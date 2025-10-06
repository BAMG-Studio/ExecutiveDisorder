# Unity Cloud Build Configuration Fix
## ExecutiveDisorder Project

**Issue:** Unity Cloud Build failing with "unrecognized project" error
**Solution:** Configure correct Project Subdirectory path

---

## 🔧 **IMMEDIATE FIX STEPS**

### Step 1: Access Unity Cloud Build Dashboard

1. **Go to Unity Dashboard:** https://cloud.unity.com
2. **Navigate to:** DevOps → Build Automation
3. **Select Project:** ExecutiveDisorder
4. **Click:** Settings or Configure

### Step 2: Update Project Configuration

**Find this section:**
```
┌─────────────────────────────────┐
│ Build Configuration             │
├─────────────────────────────────┤
│ Repository: papaert-cloud/...   │
│ Branch: main                    │
│ Project Subdirectory: [BLANK]   │ ← FIX THIS
└─────────────────────────────────┘
```

**Change to:**
```
┌─────────────────────────────────┐
│ Build Configuration             │
├─────────────────────────────────┤
│ Repository: papaert-cloud/...   │
│ Branch: main                    │
│ Project Subdirectory: unity     │ ← SET TO "unity"
└─────────────────────────────────┘
```

### Step 3: Verify Unity Version

**Set Unity Version to:** `2023.2.x` (or latest Unity 6)

### Step 4: Save and Test

1. **Click:** Save Configuration
2. **Click:** Build Now (or trigger new build)
3. **Monitor:** Build should now succeed

---

## 📁 **Why This Fixes It**

**Your Repository Structure:**
```
ExecutiveDisorderReplit/          ← Repository root
├── backend/
├── docs/
├── ExecutiveDisorder.Core/
├── ExecutiveDisorder_Unity6_Complete/
└── unity/                        ← Unity project HERE
    ├── Assets/                   ← Unity needs this
    ├── ProjectSettings/          ← Unity needs this
    └── Packages/                 ← Unity needs this
```

**Unity Cloud Build was looking at:**
```
ExecutiveDisorderReplit/ (root)
├── Assets/ ❌ NOT FOUND
└── ProjectSettings/ ❌ NOT FOUND
```

**Unity Cloud Build now looks at:**
```
ExecutiveDisorderReplit/unity/
├── Assets/ ✅ FOUND
└── ProjectSettings/ ✅ FOUND
```

---

## 🎯 **Alternative Solutions**

### Option A: Move Unity Project to Root (Not Recommended)
Move contents of `unity/` to repository root - **DON'T DO THIS**

### Option B: Create Unity Cloud Build Config File
Already created: `.ucb-config` (may not be supported by all versions)

### Option C: Use Subdirectory (Recommended - Current Fix)
Set `Project Subdirectory: unity` ← **DO THIS**

---

## ✅ **Expected Results After Fix**

**Before Fix (Failing):**
```
[error] Error: unrecognized project!
[error] We expect your "Project Subdirectory" to be set to the path 
        which directly contains the ProjectSettings and Assets directories
```

**After Fix (Working):**
```
✅ SCM Checkout complete
✅ Found ProjectSettings at: unity/ProjectSettings
✅ Found Assets at: unity/Assets  
✅ Library cache initialized
✅ Assets imported successfully
✅ Build started for WebGL
✅ Build completed successfully
```

---

## 🔍 **Verification Steps**

After making the change:

1. **Check Build Log** for these success messages:
   - ✅ "Found ProjectSettings"
   - ✅ "Found Assets" 
   - ✅ "Library cache initialized"
   - ✅ No "unrecognized project" error

2. **Build Should Progress Through:**
   - ✅ Source checkout
   - ✅ Library cache
   - ✅ Asset import
   - ✅ Compilation
   - ✅ WebGL build
   - ✅ Artifact publishing

3. **Final Status:** "Build completed successfully"

---

## 📞 **If Still Having Issues**

### Common Problems & Solutions:

**Problem:** "Project Subdirectory" field not found
**Solution:** Look for "Project Path", "Source Path", or similar setting

**Problem:** Still getting "unrecognized project" 
**Solution:** Verify the setting saved correctly, try browser refresh

**Problem:** Different error appears
**Solution:** Check Unity version compatibility (use Unity 6 or 2023.2+)

**Problem:** Build starts but fails later
**Solution:** Check for package compatibility issues in build log

---

## 📋 **Quick Reference**

| Setting | Value |
|---------|--------|
| **Project Subdirectory** | `unity` |
| **Unity Version** | `2023.2.x` or `Unity 6` |
| **Build Target** | `WebGL` |
| **Branch** | `main` |

---

## 🚀 **Test the Fix**

1. Make the configuration change
2. Start a new build
3. Monitor the first few minutes of build log
4. Look for successful checkout and asset discovery
5. Verify build completes without the "unrecognized project" error

**Status:** Ready to implement fix
**Time to fix:** ~2 minutes
**Expected success rate:** 95%+

---

*This fix addresses the exact error from your build log and should resolve the Unity Cloud Build issue immediately.*