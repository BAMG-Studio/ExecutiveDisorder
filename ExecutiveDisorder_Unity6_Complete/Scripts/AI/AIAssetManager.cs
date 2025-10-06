using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.AI
{
    /// <summary>
    /// Manages AI-generated assets and integrates with various AI services
    /// for procedural content generation in Executive Disorder.
    /// </summary>
    public class AIAssetManager : MonoBehaviour
    {
        [Header("AI Service Configuration")]
        [SerializeField] private string openAIApiKey = "";
        [SerializeField] private string stabilityAPIKey = "";
        [SerializeField] private string elevenLabsAPIKey = "";
        
        [Header("Asset Generation Settings")]
        [SerializeField] private bool useAIForCardGeneration = true;
        [SerializeField] private bool useAIForDialogue = true;
        [SerializeField] private bool useAIForArt = false;
        [SerializeField] private bool useAIForAudio = false;
        
        [Header("Caching")]
        [SerializeField] private bool enableCaching = true;
        [SerializeField] private string cachePath = "Assets/Resources/AI_Cache/";
        
        // Singleton instance
        private static AIAssetManager _instance;
        public static AIAssetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AIAssetManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("AIAssetManager");
                        _instance = go.AddComponent<AIAssetManager>();
                    }
                }
                return _instance;
            }
        }
        
        // Cache for generated content
        private Dictionary<string, Sprite> _spriteCache = new Dictionary<string, Sprite>();
        private Dictionary<string, AudioClip> _audioCache = new Dictionary<string, AudioClip>();
        private Dictionary<string, string> _textCache = new Dictionary<string, string>();
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            LoadApiKeysFromEnvironment();
            InitializeCache();
        }
        
        #region API Key Management
        
        /// <summary>
        /// Load API keys from environment variables or PlayerPrefs
        /// </summary>
        private void LoadApiKeysFromEnvironment()
        {
            if (string.IsNullOrEmpty(openAIApiKey))
                openAIApiKey = System.Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? PlayerPrefs.GetString("OPENAI_API_KEY", "");
            
            if (string.IsNullOrEmpty(stabilityAPIKey))
                stabilityAPIKey = System.Environment.GetEnvironmentVariable("STABILITY_API_KEY") ?? PlayerPrefs.GetString("STABILITY_API_KEY", "");
            
            if (string.IsNullOrEmpty(elevenLabsAPIKey))
                elevenLabsAPIKey = System.Environment.GetEnvironmentVariable("ELEVENLABS_API_KEY") ?? PlayerPrefs.GetString("ELEVENLABS_API_KEY", "");
        }
        
        /// <summary>
        /// Set API key for a specific service
        /// </summary>
        public void SetAPIKey(string service, string key)
        {
            switch (service.ToLower())
            {
                case "openai":
                    openAIApiKey = key;
                    PlayerPrefs.SetString("OPENAI_API_KEY", key);
                    break;
                case "stability":
                    stabilityAPIKey = key;
                    PlayerPrefs.SetString("STABILITY_API_KEY", key);
                    break;
                case "elevenlabs":
                    elevenLabsAPIKey = key;
                    PlayerPrefs.SetString("ELEVENLABS_API_KEY", key);
                    break;
            }
            PlayerPrefs.Save();
        }
        
        #endregion
        
        #region Cache Management
        
        private void InitializeCache()
        {
            if (enableCaching)
            {
                #if UNITY_EDITOR
                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }
                #endif
                
                LoadCachedAssets();
            }
        }
        
        private void LoadCachedAssets()
        {
            // Load previously generated assets from cache
            // This would typically load from Resources or a persistent data path
            Debug.Log("Loading cached AI assets...");
        }
        
        public void ClearCache()
        {
            _spriteCache.Clear();
            _audioCache.Clear();
            _textCache.Clear();
            
            #if UNITY_EDITOR
            if (System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.Delete(cachePath, true);
                System.IO.Directory.CreateDirectory(cachePath);
            }
            #endif
            
            Debug.Log("AI asset cache cleared.");
        }
        
        #endregion
        
        #region Text Generation (OpenAI GPT)
        
        /// <summary>
        /// Generate card text using OpenAI GPT
        /// </summary>
        public void GenerateCardText(string prompt, System.Action<string> onComplete, System.Action<string> onError = null)
        {
            if (!useAIForCardGeneration)
            {
                onError?.Invoke("AI card generation is disabled");
                return;
            }
            
            // Check cache first
            if (enableCaching && _textCache.ContainsKey(prompt))
            {
                onComplete?.Invoke(_textCache[prompt]);
                return;
            }
            
            StartCoroutine(GenerateTextCoroutine(prompt, onComplete, onError));
        }
        
        private System.Collections.IEnumerator GenerateTextCoroutine(string prompt, System.Action<string> onComplete, System.Action<string> onError)
        {
            if (string.IsNullOrEmpty(openAIApiKey))
            {
                onError?.Invoke("OpenAI API key not set");
                yield break;
            }
            
            // Construct the request
            string apiUrl = "https://api.openai.com/v1/chat/completions";
            
            var requestData = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "You are a creative writer for a political simulation game called Executive Disorder. Generate engaging decision cards with consequences." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 500,
                temperature = 0.8
            };
            
            string jsonData = JsonUtility.ToJson(requestData);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            
            using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(apiUrl, "POST"))
            {
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + openAIApiKey);
                
                yield return request.SendWebRequest();
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    string responseText = request.downloadHandler.text;
                    // Parse the response (simplified - you'd want proper JSON parsing)
                    string generatedText = ParseOpenAIResponse(responseText);
                    
                    // Cache the result
                    if (enableCaching)
                    {
                        _textCache[prompt] = generatedText;
                    }
                    
                    onComplete?.Invoke(generatedText);
                }
                else
                {
                    onError?.Invoke($"API Error: {request.error}");
                }
            }
        }
        
        private string ParseOpenAIResponse(string json)
        {
            // Simple parsing - in production, use proper JSON library like Newtonsoft.Json
            try
            {
                int contentStart = json.IndexOf("\"content\":\"") + 11;
                int contentEnd = json.IndexOf("\"", contentStart);
                if (contentStart > 11 && contentEnd > contentStart)
                {
                    return json.Substring(contentStart, contentEnd - contentStart);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to parse OpenAI response: {e.Message}");
            }
            return "";
        }
        
        #endregion
        
        #region Image Generation (Stability AI)
        
        /// <summary>
        /// Generate character portrait or card art using Stability AI
        /// </summary>
        public void GenerateImage(string prompt, System.Action<Sprite> onComplete, System.Action<string> onError = null)
        {
            if (!useAIForArt)
            {
                onError?.Invoke("AI art generation is disabled");
                return;
            }
            
            // Check cache
            if (enableCaching && _spriteCache.ContainsKey(prompt))
            {
                onComplete?.Invoke(_spriteCache[prompt]);
                return;
            }
            
            StartCoroutine(GenerateImageCoroutine(prompt, onComplete, onError));
        }
        
        private System.Collections.IEnumerator GenerateImageCoroutine(string prompt, System.Action<Sprite> onComplete, System.Action<string> onError)
        {
            if (string.IsNullOrEmpty(stabilityAPIKey))
            {
                onError?.Invoke("Stability API key not set");
                yield break;
            }
            
            string apiUrl = "https://api.stability.ai/v1/generation/stable-diffusion-xl-1024-v1-0/text-to-image";
            
            var requestData = new
            {
                text_prompts = new[] { new { text = prompt, weight = 1 } },
                cfg_scale = 7,
                height = 1024,
                width = 1024,
                samples = 1,
                steps = 30
            };
            
            string jsonData = JsonUtility.ToJson(requestData);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            
            using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(apiUrl, "POST"))
            {
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + stabilityAPIKey);
                
                yield return request.SendWebRequest();
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    // Parse response and create sprite from image data
                    Sprite sprite = ParseImageResponse(request.downloadHandler.data);
                    
                    if (sprite != null)
                    {
                        if (enableCaching)
                        {
                            _spriteCache[prompt] = sprite;
                        }
                        onComplete?.Invoke(sprite);
                    }
                    else
                    {
                        onError?.Invoke("Failed to create sprite from image data");
                    }
                }
                else
                {
                    onError?.Invoke($"API Error: {request.error}");
                }
            }
        }
        
        private Sprite ParseImageResponse(byte[] data)
        {
            // In production, you'd need to parse the JSON response and extract the base64 image
            // This is a simplified placeholder
            try
            {
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(data))
                {
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to parse image response: {e.Message}");
            }
            return null;
        }
        
        #endregion
        
        #region Audio Generation (ElevenLabs)
        
        /// <summary>
        /// Generate voiceover audio using ElevenLabs
        /// </summary>
        public void GenerateAudio(string text, string voiceId, System.Action<AudioClip> onComplete, System.Action<string> onError = null)
        {
            if (!useAIForAudio)
            {
                onError?.Invoke("AI audio generation is disabled");
                return;
            }
            
            string cacheKey = $"{voiceId}_{text}";
            if (enableCaching && _audioCache.ContainsKey(cacheKey))
            {
                onComplete?.Invoke(_audioCache[cacheKey]);
                return;
            }
            
            StartCoroutine(GenerateAudioCoroutine(text, voiceId, onComplete, onError));
        }
        
        private System.Collections.IEnumerator GenerateAudioCoroutine(string text, string voiceId, System.Action<AudioClip> onComplete, System.Action<string> onError)
        {
            if (string.IsNullOrEmpty(elevenLabsAPIKey))
            {
                onError?.Invoke("ElevenLabs API key not set");
                yield break;
            }
            
            string apiUrl = $"https://api.elevenlabs.io/v1/text-to-speech/{voiceId}";
            
            var requestData = new
            {
                text = text,
                model_id = "eleven_monolingual_v1",
                voice_settings = new
                {
                    stability = 0.5f,
                    similarity_boost = 0.75f
                }
            };
            
            string jsonData = JsonUtility.ToJson(requestData);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            
            using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(apiUrl, "POST"))
            {
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("xi-api-key", elevenLabsAPIKey);
                
                yield return request.SendWebRequest();
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    AudioClip clip = ParseAudioResponse(request.downloadHandler.data);
                    
                    if (clip != null)
                    {
                        string cacheKey = $"{voiceId}_{text}";
                        if (enableCaching)
                        {
                            _audioCache[cacheKey] = clip;
                        }
                        onComplete?.Invoke(clip);
                    }
                    else
                    {
                        onError?.Invoke("Failed to create audio clip from data");
                    }
                }
                else
                {
                    onError?.Invoke($"API Error: {request.error}");
                }
            }
        }
        
        private AudioClip ParseAudioResponse(byte[] data)
        {
            // Convert MP3/audio data to AudioClip
            // This requires additional libraries like NAudio or similar
            // Placeholder implementation
            Debug.LogWarning("Audio parsing not fully implemented - requires additional audio codec support");
            return null;
        }
        
        #endregion
        
        #region Utility Methods
        
        /// <summary>
        /// Check if AI services are properly configured
        /// </summary>
        public bool IsConfigured()
        {
            bool hasKeys = !string.IsNullOrEmpty(openAIApiKey) || 
                          !string.IsNullOrEmpty(stabilityAPIKey) || 
                          !string.IsNullOrEmpty(elevenLabsAPIKey);
            return hasKeys;
        }
        
        /// <summary>
        /// Get status of AI services
        /// </summary>
        public string GetServiceStatus()
        {
            return $"OpenAI: {(!string.IsNullOrEmpty(openAIApiKey) ? "✓" : "✗")}\n" +
                   $"Stability AI: {(!string.IsNullOrEmpty(stabilityAPIKey) ? "✓" : "✗")}\n" +
                   $"ElevenLabs: {(!string.IsNullOrEmpty(elevenLabsAPIKey) ? "✓" : "✗")}\n" +
                   $"Cache: {_textCache.Count} texts, {_spriteCache.Count} sprites, {_audioCache.Count} audio clips";
        }
        
        #endregion
    }
}
