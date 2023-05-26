using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class DialogueData
{
    public string name;
    public Sprite portrait;
    [TextArea(1, 2)]
    public string sentences;

    public List<string> buttonsAnswers;
    public List<int> afterAnswerIndexes;
}

[System.Serializable]
public class FinalDialogueData
{
    public string name;
    public Sprite portrait;
    [TextArea(1, 2)]
    public string sentences;

}