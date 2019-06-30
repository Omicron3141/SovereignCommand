using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CommandListener : MonoBehaviour
{

    DictationRecognizer dictationRecognizer;

    public bool DictationEnabled {
        get {
            return dictationRecognizer.Status == SpeechSystemStatus.Running;
        }
        set {
            if (value && dictationRecognizer.Status == SpeechSystemStatus.Stopped) {
                dictationRecognizer.Start();
            } else if (!value && dictationRecognizer.Status == SpeechSystemStatus.Running) {
                dictationRecognizer.Stop();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += dictationResult;

        dictationRecognizer.DictationHypothesis += dictationHypothesis;

        dictationRecognizer.DictationComplete += dictationComplete;

        dictationRecognizer.DictationError += dictationError;

        dictationRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void dictationResult(string text, ConfidenceLevel confidence) {
        CommandDebugController.instance.newCommand(text);
    }

    private void dictationHypothesis(string text) {
        CommandDebugController.instance.newHypothesis(text);
    }

    private void dictationComplete(DictationCompletionCause cause) {
        if (cause != DictationCompletionCause.Complete)
            Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", cause);
    }

    private void dictationError(string error, int hresult) {
        Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
    }

    void OnDestroy() {
        dictationRecognizer.DictationResult -= dictationResult;
        dictationRecognizer.DictationComplete -= dictationComplete;
        dictationRecognizer.DictationHypothesis -= dictationHypothesis;
        dictationRecognizer.DictationError -= dictationError;
        dictationRecognizer.Dispose();
    }
}
