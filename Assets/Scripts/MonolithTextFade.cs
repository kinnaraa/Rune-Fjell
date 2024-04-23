using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonolithTextFade : MonoBehaviour
{
    public float fadeDuration = 10.0f; // Duration of the fade effect in seconds
    private TextMeshProUGUI textComponent; // Reference to the Text component
    public GameObject runeCollected; 

    void Start()
    {
        // Get the Text component attached to this GameObject
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Method to start the fade effect
    public void StartFade()
    {
        // Start the coroutine to fade the text
        StartCoroutine(FadeText());
    }

    // Coroutine to fade the text
    private IEnumerator FadeText()
    {
        Color currentColor = runeCollected.GetComponent<Image>().color;

        // Set the initial alpha value to 1 (fully opaque)
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 1f);

        // Define the target color with alpha set to 0 (fully transparent)
        Color targetColor = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0f);

        // Calculate the rate of change per second
        float rate = 1.0f / fadeDuration;

        // Initialize the elapsed time
        float elapsedTime = 0.0f;

        // Continue fading until the elapsed time exceeds the fade duration
        while (elapsedTime < fadeDuration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value using Lerp
            float alpha = Mathf.Lerp(textComponent.color.a, targetColor.a, elapsedTime * rate);
            currentColor.a = Mathf.Lerp(1f, 0f, elapsedTime * rate);

            runeCollected.GetComponent<Image>().color = currentColor;

            // Update the text color with the new alpha value
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the text color is fully transparent at the end of the fade
        textComponent.color = targetColor;
    }
}
