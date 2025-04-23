using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class WordPool : MonoBehaviour
{
    [field: SerializeField] private RectTransform terminal;
    [field: SerializeField] private LevelConfig levelConfig;
    [field: SerializeField] private WordData wordData;
    [field: SerializeField] private Word wordPrefab;
    
    private Dictionary<int, List<Word>> words = new Dictionary<int, List<Word>>();
    private Dictionary<string, Word> spawned = new Dictionary<string, Word>();
    
    private int playerLevel = 1;
    private void OnEnable()
    {
        GameEvent.OnWordInput += TryRemoveWordByText;
        GameEvent.OnLevelChanged += SetPlayerLevel;
        GameEvent.OnWordAttack += ReturnWord;
    }

    private void OnDisable()
    {
        GameEvent.OnWordInput -= TryRemoveWordByText;
        GameEvent.OnLevelChanged -= SetPlayerLevel;
        GameEvent.OnWordAttack -= ReturnWord;
    }

    private void Awake()
    {
        LoadWordData();
    }

    private void LoadWordData()
    {
        for (int i = 1; i <= levelConfig.levelConfigs.Count; i++)
        {
            words.Add(i, new List<Word>());
        }
        
        foreach (var data in wordData.wordData)
        {
            string internKey = string.Intern(data.Key);
            int level = data.Value;
            
            Word word = Instantiate(wordPrefab);
            
            if (terminal)
            {
                word.transform.SetParent(terminal.transform, false);
            }
            word.Init(internKey,
                level,
                levelConfig.levelConfigs[level].damage,
                levelConfig.levelConfigs[level].duration,
                levelConfig.levelConfigs[level].moveDistance,
                levelConfig.levelConfigs[level].exp);
            words[level].Add(word);
            word.gameObject.SetActive(false);
        }
    }
    
    private void SetPlayerLevel(int level)
    {
        playerLevel = Mathf.Clamp(level, 1,  levelConfig.levelConfigs.Count);
    }
    
    public Word GetWord()
    {
        bool noWord = true;
        foreach (var wordlist in words)
        {
            if (wordlist.Key <= playerLevel && wordlist.Value.Count > 0)
            {
                noWord = false;
                break;
            }
        }

        if (noWord) return null;
        
        int level;
        int index;
        do
        {
            level = Random.Range(1, playerLevel + 1);
            index = Random.Range(0, words[level].Count);
        } while (words[level].Count == 0);
        
        Word word = words[level][index];
        words[level].RemoveAt(index);
        
        spawned.Add(string.Intern(word.text), word);

        return word;
    }

    public void ReturnWord(Word word)
    {
        words[word.level].Add(word);
        spawned.Remove(string.Intern(word.text));
        word.gameObject.SetActive(false);
    }

    private void TryRemoveWordByText(string str)
    {
        if (!spawned.TryGetValue(str, out Word value)) return;

        GameEvent.RaiseOnWordDestroyed(value.exp);
        ReturnWord(value);
        SoundEvent.PlaySound(SoundType.Pop);
    }
}
