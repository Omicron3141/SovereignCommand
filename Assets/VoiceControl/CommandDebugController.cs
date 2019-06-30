using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CommandDebugController : MonoBehaviour
{

    public static CommandDebugController instance;

    public Text hypothesis;
    public Text command;
    public Text pastCommands;
    public int numPastCommands = 11;
    public Button onoffButton;
    public Image confidence;

    private bool buttonOn = true;

    List<string> pastCommandsList;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != this) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        pastCommandsList = new List<string>();
        hypothesis.text = "";
        command.text = "";
        pastCommands.text = "";
    }

    public void newHypothesis(string hyp) {
        hypothesis.text = hyp;
    }

    public void newCommand(string com, ConfidenceLevel level) {
        pastCommandsList.Insert(0, command.text);

        if (pastCommandsList.Count > numPastCommands) {
            pastCommandsList.RemoveAt(pastCommandsList.Count-1);
        }

        hypothesis.text = "";
        command.text = com;

        pastCommands.text = "";
        foreach (string pastCom in pastCommandsList) {
            pastCommands.text += pastCom;
            pastCommands.text += "\n";
        }

        if (level == ConfidenceLevel.High) {
            confidence.color = Color.green;
        } else if (level == ConfidenceLevel.Medium) {
            confidence.color = Color.yellow;
        } else if (level == ConfidenceLevel.Low) {
            confidence.color = Color.red;
        }else if (level == ConfidenceLevel.Rejected) {
            confidence.color = Color.black;
        }



    }

    public void listenerStatusChanged(bool on) {
        buttonOn = on;
        ColorBlock buttonColors = onoffButton.colors;
        if (on) {
            buttonColors.normalColor = Color.green;
            buttonColors.selectedColor = Color.green;
        } else {
            buttonColors.normalColor = Color.red;
            buttonColors.selectedColor = Color.red;
        }
        onoffButton.colors = buttonColors;
    }

    public void onoffButtonPressed() {
        CommandListener.instance.DictationEnabled = !buttonOn;
        listenerStatusChanged(!buttonOn);
    }
}
