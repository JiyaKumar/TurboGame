using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class VoiceGameManager : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI spokenText;
    public TextMeshProUGUI accuracyText;

    private string[] targetWords;
    private List<string> spokenWords = new List<string>();

    private bool isListening = false;

    void Start()
    {
        // Set a test sentence
        SetTargetSentence("Drive fast but safe");
    }

    public void SetTargetSentence(string sentence)
    {
        targetText.text = sentence;
        targetWords = sentence.Split(' ');
    }

    public void OnWordRecognized(string word)
    {
        if (!isListening) return;

        spokenWords.Add(word);

        UpdateSpokenTextUI();
        UpdateAccuracy();
    }

    private void UpdateSpokenTextUI()
    {
        string[] result = new string[spokenWords.Count];

        for (int i = 0; i < spokenWords.Count; i++)
        {
            string correctWord = i < targetWords.Length ? targetWords[i] : "";
            string userWord = spokenWords[i];

            if (userWord.Equals(correctWord, System.StringComparison.InvariantCultureIgnoreCase))
                result[i] = $"<color=green>{userWord}</color>";
            else
                result[i] = $"<color=red>{userWord}</color>";
        }

        spokenText.text = string.Join(" ", result);
    }

    private void UpdateAccuracy()
    {
        int correctCount = 0;

        for (int i = 0; i < spokenWords.Count && i < targetWords.Length; i++)
        {
            if (spokenWords[i].Equals(targetWords[i], System.StringComparison.InvariantCultureIgnoreCase))
                correctCount++;
        }

        float accuracy = (targetWords.Length == 0) ? 0f : ((float)correctCount / targetWords.Length) * 100f;
        accuracyText.text = $"Accuracy: {accuracy:F1}%";
    }

    // Call this when starting recognition
    public void StartListening()
    {
        isListening = true;
        spokenWords.Clear();
        spokenText.text = "";
        accuracyText.text = "Accuracy: 0%";
    }

    public void StopListening()
    {
        isListening = false;
    }

    public void OnAndroidSpeechResult(string result)
{
    OnWordRecognized(result); // Call your existing function to process the word
}

}
