using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New WordData", menuName = "WordData")]
public class WordData : ScriptableObject
{
    public SerializedDictionary<String, int> wordData = new  SerializedDictionary<String, int>();
}