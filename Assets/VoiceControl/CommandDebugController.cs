using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CommandDebugController : MonoBehaviour
{

    public static CommandDebugController instance;

    public Text hypothesis;
    public Text command;
    public Text pastCommands;
    public int numPastCommands = 11;

    List<string> pastCommandsList;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != this) {
            instance = this;
        }

        pastCommandsList = new List<string>();
        hypothesis.text = "";
        command.text = "";
        pastCommands.text = "";
    }

    public void newHypothesis(string hyp) {
        hypothesis.text = hyp;
    }

    public void newCommand(string com) {
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



    }
}
