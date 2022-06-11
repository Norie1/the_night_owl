using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    [TextArea(1, 10)]
    public string[] sentences;
}

