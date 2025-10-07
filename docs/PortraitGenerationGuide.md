# Character Portrait Generation Guide

## 🎨 Generate 8 AI Portraits

You need portraits for these 8 characters:
1. Rex Scaleston III - The Iguana King
2. Donald J. Executive - The 45th
3. POTUS-9000 - The AI President
4. Alexandria Sanders-Warren - The Progressive
5. Richard M. Moneybags III - The Corporate Lobbyist
6. General James 'Ironside' Steel - The Military Hawk
7. Diana Newsworthy - The Media Mogul
8. Johnny Q. Public - The Populist

---

## Method 1: Automated Script (Recommended)

### Prerequisites
- Python 3.x installed
- Flask backend running on Replit
- OpenAI API key configured in backend

### Steps

1. **Update API URL**
```bash
# Edit: scripts/generate_character_portraits.py
# Line 12: API_BASE_URL = "https://your-replit-url.repl.co"
```

2. **Run the script**
```bash
# Windows
cd scripts
generate_portraits.bat

# Mac/Linux
cd scripts
python3 generate_character_portraits.py
```

3. **Wait for generation**
- Each portrait takes 10-30 seconds
- Total time: ~5-10 minutes
- Script waits 5 seconds between requests

4. **Import to Unity**
- Generated images saved to: `unity/Assets/Art/Characters/Generated/`
- In Unity: Right-click folder → Import
- Select all images → Inspector → Texture Type: "Sprite (2D and UI)"
- Click Apply

5. **Assign to characters**
- Open each character ScriptableObject
- Drag portrait sprite to "Portrait" field
- Disable "Use AI Portrait" checkbox

---

## Method 2: Manual Generation (Unity)

### Prerequisites
- AIBackendTester.cs setup in Unity
- Flask backend running

### Steps

1. **Create test scene**
```
File → New Scene → "PortraitGenerator"
Add Canvas
Add AIBackendTester component
Add UI buttons and image display
```

2. **Generate each portrait**
```
For each character:
1. Click "Test Portrait" button
2. Wait 10-30 seconds
3. Right-click generated texture in Project
4. Export as PNG
5. Save to Assets/Art/Characters/
6. Assign to character ScriptableObject
```

---

## Method 3: Direct API Calls (cURL)

### For each character, run:

```bash
# Rex Scaleston III
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Rex Scaleston III",
    "description": "Professional political portrait of an iguana wearing a business suit and red tie, presidential, dignified, photorealistic, studio lighting, official government portrait style, 4K quality"
  }' \
  --output RexScalestonPortrait.png

# Donald J. Executive
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Donald J. Executive",
    "description": "Professional portrait of a confident business executive in expensive navy suit, orange tan, distinctive blonde hairstyle, presidential bearing, studio lighting, official portrait style, 4K quality"
  }' \
  --output DonaldExecutivePortrait.png

# POTUS-9000
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "POTUS-9000",
    "description": "Futuristic AI robot president, sleek metallic design with chrome finish, glowing blue eyes, wearing presidential suit and tie, professional portrait, sci-fi, high-tech, studio lighting, 4K quality"
  }' \
  --output POTUS9000Portrait.png

# Alexandria Sanders-Warren
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Alexandria Sanders-Warren",
    "description": "Young progressive female politician, professional portrait, confident smile, modern business attire, diverse, inspiring, studio lighting, official government portrait style, 4K quality"
  }' \
  --output AlexandriaPortrait.png

# Richard M. Moneybags III
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Richard M. Moneybags III",
    "description": "Wealthy aristocratic businessman, expensive three-piece suit, monocle, smug expression, old money aesthetic, oil painting style portrait, luxurious, studio lighting, 4K quality"
  }' \
  --output RichardMoneybagsPortrait.png

# General James Ironside Steel
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "General James Ironside Steel",
    "description": "Stern military general in dress uniform with medals, crew cut, strong jaw, authoritative presence, professional military portrait, American flag background, studio lighting, 4K quality"
  }' \
  --output GeneralSteelPortrait.png

# Diana Newsworthy
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Diana Newsworthy",
    "description": "Sophisticated female media executive, power suit, confident pose, studio lighting, professional glamour shot, influential, modern, official portrait style, 4K quality"
  }' \
  --output DianaNewsworthyPortrait.png

# Johnny Q. Public
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{
    "character_name": "Johnny Q. Public",
    "description": "Average middle-aged man in casual business attire, friendly smile, relatable, working class hero, approachable, everyman, studio lighting, official portrait style, 4K quality"
  }' \
  --output JohnnyPublicPortrait.png
```

---

## Method 4: Web Interface (Swagger UI)

1. **Open Swagger UI**
```
https://your-replit-url.repl.co/api-docs
```

2. **Find endpoint**
```
POST /api/generate-character-portrait
```

3. **For each character**:
- Click "Try it out"
- Fill in character_name and description
- Click "Execute"
- Download generated image
- Save to Unity project

---

## Portrait Specifications

### All Portraits Should Have:
- **Resolution**: 1024x1024 (DALL-E 3 default)
- **Style**: Professional political portrait
- **Lighting**: Studio lighting, official government portrait style
- **Quality**: 4K, photorealistic (except POTUS-9000 which is sci-fi)
- **Format**: PNG with transparency (if possible)

### Character-Specific Details:

**Rex Scaleston III**
- Subject: Iguana in business suit
- Style: Dignified, presidential
- Key features: Red tie, reptilian features

**Donald J. Executive**
- Subject: Business executive
- Style: Confident, powerful
- Key features: Orange tan, distinctive hair

**POTUS-9000**
- Subject: AI robot
- Style: Futuristic, sci-fi
- Key features: Metallic, glowing blue eyes

**Alexandria Sanders-Warren**
- Subject: Young female politician
- Style: Modern, inspiring
- Key features: Confident smile, progressive

**Richard M. Moneybags III**
- Subject: Wealthy businessman
- Style: Old money, aristocratic
- Key features: Monocle, three-piece suit

**General James Ironside Steel**
- Subject: Military general
- Style: Authoritative, stern
- Key features: Dress uniform, medals

**Diana Newsworthy**
- Subject: Female media executive
- Style: Sophisticated, powerful
- Key features: Power suit, glamorous

**Johnny Q. Public**
- Subject: Average man
- Style: Relatable, friendly
- Key features: Casual attire, approachable

---

## Unity Import Settings

After generating portraits:

1. **Import to Unity**
```
Copy PNG files to: unity/Assets/Art/Characters/
```

2. **Configure import settings**
```
Select all portraits in Unity
Inspector:
- Texture Type: Sprite (2D and UI)
- Sprite Mode: Single
- Pixels Per Unit: 100
- Filter Mode: Bilinear
- Max Size: 2048
- Compression: None (or High Quality)
Click Apply
```

3. **Assign to characters**
```
For each character ScriptableObject:
1. Open in Inspector
2. Drag portrait sprite to "Portrait" field
3. Uncheck "Use AI Portrait"
4. Save
```

---

## Troubleshooting

### "Backend not responding"
- Verify Flask backend is running
- Check API URL is correct
- Test with health endpoint first

### "Generation takes too long"
- DALL-E 3 can take 10-30 seconds per image
- Be patient, don't cancel requests
- Check backend logs for errors

### "Image quality is poor"
- Verify prompt includes "4K quality"
- Check DALL-E 3 settings in backend
- Try regenerating with adjusted prompt

### "Can't import to Unity"
- Verify PNG format
- Check file isn't corrupted
- Try re-downloading from backend

---

## Cost Estimate

**DALL-E 3 Pricing**: ~$0.040 per image (1024x1024)

**Total cost for 8 portraits**: ~$0.32

**Recommendation**: Generate once, save permanently, don't regenerate

---

## Next Steps After Generation

1. ✅ All 8 portraits generated
2. ✅ Imported to Unity
3. ✅ Assigned to character ScriptableObjects
4. ✅ Test in Character Selection scene
5. ✅ Verify CircleMask.png applied correctly
6. ✅ Check portrait quality in-game
7. ✅ Save portraits to version control

---

## Alternative: Use Placeholder Images

If AI generation isn't working, you can use existing portraits:

```
Dr. Evelyn Technocrat → TheTechnocratPortrait.png (already have)
Senator Marcus Tradition → MadamCashPortrait.png (already have)

For others, use:
- placeholderpresi.png
- Ironfist presido.png
- Or create simple colored circles with initials
```

---

## Success Checklist

- [ ] Flask backend running
- [ ] API URL configured
- [ ] Python script updated (if using Method 1)
- [ ] 8 portraits generated
- [ ] Portraits imported to Unity
- [ ] Import settings configured
- [ ] Portraits assigned to characters
- [ ] Character Selection scene tested
- [ ] All portraits display correctly
- [ ] CircleMask applied properly

**Estimated Time**: 15-30 minutes (mostly waiting for AI generation)
