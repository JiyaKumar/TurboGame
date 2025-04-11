using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;

public class Speech : MonoBehaviour
{
    public TMP_Text targetText;
    public TMP_Text spokenText;
    public TMP_Text accuracyText;
    public TMP_Text feedbackText;
    public TMP_Dropdown levelDropdown;
    public CarController car; // 🔥 Link to your car controller

    private DictationRecognizer dictationRecognizer;
    private string currentTargetSentence = "";
    private string currentSpokenWords = "";
    private string[] targetWords;
    private bool isRecognizing = false;
    private List<string> currentLevelSentences;

    void Start()
    {
        LoadCSVForLevel(0); // Default to Beginner
        SetupDropdown();
        StartDictation();
    }

    void SetupDropdown()
    {
        levelDropdown.onValueChanged.AddListener(delegate {
            LoadCSVForLevel(levelDropdown.value);
        });
    }

    void LoadCSVForLevel(int levelIndex)
    {
        string fileName = "";
        switch (levelIndex)
        {
            case 0:
                fileName = "difficulty_levels";
                break;
            case 1:
                fileName = "themes";
                break;
            case 2:
                fileName = "word_length";
                break;
        }

        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile != null)
        {
            currentLevelSentences = csvFile.text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            SetNewTargetSentence();
        }
        else
        {
            Debug.LogError("CSV file not found for: " + fileName);
        }
    }

    void SetNewTargetSentence()
    {
        if (currentLevelSentences == null || currentLevelSentences.Count == 0)
        {
            targetText.text = "No sentences available";
            return;
        }
        currentTargetSentence = currentLevelSentences[UnityEngine.Random.Range(0, currentLevelSentences.Count)].Trim();
        targetWords = Regex.Split(currentTargetSentence, "\\s+");
        UpdateTargetTextUI();
        car.ResetSpeed(); // Reset car speed for new challenge
    }

    void StartDictation()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            currentSpokenWords = text;
            UpdateSpokenTextUI();
            CalculateAccuracy();
        };

        dictationRecognizer.DictationHypothesis += (text) =>
        {
            spokenText.text = text;
        };

        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogError("Dictation completed unsuccessfully: " + completionCause);
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogError("Dictation error: " + error);
        };

        dictationRecognizer.Start();
        isRecognizing = true;
    }

    void UpdateTargetTextUI()
    {
        string formatted = "";
        foreach (string word in targetWords)
        {
            formatted += "<color=white>" + word + " </color>";
        }
        targetText.text = formatted.Trim();
    }

    void UpdateSpokenTextUI()
    {
        string[] spokenWords = Regex.Split(currentSpokenWords.Trim(), "\\s+");
        string formatted = "";

        for (int i = 0; i < targetWords.Length; i++)
        {
            if (i < spokenWords.Length)
            {
                if (spokenWords[i].Equals(targetWords[i], StringComparison.OrdinalIgnoreCase))
                {
                    formatted += "<color=green>" + targetWords[i] + " </color>";
                }
                else
                {
                    formatted += "<color=red>" + targetWords[i] + " </color>";
                }
            }
            else
            {
                formatted += "<color=white>" + targetWords[i] + " </color>";
            }
        }

        spokenText.text = currentSpokenWords;
        targetText.text = formatted.Trim();
    }

    void CalculateAccuracy()
    {
        string[] spokenWords = Regex.Split(currentSpokenWords.Trim(), "\\s+");
        int correctCount = 0;

        for (int i = 0; i < targetWords.Length && i < spokenWords.Length; i++)
        {
            if (spokenWords[i].Equals(targetWords[i], StringComparison.OrdinalIgnoreCase))
            {
                correctCount++;
            }
        }

        float accuracy = ((float)correctCount / targetWords.Length) * 100f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("F1") + "%";

        if (accuracy == 100f)
        {
            feedbackText.text = "✅ Correct! Great job!";
            car.BoostSpeed(); // 🎯 Boost car when spoken perfectly
            Invoke("SetNewTargetSentence", 2f);
        }
        else
        {
            feedbackText.text = "❌ Try again!";
            car.SlowDown(); // 💢 Slow down car when it's not fully correct
        }
    }

    void OnApplicationQuit()
    {
        if (dictationRecognizer != null && isRecognizing)
        {
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
        }
    }
}
