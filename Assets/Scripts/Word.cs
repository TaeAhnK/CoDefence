using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [field: SerializeField] public string text { get; private set; }
    [field: SerializeField] public int damage { get; private set; }
    [field: SerializeField] public int level { get; private set; }
    [field: SerializeField] public float duration { get; private set; }
    [field: SerializeField] public float moveDistance { get; private set; } = 50f;
    [field: SerializeField] public RectTransform terminal;

    public TextMeshProUGUI tmpro;

    private RectTransform rectTransform;

    private float timer = 0f;
    private Vector3[] corners = new Vector3[4];
    private float terminalEndY;

    #region private Dictionary<string, string> keywordColors;
    private static readonly Dictionary<string, string> keywordColors = new Dictionary<string, string>
    {
        {"int", "<color=#569CD6>"},
        {"void", "<color=#569CD6>"},
        {"char", "<color=#569CD6>"},
        {"bool", "<color=#569CD6>"},
        {"float", "<color=#569CD6>"},
        {"unsigned", "<color=#569CD6>"},
        {"long", "<color=#569CD6>"},
        {"typename", "<color=#569CD6>"},
        {"typedef", "<color=#569CD6>"},
        {"class", "<color=#569CD6>"},
        {"template", "<color=#569CD6>"},
        {"const", "<color=#569CD6>"},
        {"string", "<color=#569CD6>"},
        {"virtual", "<color=#569CD6>"},
        {"override", "<color=#569CD6>"},

        {"enable_if_t", "<color=#569CD6>"},
        {"unordered_map", "<color=#569CD6>"},
        {"vector", "<color=#569CD6>"},
        {"pair", "<color=#569CD6>"},
        {"map", "<color=#569CD6>"},
        {"queue", "<color=#569CD6>"},
        {"array", "<color=#569CD6>"},
        {"tuple", "<color=#569CD6>"},
        {"mutex", "<color=#569CD6>"},

        {"if", "<color=#C586C0>"},
        {"for", "<color=#C586C0>"},
        {"while", "<color=#C586C0>"},
        {"switch", "<color=#C586C0>"},
        {"do", "<color=#C586C0>"},
        {"return", "<color=#C586C0>"},

        {"namespace", "<color=#569CD6>"},
        {"static_cast", "<color=#569CD6>"},
        {"static_assert", "<color=#569CD6>"},
    };
    #endregion

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        Move();

        if (IsOffScreen())
        {
            GameManager.Instance.player.Damage(damage);
            GameManager.Instance.spawner.DestroyWord(text);
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
        rectTransform.GetWorldCorners(corners);
        return corners[0].y < terminalEndY;
    }

    private string SyntaxHighlight(string text)
    {
        string result = text;
        foreach (var keyword in keywordColors)
        {
            result = result.Replace(
                keyword.Key,
                $"{keyword.Value}{keyword.Key}</color>"
            );
        }
        return result;
    }

    public void Init(string text, int level, RectTransform terminal)
    {
        this.text = text;
        this.level = Mathf.Clamp(level, 0, WordData.MAXLEVEL);
        this.terminal = terminal;

        rectTransform = GetComponent<RectTransform>();

        Vector3[] terminalCorners = new Vector3[4];
        terminal.GetWorldCorners(terminalCorners);
        terminalEndY = terminalCorners[0].y;

        switch (level)
        {
            case 1:
                damage = 5;
                duration = 2f;
                moveDistance = 50f;
                break;
            case 2:
                damage = 10;
                duration = 2f;
                moveDistance = 50f;
                break;
            case 3:
                damage = 15;
                duration = 3f;
                moveDistance = 50f;
                break;
            case 4:
                damage = 25;
                duration = 3f;
                moveDistance = 50f;
                break;
            case 5:
                damage = 30;
                duration = 3f;
                moveDistance = 50f;
                break;
            default:
                break;
        }

        tmpro.text = SyntaxHighlight(text);
    }
}
