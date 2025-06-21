using System;
using System.Collections;
using UnityEngine;

public class HitLine : MonoBehaviour
{
    private bool isActiveAnimation = false;

    public void ChangeHitZoneAnimationToReturn() => StartCoroutine(this.HitLineAnimation(0.15f, 3.5f, this.SetActivateAnimation));

    private IEnumerator HitLineAnimation(float targetAlpha, float duration, Action onComplete = null)
    {
        this.isActiveAnimation = true;
        SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
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

    public void ChangeHitZoneAnimationStart()
    {
        if (this.isActiveAnimation) return;
        StartCoroutine(this.HitLineAnimation(0.5f, 2f, this.ChangeHitZoneAnimationToReturn));
    }


    private void SetActivateAnimation()
    {
        this.isActiveAnimation = false;
    }
}
