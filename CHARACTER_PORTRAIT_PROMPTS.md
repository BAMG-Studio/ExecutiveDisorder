# Character Portrait Prompts - Quick Reference

## 🎨 Copy-Paste Ready Prompts for DALL-E 3

### 1. Rex Scaleston III - The Iguana King
```
Professional political portrait of an iguana wearing a business suit and red tie, presidential, dignified, photorealistic, studio lighting, official government portrait style, 4K quality
```

### 2. Donald J. Executive - The 45th
```
Professional portrait of a confident business executive in expensive navy suit, orange tan, distinctive blonde hairstyle, presidential bearing, studio lighting, official portrait style, 4K quality
```

### 3. POTUS-9000 - The AI President
```
Futuristic AI robot president, sleek metallic design with chrome finish, glowing blue eyes, wearing presidential suit and tie, professional portrait, sci-fi, high-tech, studio lighting, 4K quality
```

### 4. Alexandria Sanders-Warren - The Progressive
```
Young progressive female politician, professional portrait, confident smile, modern business attire, diverse, inspiring, studio lighting, official government portrait style, 4K quality
```

### 5. Richard M. Moneybags III - The Corporate Lobbyist
```
Wealthy aristocratic businessman, expensive three-piece suit, monocle, smug expression, old money aesthetic, oil painting style portrait, luxurious, studio lighting, 4K quality
```

### 6. General James 'Ironside' Steel - The Military Hawk
```
Stern military general in dress uniform with medals, crew cut, strong jaw, authoritative presence, professional military portrait, American flag background, studio lighting, 4K quality
```

### 7. Diana Newsworthy - The Media Mogul
```
Sophisticated female media executive, power suit, confident pose, studio lighting, professional glamour shot, influential, modern, official portrait style, 4K quality
```

### 8. Johnny Q. Public - The Populist
```
Average middle-aged man in casual business attire, friendly smile, relatable, working class hero, approachable, everyman, studio lighting, official portrait style, 4K quality
```

---

## 🚀 Quick Generation Methods

### Option A: Automated Script
```bash
cd scripts
generate_portraits.bat
```

### Option B: Manual via Swagger UI
```
1. Go to: https://your-replit-url.repl.co/api-docs
2. POST /api/generate-character-portrait
3. Copy-paste prompts above
4. Download images
```

### Option C: Direct API Call (Example)
```bash
curl -X POST https://your-replit-url.repl.co/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{"character_name": "Rex Scaleston III", "description": "PASTE_PROMPT_HERE"}' \
  --output portrait.png
```

---

## 📋 Generation Checklist

- [ ] Backend running at: ___________________________
- [ ] OpenAI API key configured
- [ ] Generated Rex Scaleston III
- [ ] Generated Donald J. Executive
- [ ] Generated POTUS-9000
- [ ] Generated Alexandria Sanders-Warren
- [ ] Generated Richard M. Moneybags III
- [ ] Generated General James 'Ironside' Steel
- [ ] Generated Diana Newsworthy
- [ ] Generated Johnny Q. Public
- [ ] All imported to Unity
- [ ] All assigned to characters

**Total Cost**: ~$0.32 (8 images × $0.04)
**Total Time**: ~10-15 minutes
