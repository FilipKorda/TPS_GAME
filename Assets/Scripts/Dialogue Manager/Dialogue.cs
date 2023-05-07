using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(1, 1)]
    public string[] sentences;
}