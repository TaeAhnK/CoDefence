using System.Collections;
using UnityEngine;

public class PanelBlinkEffect : MonoBehaviour
{
    [SerializeField] int blinkCount;
    [SerializeField] float blinkInterval;

    [SerializeField] CanvasGroup canvasgroup;

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            canvasgroup.alpha = 0f;
            yield return new WaitForSeconds(blinkInterval);

            canvasgroup.alpha = 1f;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
