using System.Collections;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public void TouchEffect(System.Action onComplete)
    {
        StartCoroutine(this.ChangeImageAlpha(0, 1f, onComplete));
    }

    private IEnumerator ChangeImageAlpha(float targetAlpha, float duration, System.Action onComplete)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float time = 0;
        Color currentColor = spriteRenderer.color;
        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        onComplete?.Invoke();
    }

    public void ReturnEffectTile()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
    }
}
