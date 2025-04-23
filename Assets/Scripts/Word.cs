using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Word : MonoBehaviour
{
    [field: SerializeField] public string text { get; private set; }
    [field: SerializeField] public int damage { get; private set; }
    [field: SerializeField] public int level { get; private set; }
    [field: SerializeField] public float duration { get; private set; }
    [field: SerializeField] public float moveDistance { get; private set; }
    [field: SerializeField] public int exp { get; private set; }

    [field: SerializeField] public TextMeshProUGUI tmpro;
    [field: SerializeField] public RectTransform rectT;

    private RectTransform rectTransform;
        
    private float timer = 0f;
    private float terminalEndY;
    
    private void OnEnable()
    {
        GameEvent.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvent.OnGameOver -= OnGameOver;
    }

    private void Update()
    {
        Move();

        if (IsOffScreen())
        {
            GameEvent.RaiseOnWordAttack(this);
        }
    }

    private void Move()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0f;
            transform.position = new Vector3(transform.position.x,
                transform.position.y - moveDistance,
                transform.position.z);
        }
    }

    private bool IsOffScreen()
    {
        return rectT.anchoredPosition.y < terminalEndY;
    }

    public void Init(string text, int level, int damage, int duration, int moveDistance, int exp)
    {
        this.text = string.Intern(text);
        this.level = level;
        this.damage = damage;
        this.duration = duration;
        this.moveDistance = moveDistance;
        this.exp = exp;
        
        rectT.sizeDelta = new Vector2(text.Length * 13.34f, rectT.sizeDelta.y);

        tmpro.text = SyntaxHighlighter.Highlight(text);
    }
    public void SetPopY(float y)
    {
        terminalEndY = y;
    }
    
    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}
