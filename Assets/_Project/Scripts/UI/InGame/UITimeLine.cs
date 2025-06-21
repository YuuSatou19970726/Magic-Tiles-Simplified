using UnityEngine;
using UnityEngine.UI;

public class UITimeLine : MonoBehaviour
{
    [Header("Music Info")]
    [SerializeField] private RectTransform lineRect;
    [SerializeField] private Image timeLineBarFill;

    [SerializeField] private GameObject[] starPoints;

    private int indexStar = 1;

    void Start()
    {
        this.StarNewPosition();
    }

    public void UpdateTimeLineBarFill(float currentTimeLine, float maxTimeLine)
    {
        this.timeLineBarFill.fillAmount = currentTimeLine / maxTimeLine;

        if (currentTimeLine == 0)
        {
            this.ReturnStarColor();
            this.indexStar = 1;
        }

        if (this.indexStar > 3) return;
        if (currentTimeLine >= ((float)this.indexStar * maxTimeLine / (float)this.starPoints.Length))
        {
            this.ChangeStarColor(this.indexStar - 1);
        }
    }

    private void StarNewPosition()
    {
        float lineWidth = lineRect.rect.width;

        float startX = -lineWidth * 0.5f;

        for (int i = 1; i <= this.starPoints.Length; i++)
        {
            float distanceX = (float)i / (float)this.starPoints.Length;
            float pos = startX + lineWidth * distanceX;
            RectTransform rectTransform = starPoints[i - 1].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(pos, rectTransform.anchoredPosition.y);
        }
    }

    private void ChangeStarColor(int index)
    {
        Image image = this.starPoints[index].GetComponent<Image>();
        image.color = Color.yellow;

        this.indexStar++;
    }

    private void ReturnStarColor()
    {
        for (int i = 0; i < this.starPoints.Length; i++)
        {
            Image image = this.starPoints[i].GetComponent<Image>();
            image.color = Color.white;
        }
    }
}
