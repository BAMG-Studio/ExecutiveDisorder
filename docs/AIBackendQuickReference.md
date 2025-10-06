# AI Backend Quick Reference Guide

## 🚀 Testing Your AI Backend

### Access API Documentation
```
https://[your-replit-url]/api-docs
```
Interactive Swagger UI for testing all endpoints directly in your browser.

---

## 📋 Available Endpoints

### 🎨 Visual AI (DALL-E 3)

#### 1. Generate Character Portrait
```http
POST /api/generate-character-portrait
Content-Type: application/json

{
  "character_name": "Rex Scaleston III",
  "description": "Professional portrait of an iguana in a business suit"
}
```

#### 2. Generate Decision Card Art
```http
POST /api/generate-decision-card
Content-Type: application/json

{
  "card_title": "Nuclear Submarine Crisis",
  "card_description": "A submarine has gone missing"
}
```

#### 3. Generate UI Elements
```http
POST /api/generate-ui-element
Content-Type: application/json

{
  "element_type": "button",
  "style": "political campaign button, red and blue"
}
```

---

### 🧠 Content AI (GPT-5)

#### 4. Generate Decision Card Content
```http
POST /api/generate-card-content
Content-Type: application/json

{
  "theme": "economic crisis",
  "difficulty": "hard",
  "category": "crisis"
}
```

#### 5. Generate Game Character
```http
POST /api/generate-character
Content-Type: application/json

{
  "character_type": "populist",
  "traits": ["charismatic", "controversial"]
}
```

#### 6. Generate Ending Scenario
```http
POST /api/generate-ending
Content-Type: application/json

{
  "ending_type": "victory",
  "player_stats": {
    "popularity": 80,
    "stability": 60,
    "media": 70,
    "economy": 75
  }
}
```

#### 7. Game Balance Analysis
```http
POST /api/analyze-balance
Content-Type: application/json

{
  "cards": [...],
  "difficulty_target": "medium"
}
```

#### 8. Player Hints
```http
POST /api/generate-hint
Content-Type: application/json

{
  "current_situation": "Low popularity, high stability",
  "available_cards": [...]
}
```

---

### 🎙️ Audio AI (ElevenLabs)

#### 9. Character Voice
```http
POST /api/generate-character-voice
Content-Type: application/json

{
  "text": "Welcome to my administration",
  "voice_id": "politician_male",
  "character_name": "Rex Scaleston III"
}
```

#### 10. Decision Narration
```http
POST /api/generate-narration
Content-Type: application/json

{
  "text": "The nuclear submarine has been located",
  "voice_id": "narrator_professional"
}
```

#### 11. Media Headlines
```http
POST /api/generate-headline-voice
Content-Type: application/json

{
  "headline": "President's Approval Ratings Plummet!",
  "voice_id": "news_reporter"
}
```

#### 12. Voice Library
```http
GET /api/voices
```
Returns list of available ElevenLabs voices.

---

## 🔧 Unity Integration Examples

### Example 1: Generate Character Portrait
```csharp
IEnumerator GeneratePortrait(string characterName)
{
    string url = "https://your-replit-url/api/generate-character-portrait";
    string json = JsonUtility.ToJson(new {
        character_name = characterName,
        description = "Professional political portrait"
    });
    
    var request = new UnityWebRequest(url, "POST");
    request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
    request.downloadHandler = new DownloadHandlerTexture();
    request.SetRequestHeader("Content-Type", "application/json");
    
    yield return request.SendWebRequest();
    
    if (request.result == UnityWebRequest.Result.Success)
    {
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        // Use texture for character portrait
    }
}
```

### Example 2: Generate Character Voice
```csharp
IEnumerator GenerateVoice(string dialogue)
{
    string url = $"https://your-replit-url/api/generate-character-voice?text={dialogue}&voice_id=politician_male";
    
    var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
    yield return request.SendWebRequest();
    
    if (request.result == UnityWebRequest.Result.Success)
    {
        AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
        AudioManager.Instance.PlayClip(clip);
    }
}
```

### Example 3: Generate Card Content
```csharp
[Serializable]
public class CardContentRequest
{
    public string theme;
    public string difficulty;
    public string category;
}

[Serializable]
public class CardContentResponse
{
    public string title;
    public string description;
    public Choice[] choices;
}

IEnumerator GenerateCard()
{
    string url = "https://your-replit-url/api/generate-card-content";
    var requestData = new CardContentRequest {
        theme = "economic crisis",
        difficulty = "medium",
        category = "crisis"
    };
    
    string json = JsonUtility.ToJson(requestData);
    var request = UnityWebRequest.Post(url, json, "application/json");
    
    yield return request.SendWebRequest();
    
    if (request.result == UnityWebRequest.Result.Success)
    {
        CardContentResponse response = JsonUtility.FromJson<CardContentResponse>(request.downloadHandler.text);
        // Use response to create DecisionCard
    }
}
```

---

## 🎯 Quick Test Workflow

### Step 1: Test Backend is Running
```bash
curl https://your-replit-url/health
# Should return: {"status": "healthy"}
```

### Step 2: Test Character Portrait Generation
```bash
curl -X POST https://your-replit-url/api/generate-character-portrait \
  -H "Content-Type: application/json" \
  -d '{"character_name": "Test Character", "description": "Political leader portrait"}'
```

### Step 3: Test Voice Generation
```bash
curl "https://your-replit-url/api/generate-character-voice?text=Hello&voice_id=politician_male" \
  --output test_voice.mp3
```

### Step 4: View Available Voices
```bash
curl https://your-replit-url/api/voices
```

---

## 🔑 API Keys Required

Your Flask backend needs these environment variables:
```bash
OPENAI_API_KEY=sk-...
ELEVENLABS_API_KEY=...
```

Set them in Replit Secrets or `.env` file.

---

## 💡 Best Practices

### Caching
- Cache AI-generated portraits to avoid repeated API calls
- Store generated content in Resources or Addressables

### Error Handling
```csharp
if (request.result != UnityWebRequest.Result.Success)
{
    Debug.LogError($"API Error: {request.error}");
    // Fallback to default content
}
```

### Rate Limiting
- Implement delays between API calls
- Use coroutines with `yield return new WaitForSeconds(1f)`

### Async Loading
- Show loading indicators during AI generation
- Generate content during loading screens

---

## 📊 Cost Optimization

### DALL-E 3
- $0.040 per image (1024x1024)
- Generate once, cache forever

### GPT-5
- ~$0.01 per 1K tokens
- Batch requests when possible

### ElevenLabs
- Free tier: 10,000 characters/month
- Pro: $5/month for 30,000 characters

### Recommendations
1. Pre-generate core content during development
2. Use AI for dynamic/procedural content only
3. Cache everything possible
4. Consider generating content server-side during build

---

## 🐛 Troubleshooting

### Issue: "Connection refused"
- Check Replit is running
- Verify URL is correct
- Check firewall settings

### Issue: "API key invalid"
- Verify keys in Replit Secrets
- Check key format (no extra spaces)
- Ensure keys have proper permissions

### Issue: "Timeout"
- AI generation can take 5-30 seconds
- Increase Unity timeout: `request.timeout = 60`
- Show loading indicator to user

### Issue: "Invalid JSON"
- Check request format matches API spec
- Use `JsonUtility.ToJson()` for serialization
- Validate JSON with online tools

---

## 📚 Additional Resources

- [OpenAI API Docs](https://platform.openai.com/docs)
- [ElevenLabs API Docs](https://elevenlabs.io/docs)
- [Unity Networking Docs](https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html)
- [Your Flask Backend Swagger UI](https://your-replit-url/api-docs)

---

## ✅ Integration Checklist

- [ ] Backend running on Replit
- [ ] API keys configured
- [ ] Test all 12 endpoints via Swagger UI
- [ ] Implement Unity API wrapper class
- [ ] Add error handling and fallbacks
- [ ] Implement caching system
- [ ] Test character portrait generation
- [ ] Test voice generation
- [ ] Test card content generation
- [ ] Optimize for production (caching, rate limiting)
