using UnityEngine;
using Random = UnityEngine.Random;

public class WordSpawner : MonoBehaviour
{
    [field :SerializeField] private WordPool wordPool;
    [field: SerializeField] private RectTransform terminal;

    private float timer = 0f;
    public float duration;

    private float StartY;
    private float EndY;
    private float MinX;
    private float MaxX;

    private void OnEnable()
    {
        GameEvent.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvent.OnGameOver -= OnGameOver;
    }

    private void Awake()
    {
        GetRange();
    }

    private void Start()
    {
        enabled = true;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0f;
            SpawnWord();
        }
    }

    private void GetRange()
    {
        MinX = 40f;
        MaxX = terminal.rect.width - 40f;
        StartY = -40f;
        EndY = -terminal.rect.height + 90f;
    }
    
    private void SpawnWord()
    {
        Word word = wordPool.GetWord();

        if (!word) return;
        
        word.SetPopY(EndY);
        word.gameObject.SetActive(true);
        word.rectT.anchoredPosition = GetSpawnPosition(word);
    }

    private Vector3 GetSpawnPosition(Word word)
    {
        float endX = MaxX - word.rectT.sizeDelta.x;
        return new Vector3(Random.Range(MinX, endX), StartY, 0f);
    }
    
    private void OnGameOver()
    {
        enabled = false;
    }
}
