using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public TextMeshProUGUI sentenceText;

    public void UpdateSentence(string sentence)
    {
        Debug.Log("Received from Flutter: " + sentence);
        if (sentenceText != null)
        {
            sentenceText.text = sentence;
        }
    }
}
