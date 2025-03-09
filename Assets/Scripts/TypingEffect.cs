using System.Collections;
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
        this.text.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            this.text.text += text[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
