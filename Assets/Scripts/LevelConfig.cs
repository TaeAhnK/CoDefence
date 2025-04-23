using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializedDictionary("Level", "Config")]
    public SerializedDictionary<int, LevelConfigSlot> levelConfigs = new SerializedDictionary<int, LevelConfigSlot>();
}

[System.Serializable]
public class LevelConfigSlot
{
    public int duration;
    public int moveDistance;
    public int damage;
    public int exp;
}