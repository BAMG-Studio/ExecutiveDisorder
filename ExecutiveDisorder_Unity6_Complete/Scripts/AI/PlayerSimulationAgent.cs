using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

namespace ExecutiveDisorder.AI
{
    /// <summary>
    /// ML-Agents integration for player simulation and AI training.
    /// This agent learns to play Executive Disorder by making decisions
    /// and optimizing for survival and positive outcomes.
    /// </summary>
    public class PlayerSimulationAgent : Agent
    {
        [Header("Agent Configuration")]
        [SerializeField] private bool trainMode = true;
        [SerializeField] private float survivalReward = 1.0f;
        [SerializeField] private float balanceReward = 0.5f;
        [SerializeField] private float gameOverPenalty = -10.0f;
        
        [Header("Observation Settings")]
        [SerializeField] private bool observeResourceLevels = true;
        [SerializeField] private bool observeCharacterLoyalty = true;
        [SerializeField] private bool observeCardHistory = true;
        [SerializeField] private int cardHistorySize = 5;
        
        [Header("Game References")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private CharacterManager characterManager;
        [SerializeField] private CardManager cardManager;
        
        // Observation tracking
        private Queue<int> _recentChoices = new Queue<int>();
        private float _lastResourceHealth;
        private int _currentDay;
        private DecisionCardData _currentCard;
        
        // Performance metrics
        private int _episodesSurvived;
        private float _averageSurvivalDays;
        private int _totalDecisions;
        
        #region Unity ML-Agents Lifecycle
        
        public override void Initialize()
        {
            base.Initialize();
            
            // Find managers if not assigned
            if (gameManager == null)
                gameManager = FindObjectOfType<GameManager>();
            if (resourceManager == null)
                resourceManager = FindObjectOfType<ResourceManager>();
            if (characterManager == null)
                characterManager = FindObjectOfType<CharacterManager>();
            if (cardManager == null)
                cardManager = FindObjectOfType<CardManager>();
            
            // Subscribe to game events
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnGameOver.AddListener(OnGameEnded);
                EventManager.Instance.OnDayChanged.AddListener(OnDayAdvanced);
            }
            
            Debug.Log("PlayerSimulationAgent initialized");
        }
        
        public override void OnEpisodeBegin()
        {
            // Reset game state for new episode
            _recentChoices.Clear();
            _lastResourceHealth = 0f;
            _currentDay = 0;
            _currentCard = null;
            
            // Start new game
            if (gameManager != null)
            {
                gameManager.StartNewGame();
            }
            
            Debug.Log($"Episode {CompletedEpisodes} started");
        }
        
        public override void CollectObservations(VectorSensor sensor)
        {
            // Collect observations about the current game state
            
            // 1. Resource Levels (4 observations)
            if (observeResourceLevels && resourceManager != null)
            {
                sensor.AddObservation(resourceManager.GetResourceValue("Popularity") / 100f);
                sensor.AddObservation(resourceManager.GetResourceValue("Stability") / 100f);
                sensor.AddObservation(resourceManager.GetResourceValue("MediaTrust") / 100f);
                sensor.AddObservation(resourceManager.GetResourceValue("EconomicHealth") / 100f);
            }
            
            // 2. Resource Health (1 observation)
            float resourceHealth = resourceManager != null ? resourceManager.GetResourceHealth() : 0f;
            sensor.AddObservation(resourceHealth);
            
            // 3. Current Day normalized (1 observation)
            sensor.AddObservation(_currentDay / 100f);
            
            // 4. Character Loyalty Levels (8 observations)
            if (observeCharacterLoyalty && characterManager != null)
            {
                var characters = characterManager.GetAllCharacters();
                foreach (var character in characters)
                {
                    float loyalty = characterManager.GetLoyalty(character.characterName) / 100f;
                    sensor.AddObservation(loyalty);
                }
                
                // Pad if less than 8 characters
                for (int i = characters.Count; i < 8; i++)
                {
                    sensor.AddObservation(0f);
                }
            }
            
            // 5. Current Card Context (if available)
            if (_currentCard != null)
            {
                // Card type (one-hot encoded: 3 observations)
                sensor.AddObservation(_currentCard.cardType == DecisionCardData.CardType.Standard ? 1f : 0f);
                sensor.AddObservation(_currentCard.cardType == DecisionCardData.CardType.Crisis ? 1f : 0f);
                sensor.AddObservation(_currentCard.cardType == DecisionCardData.CardType.Opportunity ? 1f : 0f);
                
                // Number of choices available (1 observation)
                sensor.AddObservation(_currentCard.GetChoices().Count / 3f); // Normalize to max 3
            }
            else
            {
                // No card available - add zeros
                sensor.AddObservation(0f);
                sensor.AddObservation(0f);
                sensor.AddObservation(0f);
                sensor.AddObservation(0f);
            }
            
            // 6. Recent Choice History (cardHistorySize observations)
            if (observeCardHistory)
            {
                int[] historyArray = _recentChoices.ToArray();
                for (int i = 0; i < cardHistorySize; i++)
                {
                    if (i < historyArray.Length)
                    {
                        sensor.AddObservation(historyArray[i] / 2f); // Normalize to max 3 choices
                    }
                    else
                    {
                        sensor.AddObservation(0f);
                    }
                }
            }
            
            // Total observations: 4 + 1 + 1 + 8 + 4 + 5 = 23
        }
        
        public override void OnActionReceived(ActionBuffers actions)
        {
            // Get the discrete action (which choice to make)
            int choiceIndex = actions.DiscreteActions[0];
            
            // Make the choice in the game
            if (_currentCard != null && cardManager != null)
            {
                var choices = _currentCard.GetChoices();
                if (choiceIndex >= 0 && choiceIndex < choices.Count)
                {
                    // Execute the choice
                    cardManager.MakeChoice(choiceIndex);
                    
                    // Track the choice
                    _recentChoices.Enqueue(choiceIndex);
                    if (_recentChoices.Count > cardHistorySize)
                    {
                        _recentChoices.Dequeue();
                    }
                    
                    _totalDecisions++;
                    
                    // Calculate reward
                    CalculateReward();
                }
                else
                {
                    // Invalid action
                    AddReward(-0.1f);
                }
                
                _currentCard = null;
            }
        }
        
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            // Manual control for testing
            var discreteActions = actionsOut.DiscreteActions;
            
            // Simple heuristic: choose based on resource health
            if (_currentCard != null && resourceManager != null)
            {
                var choices = _currentCard.GetChoices();
                int bestChoice = 0;
                float bestScore = float.MinValue;
                
                // Evaluate each choice
                for (int i = 0; i < choices.Count; i++)
                {
                    float score = EvaluateChoice(choices[i]);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestChoice = i;
                    }
                }
                
                discreteActions[0] = bestChoice;
            }
            else
            {
                discreteActions[0] = 0;
            }
        }
        
        #endregion
        
        #region Reward Calculation
        
        private void CalculateReward()
        {
            if (resourceManager == null) return;
            
            float currentHealth = resourceManager.GetResourceHealth();
            float healthChange = currentHealth - _lastResourceHealth;
            
            // Reward for maintaining/improving resource health
            AddReward(healthChange * balanceReward);
            
            // Small reward for surviving another decision
            AddReward(0.01f);
            
            // Penalty for getting close to game over thresholds
            float minResource = Mathf.Min(
                resourceManager.GetResourceValue("Popularity"),
                resourceManager.GetResourceValue("Stability"),
                resourceManager.GetResourceValue("MediaTrust"),
                resourceManager.GetResourceValue("EconomicHealth")
            );
            
            if (minResource < 20f)
            {
                AddReward(-0.1f * (20f - minResource) / 20f);
            }
            
            // Reward for maintaining balance (all resources near 50)
            float balanceScore = 1f - (Mathf.Abs(currentHealth - 0.5f) * 2f);
            AddReward(balanceScore * 0.05f);
            
            _lastResourceHealth = currentHealth;
        }
        
        private float EvaluateChoice(DecisionCardData.Choice choice)
        {
            // Simple heuristic evaluation of a choice
            if (resourceManager == null) return 0f;
            
            float score = 0f;
            
            // Predict impact on each resource
            float popImpact = resourceManager.GetResourceValue("Popularity") + choice.popularityChange;
            float stabImpact = resourceManager.GetResourceValue("Stability") + choice.stabilityChange;
            float mediaImpact = resourceManager.GetResourceValue("MediaTrust") + choice.mediaTrustChange;
            float econImpact = resourceManager.GetResourceValue("EconomicHealth") + choice.economicHealthChange;
            
            // Penalize choices that would cause game over
            if (popImpact < 0 || stabImpact < 0 || mediaImpact < 0 || econImpact < 0)
            {
                return -100f;
            }
            
            // Favor choices that keep resources balanced
            float avgResource = (popImpact + stabImpact + mediaImpact + econImpact) / 4f;
            float variance = Mathf.Pow(popImpact - avgResource, 2) +
                            Mathf.Pow(stabImpact - avgResource, 2) +
                            Mathf.Pow(mediaImpact - avgResource, 2) +
                            Mathf.Pow(econImpact - avgResource, 2);
            
            score = 100f - variance;
            
            return score;
        }
        
        #endregion
        
        #region Game Event Handlers
        
        private void OnGameEnded(string ending)
        {
            // Episode ended
            _episodesSurvived++;
            _averageSurvivalDays = (_averageSurvivalDays * (_episodesSurvived - 1) + _currentDay) / _episodesSurvived;
            
            // Give final reward based on how long survived
            float survivalBonus = (_currentDay / 100f) * survivalReward;
            AddReward(survivalBonus);
            
            // Large penalty if game over was due to poor decisions
            if (_currentDay < 50)
            {
                AddReward(gameOverPenalty);
            }
            
            Debug.Log($"Episode ended on day {_currentDay}. Total reward: {GetCumulativeReward()}");
            
            // End the episode
            EndEpisode();
        }
        
        private void OnDayAdvanced(int day)
        {
            _currentDay = day;
            
            // Give small reward for surviving another day
            AddReward(0.01f);
            
            // Extra reward for reaching milestones
            if (day % 10 == 0)
            {
                AddReward(0.1f);
                Debug.Log($"Agent reached day {day}");
            }
        }
        
        public void OnCardPresented(DecisionCardData card)
        {
            _currentCard = card;
            
            // Request a decision from the agent
            if (trainMode)
            {
                RequestDecision();
            }
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Enable or disable training mode
        /// </summary>
        public void SetTrainingMode(bool enabled)
        {
            trainMode = enabled;
        }
        
        /// <summary>
        /// Get current agent statistics
        /// </summary>
        public string GetStatistics()
        {
            return $"Episodes: {_episodesSurvived}\n" +
                   $"Avg Survival: {_averageSurvivalDays:F1} days\n" +
                   $"Total Decisions: {_totalDecisions}\n" +
                   $"Cumulative Reward: {GetCumulativeReward():F2}";
        }
        
        /// <summary>
        /// Reset agent statistics
        /// </summary>
        public void ResetStatistics()
        {
            _episodesSurvived = 0;
            _averageSurvivalDays = 0f;
            _totalDecisions = 0;
        }
        
        #endregion
        
        #region Debug Visualization
        
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || resourceManager == null) return;
            
            // Visualize resource levels in scene view
            Vector3 basePos = transform.position + Vector3.up * 2f;
            
            DrawResourceBar(basePos + Vector3.left * 1.5f, "Pop", resourceManager.GetResourceValue("Popularity"), Color.green);
            DrawResourceBar(basePos + Vector3.left * 0.5f, "Stab", resourceManager.GetResourceValue("Stability"), Color.blue);
            DrawResourceBar(basePos + Vector3.right * 0.5f, "Media", resourceManager.GetResourceValue("MediaTrust"), Color.yellow);
            DrawResourceBar(basePos + Vector3.right * 1.5f, "Econ", resourceManager.GetResourceValue("EconomicHealth"), Color.cyan);
        }
        
        private void DrawResourceBar(Vector3 position, string label, float value, Color color)
        {
            float height = (value / 100f) * 2f;
            
            Gizmos.color = color;
            Gizmos.DrawCube(position + Vector3.up * height * 0.5f, new Vector3(0.3f, height, 0.3f));
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(position + Vector3.up, new Vector3(0.3f, 2f, 0.3f));
            
            #if UNITY_EDITOR
            UnityEditor.Handles.Label(position + Vector3.up * 2.2f, $"{label}\n{value:F0}");
            #endif
        }
        
        #endregion
    }
}
