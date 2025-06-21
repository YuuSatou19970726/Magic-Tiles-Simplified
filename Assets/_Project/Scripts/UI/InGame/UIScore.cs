using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] ratingImages;

    private RatingType currentRatingType = RatingType.MISS;
    private bool isRatingActive = false;

    public void UpdateScore(int score, RatingType ratingType)
    {
        this.scoreText.text = "" + score;

        if (this.currentRatingType != ratingType)
        {
            for (int i = 0; i < this.ratingImages.Length; i++)
                this.ReturnImageAlpha(i);

            this.currentRatingType = ratingType;
            this.isRatingActive = false;
        }

        if (!this.isRatingActive)
            StartCoroutine(this.ChangeImageAlpha(1f, 0.75f, (int)ratingType, () => this.ReturnImageAlpha((int)ratingType)));

    }

    private IEnumerator ChangeImageAlpha(float targetAlpha, float duration, int indexImage, System.Action onComplete)
    {
        this.isRatingActive = true;
        float time = 0;
        Color currentColor = ratingImages[indexImage].color;
        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            ratingImages[indexImage].color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        ratingImages[indexImage].color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        onComplete?.Invoke();
    }

    private void ReturnImageAlpha(int indexImage)
    {
        Color currentColor = ratingImages[indexImage].color;
        ratingImages[indexImage].color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        this.isRatingActive = false;
    }
}