using UnityEngine;

public class TileHitBox : MonoBehaviour, IHitPoint
{
    private TileController tileController;

    public bool isTrigger = false;
    private float targetTime;

    private void Start()
    {
        this.tileController = GetComponent<TileController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
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
        if (!this.isTrigger) return;
        this.RatingScore(tapTime, this.targetTime);
        this.tileController.ReturnTile();
    }

    private void RatingScore(float tapTime, float timeRating)
    {
        float timeOffset = Mathf.Abs(tapTime - timeRating);
        int score = 0;
        string rating = RatingTags.MISS;

        if (timeOffset <= 0.05f)
        {
            score = 100;
            rating = RatingTags.PERFECT;
        }
        else if (timeOffset <= 0.1f)
        {
            score = 70;
            rating = RatingTags.GREAT;
        }
        else if (timeOffset <= 0.2f)
        {
            score = 50;
            rating = RatingTags.GOOD;
        }

        GameManager.Instance.UpdateScore(score, rating);
    }
}
