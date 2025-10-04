using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class CloudSaveManager : MonoBehaviour
{
    private const string API_URL = "http://localhost:8000/api";
    private int userId = -1;

    public IEnumerator CreateUser(string username, string password, System.Action<int> onSuccess)
    {
        var json = $"{{\"username\":\"{username}\",\"password\":\"{password}\"}}";
        yield return PostRequest($"{API_URL}/users", json, (response) => {
            var data = JsonUtility.FromJson<UserResponse>(response);
            userId = data.id;
            onSuccess?.Invoke(userId);
        });
    }

    public IEnumerator SaveGame(string saveData, System.Action<int> onSuccess)
    {
        if (userId == -1) yield break;
        var json = $"{{\"user_id\":{userId},\"save_data\":{saveData}}}";
        yield return PostRequest($"{API_URL}/saves", json, (response) => {
            var data = JsonUtility.FromJson<SaveResponse>(response);
            onSuccess?.Invoke(data.id);
        });
    }

    public IEnumerator LoadSaves(System.Action<string> onSuccess)
    {
        if (userId == -1) yield break;
        yield return GetRequest($"{API_URL}/saves/{userId}", onSuccess);
    }

    private IEnumerator PostRequest(string url, string json, System.Action<string> onSuccess)
    {
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
    }

    private IEnumerator GetRequest(string url, System.Action<string> onSuccess)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
    }

    [System.Serializable]
    private class UserResponse { public int id; public string username; }
    [System.Serializable]
    private class SaveResponse { public int id; }
}
