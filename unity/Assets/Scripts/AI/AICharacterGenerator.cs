using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class AICharacterGenerator : MonoBehaviour
{
    [SerializeField] private string apiBaseUrl = "https://your-replit-url.repl.co";
    
    public IEnumerator GenerateCharacterPortrait(string characterName, string description, Action<Texture2D> onComplete)
    {
        string json = JsonUtility.ToJson(new PortraitRequest
        {
            character_name = characterName,
            description = description
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
            onComplete?.Invoke(texture);
        }
        else
        {
            Debug.LogError($"Portrait generation failed: {request.error}");
            onComplete?.Invoke(null);
        }
    }
    
    public IEnumerator GenerateCharacterVoice(string text, string voiceId, Action<AudioClip> onComplete)
    {
        string url = $"{apiBaseUrl}/api/generate-character-voice?text={UnityWebRequest.EscapeURL(text)}&voice_id={voiceId}";
        var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
        request.timeout = 60;
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            onComplete?.Invoke(clip);
        }
        else
        {
            Debug.LogError($"Voice generation failed: {request.error}");
            onComplete?.Invoke(null);
        }
    }
    
    [Serializable]
    private class PortraitRequest
    {
        public string character_name;
        public string description;
    }
}
