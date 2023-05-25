using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite portrait;
    [TextArea(1, 2)]
    public string sentences;    
}

[System.Serializable]
public class LastDialogue
{
    public string lastName;
    public Sprite lastPortrait;
    [TextArea(1, 2)]
    public string lastSentences;
}

