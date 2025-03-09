using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WordData", menuName = "WordData")]
public class WordData : ScriptableObject
{
    public const int MAXLEVEL = 4;
    public List<WordDataSlot> wordData = new List<WordDataSlot>();
}

[Serializable]
public class WordDataSlot
{
    public int level;
    public string word;
}
