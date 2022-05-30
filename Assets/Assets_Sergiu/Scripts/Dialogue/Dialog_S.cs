using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog_S
{
    public string name;
    [TextArea(1, 10)]
    public string[] sentences;
}
