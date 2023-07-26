using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using TMPro;
using Microsoft.CognitiveServices.Speech.Translation;

public class Translate : MonoBehaviour
{
    private SpeechTranslationConfig speechTranslationConfig;
    private TranslationRecognizer translationRecognizer;

    public Button speakButton;
    public TMP_InputField outputText;

    private bool waitingForTranslate;
    private bool isReadyForText;

    private object threadLocker = new object();
    //private bool micPermissionGranted = false;
    private string message;

  //  [SerializeField] string[] inputLangrage = { "ja-JP", "de-DE", "zh-CN" };
    //string str = "";
    void Start()
    {
        if (outputText == null)
        {
            Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (speakButton == null)
        {
           // message = "speakButton property is null! Assign a UI Button to it.";
            Debug.LogError("speakButton property is null! Assign a UI Button to it.");
        }
    }
    void Update()
    {
        lock (threadLocker)
        {
            if (speakButton != null)
            {
                speakButton.interactable = !waitingForTranslate;//&& micPermissionGranted;
            }
            if (outputText != null && isReadyForText)
            {
                outputText.text = message;
                message = string.Empty;
                isReadyForText = false;
            }
        }

    }
    async public void ButtonClick_Translate()
    {
        
        
        // Creates an instance of a speech config with specified subscription key and service region.
        speechTranslationConfig = SpeechTranslationConfig.FromSubscription(CloudServiseKeyManager.Instance.Get_Key, CloudServiseKeyManager.Instance.Get_Region);
        speechTranslationConfig.SpeechRecognitionLanguage = "ja-JP";
       // AutoDetectSourceLanguageConfig auto = AutoDetectSourceLanguageConfig.FromLanguages(inputLangrage);
        //target language
        speechTranslationConfig.AddTargetLanguage("en-US");
        // Creates a speech translator.
        // Make sure to dispose the translator after use!
        using (var translation_Recognizer = new TranslationRecognizer(speechTranslationConfig)) { 
        
            lock (threadLocker)
        {
            waitingForTranslate = true;
                isReadyForText = false;
        }



        translationRecognizer = new TranslationRecognizer(speechTranslationConfig);
            var result = await translationRecognizer.RecognizeOnceAsync().ConfigureAwait(false);
        
        switch (result.Reason)
        {
            case ResultReason.TranslatedSpeech:
#if UNITY_EDITOR
                    Debug.Log($"RECOGNIZED: Text={result.Text}");
#endif

                    foreach (var element in result.Translations)
                {
#if UNITY_EDITOR
                        Debug.Log($"TRANSLATED into '{element.Key}': {element.Value}");
#endif
                        message = element.Value;
                }
                
                break;
            case ResultReason.NoMatch:
                Debug.Log($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(result);
                Debug.Log($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Debug.Log($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Debug.Log($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
        }
            lock (threadLocker)
            {
                waitingForTranslate = false;
                isReadyForText = true;
            }
        }
    }
}
