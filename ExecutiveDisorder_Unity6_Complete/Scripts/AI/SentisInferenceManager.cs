using UnityEngine;
using Unity.Sentis;
using System.Collections.Generic;

namespace ExecutiveDisorder.AI
{
    /// <summary>
    /// Manages Sentis neural network inference for Executive Disorder.
    /// Loads trained models and performs real-time predictions.
    /// </summary>
    public class SentisInferenceManager : MonoBehaviour
    {
        [Header("Model Configuration")]
        [SerializeField] private ModelAsset playerBehaviorModel;
        [SerializeField] private ModelAsset difficultyPredictionModel;
        [SerializeField] private BackendType backendType = BackendType.GPUCompute;
        
        [Header("Inference Settings")]
        [SerializeField] private bool enableInference = true;
        [SerializeField] private bool useBatching = true;
        [SerializeField] private int batchSize = 8;
        
        // Workers for model execution
        private IWorker _playerBehaviorWorker;
        private IWorker _difficultyWorker;
        
        // Input/Output tensors
        private TensorFloat _inputTensor;
        private TensorFloat _outputTensor;
        
        // Singleton
        private static SentisInferenceManager _instance;
        public static SentisInferenceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SentisInferenceManager>();
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeWorkers();
        }
        
        private void OnDestroy()
        {
            CleanupWorkers();
        }
        
        #region Initialization
        
        private void InitializeWorkers()
        {
            if (!enableInference) return;
            
            try
            {
                // Initialize player behavior model worker
                if (playerBehaviorModel != null)
                {
                    Model runtimeModel = ModelLoader.Load(playerBehaviorModel);
                    _playerBehaviorWorker = new Worker(runtimeModel, backendType);
                    Debug.Log("Player behavior model loaded successfully");
                }
                else
                {
                    Debug.LogWarning("Player behavior model not assigned");
                }
                
                // Initialize difficulty prediction model worker
                if (difficultyPredictionModel != null)
                {
                    Model runtimeModel = ModelLoader.Load(difficultyPredictionModel);
                    _difficultyWorker = new Worker(runtimeModel, backendType);
                    Debug.Log("Difficulty prediction model loaded successfully");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to initialize Sentis workers: {e.Message}");
            }
        }
        
        private void CleanupWorkers()
        {
            _playerBehaviorWorker?.Dispose();
            _difficultyWorker?.Dispose();
            _inputTensor?.Dispose();
            _outputTensor?.Dispose();
        }
        
        #endregion
        
        #region Player Behavior Prediction
        
        /// <summary>
        /// Predict player's likely choice based on current game state
        /// </summary>
        public int PredictPlayerChoice(float[] gameState, int numChoices)
        {
            if (!enableInference || _playerBehaviorWorker == null)
            {
                return Random.Range(0, numChoices);
            }
            
            try
            {
                // Create input tensor from game state
                _inputTensor?.Dispose();
                _inputTensor = new TensorFloat(new TensorShape(1, gameState.Length), gameState);
                
                // Execute inference
                _playerBehaviorWorker.Schedule(_inputTensor);
                
                // Get output tensor
                _outputTensor?.Dispose();
                _outputTensor = _playerBehaviorWorker.PeekOutput() as TensorFloat;
                
                // Download results from GPU
                var outputData = _outputTensor.DownloadToArray();
                
                // Find choice with highest probability
                int predictedChoice = 0;
                float maxProbability = float.MinValue;
                
                for (int i = 0; i < Mathf.Min(numChoices, outputData.Length); i++)
                {
                    if (outputData[i] > maxProbability)
                    {
                        maxProbability = outputData[i];
                        predictedChoice = i;
                    }
                }
                
                return predictedChoice;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Prediction error: {e.Message}");
                return Random.Range(0, numChoices);
            }
        }
        
        /// <summary>
        /// Get probability distribution for all choices
        /// </summary>
        public float[] GetChoiceProbabilities(float[] gameState, int numChoices)
        {
            if (!enableInference || _playerBehaviorWorker == null)
            {
                // Return uniform distribution
                float[] uniform = new float[numChoices];
                float prob = 1f / numChoices;
                for (int i = 0; i < numChoices; i++)
                    uniform[i] = prob;
                return uniform;
            }
            
            try
            {
                _inputTensor?.Dispose();
                _inputTensor = new TensorFloat(new TensorShape(1, gameState.Length), gameState);
                
                _playerBehaviorWorker.Schedule(_inputTensor);
                _outputTensor?.Dispose();
                _outputTensor = _playerBehaviorWorker.PeekOutput() as TensorFloat;
                
                var outputData = _outputTensor.DownloadToArray();
                
                // Apply softmax and normalize
                float[] probabilities = new float[numChoices];
                float sum = 0f;
                
                for (int i = 0; i < numChoices && i < outputData.Length; i++)
                {
                    probabilities[i] = Mathf.Exp(outputData[i]);
                    sum += probabilities[i];
                }
                
                // Normalize
                for (int i = 0; i < numChoices; i++)
                {
                    probabilities[i] /= sum;
                }
                
                return probabilities;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Probability calculation error: {e.Message}");
                float[] uniform = new float[numChoices];
                float prob = 1f / numChoices;
                for (int i = 0; i < numChoices; i++)
                    uniform[i] = prob;
                return uniform;
            }
        }
        
        #endregion
        
        #region Difficulty Prediction
        
        /// <summary>
        /// Predict optimal difficulty level based on player performance
        /// </summary>
        public float PredictDifficulty(float[] performanceMetrics)
        {
            if (!enableInference || _difficultyWorker == null)
            {
                return 0.5f; // Default medium difficulty
            }
            
            try
            {
                _inputTensor?.Dispose();
                _inputTensor = new TensorFloat(new TensorShape(1, performanceMetrics.Length), performanceMetrics);
                
                _difficultyWorker.Schedule(_inputTensor);
                _outputTensor?.Dispose();
                _outputTensor = _difficultyWorker.PeekOutput() as TensorFloat;
                
                var outputData = _outputTensor.DownloadToArray();
                
                // Clamp difficulty between 0 and 1
                return Mathf.Clamp01(outputData[0]);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Difficulty prediction error: {e.Message}");
                return 0.5f;
            }
        }
        
        #endregion
        
        #region Batch Inference
        
        /// <summary>
        /// Perform batch inference for multiple game states
        /// </summary>
        public int[] PredictBatch(List<float[]> gameStates, int numChoices)
        {
            if (!useBatching || !enableInference || _playerBehaviorWorker == null)
            {
                // Fall back to sequential processing
                int[] results = new int[gameStates.Count];
                for (int i = 0; i < gameStates.Count; i++)
                {
                    results[i] = PredictPlayerChoice(gameStates[i], numChoices);
                }
                return results;
            }
            
            try
            {
                int batchCount = gameStates.Count;
                int stateSize = gameStates[0].Length;
                
                // Prepare batch input
                float[] batchInput = new float[batchCount * stateSize];
                for (int i = 0; i < batchCount; i++)
                {
                    System.Array.Copy(gameStates[i], 0, batchInput, i * stateSize, stateSize);
                }
                
                _inputTensor?.Dispose();
                _inputTensor = new TensorFloat(new TensorShape(batchCount, stateSize), batchInput);
                
                _playerBehaviorWorker.Schedule(_inputTensor);
                _outputTensor?.Dispose();
                _outputTensor = _playerBehaviorWorker.PeekOutput() as TensorFloat;
                
                var outputData = _outputTensor.DownloadToArray();
                
                // Extract predictions for each sample
                int[] predictions = new int[batchCount];
                for (int i = 0; i < batchCount; i++)
                {
                    int maxIdx = 0;
                    float maxVal = float.MinValue;
                    
                    for (int j = 0; j < numChoices; j++)
                    {
                        int idx = i * numChoices + j;
                        if (idx < outputData.Length && outputData[idx] > maxVal)
                        {
                            maxVal = outputData[idx];
                            maxIdx = j;
                        }
                    }
                    
                    predictions[i] = maxIdx;
                }
                
                return predictions;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Batch prediction error: {e.Message}");
                
                // Fallback
                int[] results = new int[gameStates.Count];
                for (int i = 0; i < gameStates.Count; i++)
                {
                    results[i] = Random.Range(0, numChoices);
                }
                return results;
            }
        }
        
        #endregion
        
        #region Utility Methods
        
        /// <summary>
        /// Check if inference is ready
        /// </summary>
        public bool IsReady()
        {
            return enableInference && _playerBehaviorWorker != null;
        }
        
        /// <summary>
        /// Switch backend type (CPU/GPU)
        /// </summary>
        public void SetBackendType(BackendType newBackend)
        {
            if (newBackend == backendType) return;
            
            backendType = newBackend;
            CleanupWorkers();
            InitializeWorkers();
        }
        
        /// <summary>
        /// Reload models
        /// </summary>
        public void ReloadModels()
        {
            CleanupWorkers();
            InitializeWorkers();
        }
        
        /// <summary>
        /// Get model information
        /// </summary>
        public string GetModelInfo()
        {
            string info = "Sentis Inference Manager\n";
            info += $"Backend: {backendType}\n";
            info += $"Player Model: {(playerBehaviorModel != null ? "Loaded" : "Not Loaded")}\n";
            info += $"Difficulty Model: {(difficultyPredictionModel != null ? "Loaded" : "Not Loaded")}\n";
            info += $"Inference Enabled: {enableInference}\n";
            info += $"Batching: {(useBatching ? $"Enabled (batch size: {batchSize})" : "Disabled")}";
            return info;
        }
        
        #endregion
    }
}
