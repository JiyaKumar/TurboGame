using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SentenceManager : MonoBehaviour
{
    public TextAsset[] csvFiles; // 👈 Now accepts multiple files
    public TextMeshProUGUI sentenceDisplayText;

    private List<string> sentences = new List<string>();
    private int currentFileIndex = 0;

    void Start()
    {
        LoadCSV(currentFileIndex); // Load the first one by default
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

        // Display the first sentence for now
        if (sentences.Count > 0)
            sentenceDisplayText.text = sentences[0];
    }

    public void LoadNextFile()
    {
        currentFileIndex = (currentFileIndex + 1) % csvFiles.Length;
        LoadCSV(currentFileIndex);
    }
}
