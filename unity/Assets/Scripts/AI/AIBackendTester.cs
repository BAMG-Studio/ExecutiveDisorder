using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AIBackendTester : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string apiBaseUrl = "https://your-replit-url.repl.co";
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Image testImage;
    [SerializeField] private Button testPortraitButton;
    [SerializeField] private Button testVoiceButton;
    [SerializeField] private Button testHealthButton;
    
    [Header("Test Results")]
    [SerializeField] private TextMeshProUGUI resultsText;
    
    private void Start()
    {
        if (testHealthButton) testHealthButton.onClick.AddListener(() => StartCoroutine(TestHealth()));
        if (testPortraitButton) testPortraitButton.onClick.AddListener(() => StartCoroutine(TestPortraitGeneration()));
        if (testVoiceButton) testVoiceButton.onClick.AddListener(() => StartCoroutine(TestVoiceGeneration()));
        
        UpdateStatus("AI Backend Tester Ready");
    }
    
    public IEnumerator TestHealth()
    {
        UpdateStatus("Testing backend health...");
        
        var request = UnityWebRequest.Get($"{apiBaseUrl}/health");
        request.timeout = 10;
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            UpdateStatus("✅ Backend is healthy!");
            LogResult($"Health Check: SUCCESS\n{request.downloadHandler.text}");
        }
        else
        {
            UpdateStatus($"❌ Backend error: {request.error}");
            LogResult($"Health Check: FAILED\n{request.error}");
        }
    }
    
    public IEnumerator TestPortraitGeneration()
    {
        UpdateStatus("Generating character portrait...");
        
        string json = JsonUtility.ToJson(new PortraitRequest
        {
            character_name = "Test Character",
            description = "Professional portrait of a political leader in a business suit"
        });
        
        var request = new UnityWebRequest($"{apiBaseUrl}/api/generate-character-portrait", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerTexture();
        request.SetRequestHeader("Content-Type", "application/json");
        request.timeout = 60;
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (testImage)
            {
                testImage.sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );
            }
            UpdateStatus("✅ Portrait generated successfully!");
            LogResult($"Portrait Generation: SUCCESS\nSize: {texture.width}x{texture.height}");
        }
        else
        {
            UpdateStatus($"❌ Portrait generation failed: {request.error}");
            LogResult($"Portrait Generation: FAILED\n{request.error}");
        }
    }
    
    public IEnumerator TestVoiceGeneration()
    {
        UpdateStatus("Generating character voice...");
        
        string url = $"{apiBaseUrl}/api/generate-character-voice?text=Welcome to my administration&voice_id=politician_male";
        var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
        request.timeout = 60;
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            AudioManager.Instance?.PlayClip(clip);
            UpdateStatus("✅ Voice generated and playing!");
            LogResult($"Voice Generation: SUCCESS\nLength: {clip.length}s");
        }
        else
        {
            UpdateStatus($"❌ Voice generation failed: {request.error}");
            LogResult($"Voice Generation: FAILED\n{request.error}");
        }
    }
    
    private void UpdateStatus(string message)
    {
        if (statusText) statusText.text = message;
        Debug.Log($"[AIBackendTester] {message}");
    }
    
    private void LogResult(string result)
    {
        if (resultsText)
        {
            resultsText.text += $"\n{System.DateTime.Now:HH:mm:ss} - {result}\n";
        }
    }
    
    [System.Serializable]
    private class PortraitRequest
    {
        public string character_name;
        public string description;
    }
}
