using UnityEngine;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
#endif

public class SpeechRecognition : MonoBehaviour
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> actions;
#endif

    public Player_Movement playerMovement; // Reference to your car movement script

    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        actions = new Dictionary<string, System.Action>();

        // Add words that will trigger movement
        actions.Add("start", MoveCar);
        actions.Add("go", MoveCar);
        actions.Add("drive", MoveCar);
        actions.Add("stop", StopCar);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnSpeechRecognized;
        keywordRecognizer.Start();
#endif
    }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    void OnSpeechRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Recognized: " + speech.text);
        actions[speech.text].Invoke();
    }
#endif

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
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
#endif
    }
}
