using UnityEngine;

public class VoiceBridge : MonoBehaviour
{
    private AndroidJavaObject speechPlugin;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            speechPlugin = new AndroidJavaObject("com.varun.speechplugin.SpeechPlugin", activity);
        }
#endif
    }

    public void StartListening()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        speechPlugin.Call("StartListening");
#endif
    }

    public void StopListening()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        speechPlugin.Call("StopListening");
#endif
    }

    // Called by Android Java
    public void OnAndroidSpeechResult(string recognizedText)
    {
        Debug.Log("Speech Result: " + recognizedText);
        FindObjectOfType<VoiceGameManager>().OnWordRecognized(recognizedText);
    }
}

