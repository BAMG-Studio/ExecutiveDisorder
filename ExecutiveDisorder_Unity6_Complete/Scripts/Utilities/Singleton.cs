using UnityEngine;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Generic singleton pattern for MonoBehaviour
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static object lockObject = new object();
        private static bool applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance of {typeof(T)} already destroyed. Returning null.");
                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            GameObject singletonObject = new GameObject($"{typeof(T).Name} (Singleton)");
                            instance = singletonObject.AddComponent<T>();
                            
                            Debug.Log($"[Singleton] Created new instance of {typeof(T)}");
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T)} destroyed");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                applicationIsQuitting = true;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }
    }
}
