using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Voice : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI spokenText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI feedbackText;
    public TMP_Dropdown levelDropdown;

    public void UpdateSpokenText(string spoken)
    {
        spokenText.text = spoken;

        // Example: check match with target sentence
        float accuracy = CalculateAccuracy(spoken, targetText.text);
        accuracyText.text = "Accuracy: " + Mathf.RoundToInt(accuracy * 100f) + "%";

        if (accuracy >= 0.9f)
        {
            feedbackText.text = "✅ Correct!";
        }
        else
        {
            feedbackText.text = "❌ Try again!";
        }
    }

    float CalculateAccuracy(string spoken, string target)
    {
        string[] spokenWords = spoken.ToLower().Split(' ');
        string[] targetWords = target.ToLower().Split(' ');

        int correct = 0;
        int count = Mathf.Min(spokenWords.Length, targetWords.Length);

        for (int i = 0; i < count; i++)
        {
            if (spokenWords[i] == targetWords[i])
            {
                correct++;
            }
        }

        return (float)correct / targetWords.Length;
    }
}
