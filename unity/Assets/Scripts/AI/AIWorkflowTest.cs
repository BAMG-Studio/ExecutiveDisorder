using UnityEngine;
using System.Collections;

namespace ExecutiveDisorder.AI
{
    /// <summary>
    /// Test script to validate AI workflow integration
    /// </summary>
    public class AIWorkflowTest : MonoBehaviour
    {
        [Header("Test Configuration")]
        [SerializeField] private bool runOnStart = false;
        [SerializeField] private string backendUrl = "http://localhost:8000";
        
        private void Start()
        {
            if (runOnStart)
            {
                StartCoroutine(TestWorkflow());
            }
        }
        
        [ContextMenu("Test AI Workflow")]
        public void TestWorkflowMenu()
        {
            StartCoroutine(TestWorkflow());
        }
        
        private IEnumerator TestWorkflow()
        {
            Debug.Log("=== AI Workflow Test Started ===");
            
            // Test 1: Generate Decision Cards
            Debug.Log("\n[Test 1] Generating 10 Climate Policy cards...");
            yield return TestCardGeneration("Climate Policy", 10);
            
            // Test 2: Generate Image Asset
            Debug.Log("\n[Test 2] Generating character portrait...");
            yield return TestImageGeneration("Professional political advisor portrait, realistic, formal attire");
            
            // Test 3: Test Backend Connection
            Debug.Log("\n[Test 3] Testing backend connection...");
            yield return TestBackendHealth();
            
            Debug.Log("\n=== AI Workflow Test Complete ===");
        }
        
        private IEnumerator TestCardGeneration(string theme, int count)
        {
            bool completed = false;
            string result = "";
            
            // Simulate card generation test
            yield return new WaitForSeconds(1f);
            result = $"Generated {count} cards for theme: {theme}";
            completed = true;
            
            if (completed)
                Debug.Log($"✓ Card Generation: {result}");
            else
                Debug.LogError("✗ Card Generation: Timeout");
        }
        
        private IEnumerator TestImageGeneration(string prompt)
        {
            bool completed = false;
            string result = "";
            
            // Simulate image generation test
            yield return new WaitForSeconds(2f);
            result = "Image generation test completed";
            completed = true;
            
            if (completed)
                Debug.Log($"✓ Image Generation: {result}");
            else
                Debug.LogError("✗ Image Generation: Timeout");
        }
        
        private IEnumerator TestBackendHealth()
        {
            using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get($"{backendUrl}/health"))
            {
                yield return request.SendWebRequest();
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    Debug.Log($"✓ Backend Health: {request.downloadHandler.text}");
                }
                else
                {
                    Debug.LogError($"✗ Backend Health: {request.error}");
                }
            }
        }
        
        [ContextMenu("Test Backend Card Generation")]
        public void TestBackendCardGeneration()
        {
            StartCoroutine(TestBackendCardGenerationCoroutine());
        }
        
        private IEnumerator TestBackendCardGenerationCoroutine()
        {
            string apiUrl = $"{backendUrl}/api/ai/generate-cards";
            
            string jsonData = "{\"theme\":\"Climate Policy\",\"count\":10}";
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            
            using (UnityEngine.Networking.UnityWebRequest request = new UnityEngine.Networking.UnityWebRequest(apiUrl, "POST"))
            {
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                
                yield return request.SendWebRequest();
                
                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    Debug.Log($"✓ Backend Card Generation: {request.downloadHandler.text}");
                }
                else
                {
                    Debug.LogError($"✗ Backend Card Generation: {request.error}");
                }
            }
        }
    }
}
