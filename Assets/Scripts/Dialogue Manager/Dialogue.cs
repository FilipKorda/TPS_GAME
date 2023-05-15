using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("==== First Person ====")]
    [Space(10)]
    public string Name1;
    public Sprite Image1;
    [TextArea(1, 1)]
    public string[] sentences1;

    [Header("==== Second Person ====")]
    [Space(10)]
    public string Name2;
    public Sprite Image2;
    [TextArea(1, 1)]
    public string[] sentences2;

}
