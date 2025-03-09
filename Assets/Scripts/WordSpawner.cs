using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [field: SerializeField] GameObject prefab;
    [field: SerializeField] WordData wordData;
    private static readonly Dictionary<int, List<WordDataSlot>> words = new();
    public Dictionary<string, GameObject> spawned { get; private set; } = new Dictionary<string, GameObject>();

    private float timer;
    public float duration;

    private Player player;
    [field : SerializeField] public RectTransform terminal { get; private set; }
    [field: SerializeField] public Canvas canvas;

    private float StartY;
    private float MinX;
    private float MaxX;

    private void Awake()
    {
        player = GameManager.Instance.player;

        Vector3[] terminalCorners = new Vector3[4];
        terminal.GetWorldCorners(terminalCorners);
        
        StartY = terminalCorners[1].y - 70f;
        MinX = terminalCorners[1].x + prefab.GetComponent<RectTransform>().rect.width / 2 + 20f;
        MaxX = terminalCorners[2].x - prefab.GetComponent<RectTransform>().rect.width / 2 - 20f;
       
        LoadWordData();
    }

    private void Start()
    {
        enabled = true;
        timer = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0;
            SpawnWord();
        }
    }

    private void LoadWordData()
    {
        for (int i = 1; i <= WordData.MAXLEVEL; i++)
        {
            words.Add(i, new List<WordDataSlot>());
        }

        foreach (var worddata in wordData.wordData)
        {   
            words[worddata.level].Add(worddata);
        }
    }

    private void SpawnWord()
    {
        int level = Mathf.Clamp(Random.Range(1, player.PlayerLevel), 1, 5);
        WordDataSlot wordinfo = words[level][Random.Range(0, words[level].Count)];
        if (spawned.ContainsKey(wordinfo.word))
        {
            return;
        }
        

        GameObject word = Instantiate(prefab);
        word.transform.SetParent(canvas.transform, false);

        word.GetComponent<Word>().Init(wordinfo.word, wordinfo.level, terminal);

        word.transform.position = GetSpawnPosition(word);

        spawned.Add(wordinfo.word, word);
    }

    private Vector3 GetSpawnPosition(GameObject prefab)
    {
        return new Vector3(Random.Range(MinX, MaxX), StartY, 0f);   
    }

    public void DestroyWord(string word)
    {
        Destroy(spawned[word]);
        spawned.Remove(word);
    }
}
