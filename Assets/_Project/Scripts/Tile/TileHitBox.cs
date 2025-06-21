using UnityEngine;

public class TileHitBox : MonoBehaviour, IHitPoint
{
    private TileController tileController;

    public bool isTrigger = false;
    private float targetTime;
    public bool isVisualActive = false;

    private void Start()
    {
        this.tileController = GetComponent<TileController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isVisualActive) return;

        HitLine hitLine = collision.GetComponent<HitLine>();
        if (hitLine == null) return;

        this.isTrigger = true;
        this.targetTime = Time.time;
        hitLine.ChangeHitZoneAnimationStart();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        HitLine hitLine = collision.GetComponent<HitLine>();
        if (hitLine == null) return;

        this.isTrigger = false;
    }

    public void TakeScore(float tapTime)
    {
        if (!this.isTrigger && this.isVisualActive) return;
        this.RatingScore(tapTime, this.targetTime);

        this.isVisualActive = true;
        this.tileController.visual.TouchEffect(this.ReturnTileVisual);
    }

    private void RatingScore(float tapTime, float timeRating)
    {
        float timeOffset = Mathf.Abs(tapTime - timeRating);
        int score = 0;
        RatingType ratingType = RatingType.MISS;

        if (timeOffset >= 0.05f && timeOffset <= 0.15f)
        {
            score = 100;
            ratingType = RatingType.PERFECT;
        }
        else if (timeOffset > 0.15f && timeOffset <= 0.35f)
        {
            score = 70;
            ratingType = RatingType.GREAT;
        }
        else if (timeOffset > 0.35f && timeOffset <= 0.5f)
        {
            score = 50;
            ratingType = RatingType.GOOD;
        }

        GameManager.Instance.UpdateScore(score, ratingType);
    }

    private void ReturnTileVisual()
    {
        this.isVisualActive = false;

        this.tileController.visual.ReturnEffectTile();

        this.tileController.ReturnTile();
    }
}
