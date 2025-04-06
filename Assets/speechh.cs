using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SpeechRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> actions;

    public Player_Movement playerMovement; // Reference to your car movement script

    void Start()
    {
        actions = new Dictionary<string, System.Action>();

        // Add words that will trigger movement
        actions.Add("start", MoveCar);
        actions.Add("go", MoveCar);
        actions.Add("drive", MoveCar);
        actions.Add("stop", StopCar);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnSpeechRecognized;
        keywordRecognizer.Start();
    }

    void OnSpeechRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Recognized: " + speech.text);
        actions[speech.text].Invoke();
    }

    void MoveCar()
    {
        Debug.Log("Car Moving!");
        playerMovement.speed = 2f; // Increase speed when the correct word is spoken
    }

    void StopCar()
    {
        Debug.Log("Car Stopped!");
        playerMovement.speed = 0f; // Stop the car if the word "stop" is spoken
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}

