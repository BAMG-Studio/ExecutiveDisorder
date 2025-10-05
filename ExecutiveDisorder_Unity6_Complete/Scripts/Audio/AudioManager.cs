using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

namespace ExecutiveDisorder.Audio
{
    /// <summary>
    /// Central audio management system
    /// </summary>
    public class AudioManager : Core.Singleton<AudioManager>
    {
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource uiSource;
        [SerializeField] private AudioSource voiceSource;

        [Header("Music Tracks")]
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip gameplayMusic;
        [SerializeField] private AudioClip crisisMusic;
        [SerializeField] private AudioClip endingMusic;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip buttonClickSound;
        [SerializeField] private AudioClip cardFlipSound;
        [SerializeField] private AudioClip resourceGainSound;
        [SerializeField] private AudioClip resourceLossSound;
        [SerializeField] private AudioClip crisisAlertSound;
        [SerializeField] private AudioClip successSound;
        [SerializeField] private AudioClip failureSound;

        [Header("Settings")]
        [SerializeField] private float musicFadeDuration = 1f;
        [SerializeField] private bool enableMusic = true;
        [SerializeField] private bool enableSFX = true;

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
        private Coroutine musicFadeCoroutine;

        protected override void Awake()
        {
            base.Awake();
            
            // Create audio sources if not assigned
            EnsureAudioSources();
            
            // Build sound library
            BuildSoundLibrary();
        }

        private void Start()
        {
            // Load saved audio settings
            LoadAudioSettings();

            // Play main menu music
            PlayMusic(mainMenuMusic);
        }

        /// <summary>
        /// Ensure all audio sources exist
        /// </summary>
        private void EnsureAudioSources()
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
                musicSource.outputAudioMixerGroup = audioMixer?.FindMatchingGroups("Music")[0];
            }

            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.playOnAwake = false;
                sfxSource.outputAudioMixerGroup = audioMixer?.FindMatchingGroups("SFX")[0];
            }

            if (uiSource == null)
            {
                uiSource = gameObject.AddComponent<AudioSource>();
                uiSource.playOnAwake = false;
                uiSource.outputAudioMixerGroup = audioMixer?.FindMatchingGroups("UI")[0];
            }

            if (voiceSource == null)
            {
                voiceSource = gameObject.AddComponent<AudioSource>();
                voiceSource.playOnAwake = false;
                voiceSource.outputAudioMixerGroup = audioMixer?.FindMatchingGroups("Voice")[0];
            }
        }

        /// <summary>
        /// Build sound library for quick access
        /// </summary>
        private void BuildSoundLibrary()
        {
            if (buttonClickSound != null)
                soundLibrary["button_click"] = buttonClickSound;
            if (cardFlipSound != null)
                soundLibrary["card_flip"] = cardFlipSound;
            if (resourceGainSound != null)
                soundLibrary["resource_gain"] = resourceGainSound;
            if (resourceLossSound != null)
                soundLibrary["resource_loss"] = resourceLossSound;
            if (crisisAlertSound != null)
                soundLibrary["crisis_alert"] = crisisAlertSound;
            if (successSound != null)
                soundLibrary["success"] = successSound;
            if (failureSound != null)
                soundLibrary["failure"] = failureSound;
        }

        /// <summary>
        /// Play music track
        /// </summary>
        public void PlayMusic(AudioClip clip, bool fadeIn = true)
        {
            if (!enableMusic || clip == null || musicSource == null)
                return;

            if (musicFadeCoroutine != null)
            {
                StopCoroutine(musicFadeCoroutine);
            }

            if (fadeIn)
            {
                musicFadeCoroutine = StartCoroutine(CrossfadeMusic(clip));
            }
            else
            {
                musicSource.clip = clip;
                musicSource.Play();
            }

            if (showDebugLogs)
                Debug.Log($"[AudioManager] Playing music: {clip.name}");
        }

        /// <summary>
        /// Crossfade between music tracks
        /// </summary>
        private IEnumerator CrossfadeMusic(AudioClip newClip)
        {
            float halfFadeDuration = musicFadeDuration / 2f;

            // Fade out current music
            float elapsed = 0f;
            float startVolume = musicSource.volume;

            while (elapsed < halfFadeDuration)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / halfFadeDuration);
                yield return null;
            }

            musicSource.volume = 0f;

            // Change track
            musicSource.clip = newClip;
            musicSource.Play();

            // Fade in new music
            elapsed = 0f;

            while (elapsed < halfFadeDuration)
            {
                elapsed += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(0f, 1f, elapsed / halfFadeDuration);
                yield return null;
            }

            musicSource.volume = 1f;
            musicFadeCoroutine = null;
        }

        /// <summary>
        /// Play sound effect by name
        /// </summary>
        public void PlaySound(string soundName, float volumeScale = 1f)
        {
            if (!enableSFX)
                return;

            if (soundLibrary.ContainsKey(soundName))
            {
                PlaySound(soundLibrary[soundName], volumeScale);
            }
            else if (showDebugLogs)
            {
                Debug.LogWarning($"[AudioManager] Sound not found: {soundName}");
            }
        }

        /// <summary>
        /// Play sound effect by clip
        /// </summary>
        public void PlaySound(AudioClip clip, float volumeScale = 1f)
        {
            if (!enableSFX || clip == null || sfxSource == null)
                return;

            sfxSource.PlayOneShot(clip, volumeScale);
        }

        /// <summary>
        /// Play UI sound
        /// </summary>
        public void PlayUISound(AudioClip clip, float volumeScale = 1f)
        {
            if (!enableSFX || clip == null || uiSource == null)
                return;

            uiSource.PlayOneShot(clip, volumeScale);
        }

        /// <summary>
        /// Play button click sound
        /// </summary>
        public void PlayButtonClick()
        {
            PlaySound("button_click");
        }

        /// <summary>
        /// Play card flip sound
        /// </summary>
        public void PlayCardFlip()
        {
            PlaySound("card_flip");
        }

        /// <summary>
        /// Play resource change sound
        /// </summary>
        public void PlayResourceChange(bool isGain)
        {
            PlaySound(isGain ? "resource_gain" : "resource_loss");
        }

        /// <summary>
        /// Play crisis alert
        /// </summary>
        public void PlayCrisisAlert()
        {
            PlaySound("crisis_alert", 1.2f);
        }

        /// <summary>
        /// Set master volume
        /// </summary>
        public void SetMasterVolume(float volume)
        {
            audioMixer?.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        /// <summary>
        /// Set music volume
        /// </summary>
        public void SetMusicVolume(float volume)
        {
            audioMixer?.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        /// <summary>
        /// Set SFX volume
        /// </summary>
        public void SetSFXVolume(float volume)
        {
            audioMixer?.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        /// <summary>
        /// Toggle music on/off
        /// </summary>
        public void ToggleMusic(bool enabled)
        {
            enableMusic = enabled;
            musicSource.mute = !enabled;
            PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
        }

        /// <summary>
        /// Toggle SFX on/off
        /// </summary>
        public void ToggleSFX(bool enabled)
        {
            enableSFX = enabled;
            PlayerPrefs.SetInt("SFXEnabled", enabled ? 1 : 0);
        }

        /// <summary>
        /// Load saved audio settings
        /// </summary>
        private void LoadAudioSettings()
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            }

            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            }

            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            }

            if (PlayerPrefs.HasKey("MusicEnabled"))
            {
                enableMusic = PlayerPrefs.GetInt("MusicEnabled") == 1;
                musicSource.mute = !enableMusic;
            }

            if (PlayerPrefs.HasKey("SFXEnabled"))
            {
                enableSFX = PlayerPrefs.GetInt("SFXEnabled") == 1;
            }
        }

        /// <summary>
        /// Play gameplay music
        /// </summary>
        public void PlayGameplayMusic()
        {
            PlayMusic(gameplayMusic);
        }

        /// <summary>
        /// Play crisis music
        /// </summary>
        public void PlayCrisisMusic()
        {
            PlayMusic(crisisMusic);
        }

        /// <summary>
        /// Play ending music
        /// </summary>
        public void PlayEndingMusic()
        {
            PlayMusic(endingMusic);
        }
    }
}
