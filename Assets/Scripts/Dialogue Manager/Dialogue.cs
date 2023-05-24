using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite portrait;
    [TextArea(1, 2)]
    public string sentences;
    public bool isQuestion;
    public List<DialogueQuestion> questions;
}

[System.Serializable]
public class LastDialogue
{
    public string lastName;
    public Sprite lastPortrait;
    [TextArea(1, 2)]
    public string lastSentences;
}

[System.Serializable]
public class DialogueQuestion
{
    public string Name;
    public Sprite Portrait;
    [TextArea(1, 2)]
    public string question;
    public List<DialogueAnswer> answers;

    public AfterYesAswer afterYesAnswer;
    public AfterNoAswer afterNoAnswer;
}

[System.Serializable]
public class DialogueAnswer
{
    [TextArea(1, 2)]
    public string answer;
}

[System.Serializable]
public class AfterYesAswer
{
    public string Name;
    public Sprite Portrait;
    [TextArea(1, 2)]
    public string Sentences;
}
[System.Serializable]
public class AfterNoAswer
{
    public string Name;
    public Sprite Portrait;
    [TextArea(1, 2)]
    public string Sentences;
}