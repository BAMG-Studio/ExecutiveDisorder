using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Displays and animates resource bar
    /// </summary>
    public class ResourceBarUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private Image iconImage;

        [Header("Resource Settings")]
        [SerializeField] private Core.ResourceType resourceType;
        [SerializeField] private Core.ResourceDefinition resourceDefinition;

        [Header("Visual Settings")]
        [SerializeField] private bool animateChanges = true;
        [SerializeField] private float animationSpeed = 2f;
        [SerializeField] private bool showPercentage = true;
        [SerializeField] private bool flashOnChange = true;

        [Header("Color Coding")]
        [SerializeField] private Color highColor = Color.green;
        [SerializeField] private Color mediumColor = Color.yellow;
        [SerializeField] private Color lowColor = Color.red;
        [SerializeField] private float lowThreshold = 20f;
        [SerializeField] private float highThreshold = 80f;

        private float currentValue = 50f;
        private float targetValue = 50f;
        private bool isAnimating = false;

        private void Start()
        {
            // Initialize from resource definition if available
            if (resourceDefinition != null)
            {
                if (labelText != null)
                    labelText.text = resourceDefinition.displayName;

                if (iconImage != null && resourceDefinition.icon != null)
                    iconImage.sprite = resourceDefinition.icon;

                currentValue = resourceDefinition.startingValue;
                targetValue = resourceDefinition.startingValue;
            }

            UpdateVisuals();
        }

        /// <summary>
        /// Set resource value
        /// </summary>
        public void SetValue(float value)
        {
            targetValue = Mathf.Clamp(value, 0f, 100f);

            if (animateChanges && gameObject.activeInHierarchy)
            {
                if (!isAnimating)
                {
                    StartCoroutine(AnimateToValue());
                }
            }
            else
            {
                currentValue = targetValue;
                UpdateVisuals();
            }

            if (flashOnChange && gameObject.activeInHierarchy)
            {
                StartCoroutine(FlashEffect());
            }
        }

        /// <summary>
        /// Animate value change
        /// </summary>
        private IEnumerator AnimateToValue()
        {
            isAnimating = true;

            while (Mathf.Abs(currentValue - targetValue) > 0.1f)
            {
                currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * animationSpeed);
                UpdateVisuals();
                yield return null;
            }

            currentValue = targetValue;
            UpdateVisuals();
            isAnimating = false;
        }

        /// <summary>
        /// Update all visual elements
        /// </summary>
        private void UpdateVisuals()
        {
            // Update fill amount
            if (fillImage != null)
            {
                fillImage.fillAmount = currentValue / 100f;
                fillImage.color = GetColorForValue(currentValue);
            }

            // Update text
            if (valueText != null)
            {
                if (showPercentage)
                {
                    valueText.text = $"{currentValue:F0}%";
                }
                else
                {
                    valueText.text = $"{currentValue:F0}";
                }
            }
        }

        /// <summary>
        /// Get color based on value
        /// </summary>
        private Color GetColorForValue(float value)
        {
            if (value <= lowThreshold)
            {
                return Color.Lerp(lowColor, mediumColor, value / lowThreshold);
            }
            else if (value >= highThreshold)
            {
                float t = (value - highThreshold) / (100f - highThreshold);
                return Color.Lerp(mediumColor, highColor, t);
            }
            else
            {
                float t = (value - lowThreshold) / (highThreshold - lowThreshold);
                return Color.Lerp(mediumColor, mediumColor, t);
            }
        }

        /// <summary>
        /// Flash effect on value change
        /// </summary>
        private IEnumerator FlashEffect()
        {
            if (fillImage == null)
                yield break;

            Color originalColor = fillImage.color;
            Color flashColor = Color.white;

            float flashDuration = 0.2f;
            float elapsed = 0f;

            while (elapsed < flashDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / flashDuration;
                fillImage.color = Color.Lerp(flashColor, originalColor, t);
                yield return null;
            }

            fillImage.color = GetColorForValue(currentValue);
        }

        /// <summary>
        /// Set resource definition
        /// </summary>
        public void SetResourceDefinition(Core.ResourceDefinition definition)
        {
            resourceDefinition = definition;
            
            if (labelText != null)
                labelText.text = definition.displayName;

            if (iconImage != null && definition.icon != null)
                iconImage.sprite = definition.icon;
        }
    }
}
