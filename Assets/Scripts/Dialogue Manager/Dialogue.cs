using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite portrait;
    [TextArea(1, 2)]
    public List<string> sentences;
    public bool isQuestion;
    public List<DialogueChoice> choices;
}

[System.Serializable]
public class LastDialogue
{
    public string lastName;
    public Sprite lastPortrait;
    [TextArea(1, 2)]
    public List<string> lastSentences;
    public List<DialogueChoice> choices;
}
[System.Serializable]
public class DialogueChoice
{
    [TextArea(1, 2)]
    public string optionText;
    [TextArea(1, 2)]
    public List<string> sentences;
}

