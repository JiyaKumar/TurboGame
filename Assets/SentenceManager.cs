using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SentenceManager : MonoBehaviour
{
    public TextAsset[] csvFiles;
    public TextMeshProUGUI sentenceDisplayText;

    public float changeInterval = 5f; // ⏱ Change sentence every 5 seconds

    private List<string> sentences = new List<string>();
    private int currentFileIndex = 0;
    public string currentTargetWord;

    void Start()
    {
        LoadCSV(currentFileIndex);
        StartCoroutine(ShowRandomSentenceRoutine());
    }

    public void LoadCSV(int fileIndex)
    {
        if (csvFiles == null || fileIndex >= csvFiles.Length)
        {
            Debug.LogWarning("CSV file not assigned or index out of range.");
            return;
        }

        sentences.Clear();
        string[] lines = csvFiles[fileIndex].text.Split('\n');

        foreach (var line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
                sentences.Add(line.Trim());
        }
    }

    IEnumerator ShowRandomSentenceRoutine()
    {
        while (true)
        {
            ShowRandomSentence();
            yield return new WaitForSeconds(changeInterval);
        }
    }

    public void ShowRandomSentence()
    {
        if (sentences.Count > 0)
        {
            int randomIndex = Random.Range(0, sentences.Count);
            currentTargetWord = sentences[randomIndex];

            // Highlight with animation (flash yellow using rich text)
            StartCoroutine(AnimateSentence(currentTargetWord));
        }
        else
        {
            sentenceDisplayText.text = "<color=red>No sentences available.</color>";
        }
    }

    IEnumerator AnimateSentence(string sentence)
    {
        sentenceDisplayText.text = $"<color=yellow>{sentence}</color>"; // Flash highlight
        yield return new WaitForSeconds(0.5f);
        sentenceDisplayText.text = sentence; // Return to normal color
    }

    public void LoadNextFile()
    {
        currentFileIndex = (currentFileIndex + 1) % csvFiles.Length;
        LoadCSV(currentFileIndex);
    }
}
