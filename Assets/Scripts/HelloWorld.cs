//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
// <code>
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class HelloWorld : MonoBehaviour
{
    enum VoiceName
    {
        Jenny,
        Guy,
        Sara,
        Tony
    }
    [Header("VoiceName")]
    [SerializeField] VoiceName voiceName = VoiceName.Sara;
    public bool isJapanese=false;
    // Hook up the three properties below with a Text, InputField and Button object in your UI.
    public TMP_Text outputText;
    public TMP_InputField inputField;
    public Button speakButton;
    public AudioSource audioSource;

    // Replace with your own subscription key and service region (e.g., "westus").

    private const int SampleRate = 24000;

    private object threadLocker = new object();
    private bool waitingForSpeak;
    private bool audioSourceNeedStop;
    private string message;
    [SerializeField] bool useSpeech = true;
    [SerializeField] TMP_Text[] _useSpeechText;

    private SpeechConfig speechConfig;
    private SpeechSynthesizer synthesizer;


    //public void ButtonClick()
    //{
    //    lock (threadLocker)
    //    {
    //        waitingForSpeak = true;
    //    }

    //    string newMessage = null;
    //    var startTime = DateTime.Now;

    //    // Starts speech synthesis, and returns once the synthesis is started.
    //    using (var result = synthesizer.StartSpeakingTextAsync(inputField.text).Result)
    //    {
    //        // Native playback is not supported on Unity yet (currently only supported on Windows/Linux Desktop).
    //        // Use the Unity API to play audio here as a short term solution.
    //        // Native playback support will be added in the future release.
    //        var audioDataStream = AudioDataStream.FromResult(result);
    //        var isFirstAudioChunk = true;
    //        var audioClip = AudioClip.Create(
    //            "Speech",
    //            SampleRate * 600, // Can speak 10mins audio as maximum
    //            1,
    //            SampleRate,
    //            true,
    //            (float[] audioChunk) =>
    //            {
    //                var chunkSize = audioChunk.Length;
    //                var audioChunkBytes = new byte[chunkSize * 2];
    //                var readBytes = audioDataStream.ReadData(audioChunkBytes);
    //                if (isFirstAudioChunk && readBytes > 0)
    //                {
    //                    var endTime = DateTime.Now;
    //                    var latency = endTime.Subtract(startTime).TotalMilliseconds;
    //                    newMessage = $"Speech synthesis succeeded!\nLatency: {latency} ms.";
    //                    isFirstAudioChunk = false;
    //                }

    //                for (int i = 0; i < chunkSize; ++i)
    //                {
    //                    if (i < readBytes / 2)
    //                    {
    //                        audioChunk[i] = (short)(audioChunkBytes[i * 2 + 1] << 8 | audioChunkBytes[i * 2]) / 32768.0F;
    //                    }
    //                    else
    //                    {
    //                        audioChunk[i] = 0.0f;
    //                    }
    //                }

    //                if (readBytes == 0)
    //                {
    //                    Thread.Sleep(200); // Leave some time for the audioSource to finish playback
    //                    audioSourceNeedStop = true;
    //                }
    //            });

    //        audioSource.clip = audioClip;
    //        audioSource.PlayOneShot(audioClip);
    //    }

    //    lock (threadLocker)
    //    {
    //        if (newMessage != null)
    //        {
    //            message = newMessage;
    //        }

    //        waitingForSpeak = false;
    //    }
    //}

    void Start()
    {
        if (_useSpeechText != null)
        {
            UseSpeechChecker();
        }
        if (outputText == null)
        {
            Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (inputField == null)
        {
            message = "inputField property is null! Assign a UI InputField element to it.";
            Debug.LogError(message);
        }
        else if (speakButton == null)
        {
            message = "speakButton property is null! Assign a UI Button to it.";
            Debug.LogError(message);
        }
        else
        {
            // Continue with normal initialization, Text, InputField and Button objects are present.
            // inputField.text = "Enter text you wish spoken here.";
            message = "Click button to synthesize speech";
            // speakButton.onClick.AddListener(ButtonClick);

            // Creates an instance of a speech config with specified subscription key and service region.
            speechConfig = SpeechConfig.FromSubscription(CloudServiseKeyManager.Instance.Get_Key, CloudServiseKeyManager.Instance.Get_Region);
            string str;
            switch (voiceName)
            {
                case VoiceName.Jenny:
                    str = "en-US-JennyNeural";
                    InitializeVoise(str);
                   // speechConfig.SpeechSynthesisVoiceName = str;
                    break;
                case VoiceName.Guy:
                    str = "en-US-GuyNeural";
                    InitializeVoise(str);
                    //speechConfig.SpeechSynthesisVoiceName = str;
                    break;
                case VoiceName.Sara:
                    str = "en-US-SaraNeural";
                    InitializeVoise(str);
                   // speechConfig.SpeechSynthesisVoiceName = str;
                    break;
                case VoiceName.Tony:
                    str = "en-US-TonyNeural";
                    InitializeVoise(str);
                   // speechConfig.SpeechSynthesisVoiceName = str;
                    break;

            }


            // The default format is RIFF, which has a riff header.
            // We are playing the audio in memory as audio clip, which doesn't require riff header.
            // So we need to set the format to raw (24KHz for better quality).
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw24Khz16BitMonoPcm);

            // Creates a speech synthesizer.
            // Make sure to dispose the synthesizer after use!
           // synthesizer = new SpeechSynthesizer(speechConfig, null);

            synthesizer.SynthesisCanceled += (s, e) =>
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(e.Result);
                message = $"CANCELED:\nReason=[{cancellation.Reason}]\nErrorDetails=[{cancellation.ErrorDetails}]\nDid you update the subscription info?";
            };
        }
    }

    //void Update()
    //{
    //    //lock (threadLocker)
    //    //{
    //    //    if (speakButton != null)
    //    //    {
    //    //        speakButton.interactable = !waitingForSpeak;
    //    //    }
    //    //    if (audioSourceNeedStop)
    //    //    {
    //    //        audioSource.Stop();
    //    //        audioSourceNeedStop = false;
    //    //    }
    //    //}
    //}

    void OnDestroy()
    {
        if (synthesizer != null)
        {
            synthesizer.Dispose();
        }
    }
    public async void PlayTextVoice()
    {
        if (!useSpeech)
            return;
        //lock (threadLocker)
        //{
        //    waitingForSpeak = true;
        //}

        var audioclip = await GetVoiceData_Wait();
        audioSource.PlayOneShot(audioclip);

        //lock (threadLocker)
        //{
        //    waitingForSpeak = false;
        //}
    }
    public async void PlayWeatherVoice(string text)
    {
        speakButton.interactable = false;
        //lock (threadLocker)
        //{
        //    waitingForSpeak = true;
        //}

        var audioclip = await GetVoiceData_Wait(text);
        if (audioSource.isPlaying)
        { audioSource.Stop(); }
        audioSource.PlayOneShot(audioclip);
        speakButton.interactable = true;
        //lock (threadLocker)
        //{
        //    waitingForSpeak = false;
        //}
    }
    async Task<AudioClip> GetVoiceData_Wait()
    {
        var startTime = DateTime.Now;

        // Starts speech synthesis, and returns once the synthesis is started.
        using (var result = synthesizer.StartSpeakingTextAsync(inputField.text).Result)
        {
            // Native playback is not supported on Unity yet (currently only supported on Windows/Linux Desktop).
            // Use the Unity API to play audio here as a short term solution.
            // Native playback support will be added in the future release.
            var audioDataStream = AudioDataStream.FromResult(result);
            var isFirstAudioChunk = true;
            var audioClip = AudioClip.Create(
                "Speech",
                SampleRate * 600, // Can speak 10mins audio as maximum
                1,
                SampleRate,
                true,
                (float[] audioChunk) =>
                {
                    var chunkSize = audioChunk.Length;
                    var audioChunkBytes = new byte[chunkSize * 2];
                    var readBytes = audioDataStream.ReadData(audioChunkBytes);
                    if (isFirstAudioChunk && readBytes > 0)
                    {
                        var endTime = DateTime.Now;
                        var latency = endTime.Subtract(startTime).TotalMilliseconds;
                        isFirstAudioChunk = false;
                    }

                    for (int i = 0; i < chunkSize; ++i)
                    {
                        if (i < readBytes / 2)
                        {
                            audioChunk[i] = (short)(audioChunkBytes[i * 2 + 1] << 8 | audioChunkBytes[i * 2]) / 32768.0F;
                        }
                        else
                        {
                            audioChunk[i] = 0.0f;
                        }
                    }

                    if (readBytes == 0)
                    {
                        //Thread.Sleep(200); // Leave some time for the audioSource to finish playback
                        audioSourceNeedStop = true;
                    }
                });
            await Task.Delay(500);
            audioSource.clip = audioClip;
        }
        return audioSource.clip;
    }
    async Task<AudioClip> GetVoiceData_Wait(string text)
    {
        var startTime = DateTime.Now;

        // Starts speech synthesis, and returns once the synthesis is started.
        using (var result = synthesizer.StartSpeakingTextAsync(text).Result)
        {
            // Native playback is not supported on Unity yet (currently only supported on Windows/Linux Desktop).
            // Use the Unity API to play audio here as a short term solution.
            // Native playback support will be added in the future release.
            var audioDataStream = AudioDataStream.FromResult(result);
            var isFirstAudioChunk = true;
            var audioClip = AudioClip.Create(
                "Speech",
                SampleRate * 600, // Can speak 10mins audio as maximum
                1,
                SampleRate,
                true,
                (float[] audioChunk) =>
                {
                    var chunkSize = audioChunk.Length;
                    var audioChunkBytes = new byte[chunkSize * 2];
                    var readBytes = audioDataStream.ReadData(audioChunkBytes);
                    if (isFirstAudioChunk && readBytes > 0)
                    {
                        var endTime = DateTime.Now;
                        var latency = endTime.Subtract(startTime).TotalMilliseconds;
                        isFirstAudioChunk = false;
                    }

                    for (int i = 0; i < chunkSize; ++i)
                    {
                        if (i < readBytes / 2)
                        {
                            audioChunk[i] = (short)(audioChunkBytes[i * 2 + 1] << 8 | audioChunkBytes[i * 2]) / 32768.0F;
                        }
                        else
                        {
                            audioChunk[i] = 0.0f;
                        }
                    }

                    if (readBytes == 0)
                    {
                        //Thread.Sleep(200); // Leave some time for the audioSource to finish playback
                        audioSourceNeedStop = true;
                    }
                });
            await Task.Delay(500);
            audioSource.clip = audioClip;
        }
        return audioSource.clip;
    }
    public void UseSpeechChecker()
    {
        if (useSpeech == true)
        {
            useSpeech = !useSpeech;// "StopSpeech"
            ButtonContentChange("UseSpeech");
        }
        else
        {
            useSpeech = !useSpeech;//useSpeech'
            ButtonContentChange("StopSpeech");
        }
    }
    void ButtonContentChange(string text)
    {
        foreach (var item in _useSpeechText)
        {
            item.text = text;
        }
        // tMP_Texts[].text = text;
    }
    public void VoiseSelect(int num)
    {
        string str;
        switch (num)
        {
            case 0:
                str = "en-US-JennyNeural";
                isJapanese = false;
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 1:
                str = "en-US-GuyNeural";
                isJapanese = false;
                speechConfig.SetServiceProperty("style", "sad", ServicePropertyChannel.UriQueryParameter);
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 2:
                str = "en-US-SaraNeural";
                isJapanese = false;
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 3:
                str = "en-US-TonyNeural";
                isJapanese = false;
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 4:
                str = "ja-JP-NanamiNeural";
                isJapanese = true;
               // speechConfig.SetServiceProperty("style", "cheerful",ServicePropertyChannel.HttpHeader);
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 5:
                str = "en-US-JasonNeural";
                isJapanese = false;
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 6:
                str = "en-GB-SoniaNeural";
                isJapanese = false;
                InitializeVoise(str);
                //speechConfig.SpeechSynthesisVoiceName = str;
                //synthesizer = new SpeechSynthesizer(speechConfig, null);
                break;
            case 7:
                str = "ja-JP-DaichiNeural";
                isJapanese = true;
                InitializeVoise(str);
                break;
        }
    }
    void InitializeVoise(string str)
    {
        speechConfig.SpeechSynthesisVoiceName = str;
        synthesizer = new SpeechSynthesizer(speechConfig, null);
    }
}
// </code>
