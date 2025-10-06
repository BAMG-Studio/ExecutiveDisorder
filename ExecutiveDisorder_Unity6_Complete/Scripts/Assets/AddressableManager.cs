using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;

namespace ExecutiveDisorder.Assets
{
    /// <summary>
    /// Manages Addressable assets for Executive Disorder.
    /// Handles dynamic loading, caching, and memory management.
    /// </summary>
    public class AddressableManager : MonoBehaviour
    {
        [Header("Loading Settings")]
        [SerializeField] private bool preloadOnStart = true;
        [SerializeField] private bool cacheLoadedAssets = true;
        [SerializeField] private int maxCachedAssets = 50;
        
        [Header("Asset Labels")]
        [SerializeField] private string cardLabel = "Cards";
        [SerializeField] private string characterLabel = "Characters";
        [SerializeField] private string audioLabel = "Audio";
        [SerializeField] private string uiLabel = "UI";
        
        // Asset caches
        private Dictionary<string, DecisionCardData> _cardCache = new Dictionary<string, DecisionCardData>();
        private Dictionary<string, CharacterData> _characterCache = new Dictionary<string, CharacterData>();
        private Dictionary<string, AudioClip> _audioCache = new Dictionary<string, AudioClip>();
        private Dictionary<string, Sprite> _spriteCache = new Dictionary<string, Sprite>();
        
        // Loading operation tracking
        private Dictionary<string, AsyncOperationHandle> _loadingOperations = new Dictionary<string, AsyncOperationHandle>();
        
        // Singleton
        private static AddressableManager _instance;
        public static AddressableManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AddressableManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("AddressableManager");
                        _instance = go.AddComponent<AddressableManager>();
                    }
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
        }
        
        private void Start()
        {
            if (preloadOnStart)
            {
                PreloadEssentialAssets();
            }
        }
        
        private void OnDestroy()
        {
            ReleaseAllAssets();
        }
        
        #region Card Loading
        
        /// <summary>
        /// Load a decision card by address or label
        /// </summary>
        public void LoadCard(string address, Action<DecisionCardData> onComplete, Action<string> onError = null)
        {
            // Check cache first
            if (cacheLoadedAssets && _cardCache.ContainsKey(address))
            {
                onComplete?.Invoke(_cardCache[address]);
                return;
            }
            
            // Load from Addressables
            var handle = Addressables.LoadAssetAsync<DecisionCardData>(address);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    if (cacheLoadedAssets)
                    {
                        _cardCache[address] = op.Result;
                        ManageCacheSize(_cardCache, maxCachedAssets);
                    }
                    onComplete?.Invoke(op.Result);
                }
                else
                {
                    onError?.Invoke($"Failed to load card: {address}");
                }
            };
            
            _loadingOperations[address] = handle;
        }
        
        /// <summary>
        /// Load all cards with a specific label
        /// </summary>
        public void LoadCardsByLabel(string label, Action<List<DecisionCardData>> onComplete)
        {
            var handle = Addressables.LoadAssetsAsync<DecisionCardData>(label, null);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    List<DecisionCardData> cards = new List<DecisionCardData>(op.Result);
                    
                    if (cacheLoadedAssets)
                    {
                        foreach (var card in cards)
                        {
                            _cardCache[card.name] = card;
                        }
                    }
                    
                    onComplete?.Invoke(cards);
                }
            };
        }
        
        #endregion
        
        #region Character Loading
        
        /// <summary>
        /// Load character data
        /// </summary>
        public void LoadCharacter(string address, Action<CharacterData> onComplete, Action<string> onError = null)
        {
            if (cacheLoadedAssets && _characterCache.ContainsKey(address))
            {
                onComplete?.Invoke(_characterCache[address]);
                return;
            }
            
            var handle = Addressables.LoadAssetAsync<CharacterData>(address);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    if (cacheLoadedAssets)
                    {
                        _characterCache[address] = op.Result;
                    }
                    onComplete?.Invoke(op.Result);
                }
                else
                {
                    onError?.Invoke($"Failed to load character: {address}");
                }
            };
            
            _loadingOperations[address] = handle;
        }
        
        /// <summary>
        /// Load all characters
        /// </summary>
        public void LoadAllCharacters(Action<List<CharacterData>> onComplete)
        {
            LoadCharactersByLabel(characterLabel, onComplete);
        }
        
        /// <summary>
        /// Load characters by label
        /// </summary>
        public void LoadCharactersByLabel(string label, Action<List<CharacterData>> onComplete)
        {
            var handle = Addressables.LoadAssetsAsync<CharacterData>(label, null);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    List<CharacterData> characters = new List<CharacterData>(op.Result);
                    
                    if (cacheLoadedAssets)
                    {
                        foreach (var character in characters)
                        {
                            _characterCache[character.name] = character;
                        }
                    }
                    
                    onComplete?.Invoke(characters);
                }
            };
        }
        
        #endregion
        
        #region Audio Loading
        
        /// <summary>
        /// Load audio clip
        /// </summary>
        public void LoadAudio(string address, Action<AudioClip> onComplete, Action<string> onError = null)
        {
            if (cacheLoadedAssets && _audioCache.ContainsKey(address))
            {
                onComplete?.Invoke(_audioCache[address]);
                return;
            }
            
            var handle = Addressables.LoadAssetAsync<AudioClip>(address);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    if (cacheLoadedAssets)
                    {
                        _audioCache[address] = op.Result;
                        ManageCacheSize(_audioCache, maxCachedAssets);
                    }
                    onComplete?.Invoke(op.Result);
                }
                else
                {
                    onError?.Invoke($"Failed to load audio: {address}");
                }
            };
            
            _loadingOperations[address] = handle;
        }
        
        #endregion
        
        #region Sprite Loading
        
        /// <summary>
        /// Load sprite
        /// </summary>
        public void LoadSprite(string address, Action<Sprite> onComplete, Action<string> onError = null)
        {
            if (cacheLoadedAssets && _spriteCache.ContainsKey(address))
            {
                onComplete?.Invoke(_spriteCache[address]);
                return;
            }
            
            var handle = Addressables.LoadAssetAsync<Sprite>(address);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    if (cacheLoadedAssets)
                    {
                        _spriteCache[address] = op.Result;
                        ManageCacheSize(_spriteCache, maxCachedAssets);
                    }
                    onComplete?.Invoke(op.Result);
                }
                else
                {
                    onError?.Invoke($"Failed to load sprite: {address}");
                }
            };
            
            _loadingOperations[address] = handle;
        }
        
        #endregion
        
        #region Preloading
        
        /// <summary>
        /// Preload essential assets for quick access
        /// </summary>
        public void PreloadEssentialAssets()
        {
            Debug.Log("Preloading essential assets...");
            
            // Preload all characters
            LoadAllCharacters((characters) =>
            {
                Debug.Log($"Preloaded {characters.Count} characters");
            });
            
            // Preload UI assets
            LoadAssetsByLabel<Sprite>(uiLabel, (sprites) =>
            {
                foreach (var sprite in sprites)
                {
                    _spriteCache[sprite.name] = sprite;
                }
                Debug.Log($"Preloaded {sprites.Count} UI sprites");
            });
        }
        
        /// <summary>
        /// Generic method to load assets by label
        /// </summary>
        public void LoadAssetsByLabel<T>(string label, Action<List<T>> onComplete) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetsAsync<T>(label, null);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    onComplete?.Invoke(new List<T>(op.Result));
                }
            };
        }
        
        #endregion
        
        #region Cache Management
        
        private void ManageCacheSize<T>(Dictionary<string, T> cache, int maxSize)
        {
            if (cache.Count > maxSize)
            {
                // Simple FIFO eviction - remove oldest entries
                int toRemove = cache.Count - maxSize;
                var enumerator = cache.GetEnumerator();
                
                for (int i = 0; i < toRemove && enumerator.MoveNext(); i++)
                {
                    string key = enumerator.Current.Key;
                    cache.Remove(key);
                    
                    // Release the addressable
                    if (_loadingOperations.ContainsKey(key))
                    {
                        Addressables.Release(_loadingOperations[key]);
                        _loadingOperations.Remove(key);
                    }
                }
            }
        }
        
        /// <summary>
        /// Clear all caches
        /// </summary>
        public void ClearCaches()
        {
            _cardCache.Clear();
            _characterCache.Clear();
            _audioCache.Clear();
            _spriteCache.Clear();
            
            Debug.Log("All asset caches cleared");
        }
        
        /// <summary>
        /// Release a specific asset
        /// </summary>
        public void ReleaseAsset(string address)
        {
            _cardCache.Remove(address);
            _characterCache.Remove(address);
            _audioCache.Remove(address);
            _spriteCache.Remove(address);
            
            if (_loadingOperations.ContainsKey(address))
            {
                Addressables.Release(_loadingOperations[address]);
                _loadingOperations.Remove(address);
            }
        }
        
        /// <summary>
        /// Release all loaded assets
        /// </summary>
        public void ReleaseAllAssets()
        {
            foreach (var handle in _loadingOperations.Values)
            {
                Addressables.Release(handle);
            }
            
            _loadingOperations.Clear();
            ClearCaches();
            
            Debug.Log("All addressable assets released");
        }
        
        #endregion
        
        #region Utility Methods
        
        /// <summary>
        /// Get cache statistics
        /// </summary>
        public string GetCacheStats()
        {
            return $"Cache Statistics:\n" +
                   $"Cards: {_cardCache.Count}\n" +
                   $"Characters: {_characterCache.Count}\n" +
                   $"Audio: {_audioCache.Count}\n" +
                   $"Sprites: {_spriteCache.Count}\n" +
                   $"Active Operations: {_loadingOperations.Count}";
        }
        
        /// <summary>
        /// Check if an asset is loaded
        /// </summary>
        public bool IsAssetLoaded(string address)
        {
            return _cardCache.ContainsKey(address) ||
                   _characterCache.ContainsKey(address) ||
                   _audioCache.ContainsKey(address) ||
                   _spriteCache.ContainsKey(address);
        }
        
        #endregion
    }
}
