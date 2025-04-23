using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private float typingSpeed;

    public void Typing(string text)
    { 
        StartCoroutine(TypingEnumerator(text));
    }

    private IEnumerator TypingEnumerator(string text)
    {
        StringBuilder sb = new StringBuilder();
        this.text.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            sb.Append(text[i]);
            this.text.text = sb.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
