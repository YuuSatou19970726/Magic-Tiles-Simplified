using UnityEngine;
using UnityEngine.UI;

public class UIInGame : CustomMonoBehaviour
{
    private UIScore scoreUI;
    private TextFPS textFPS;
    private UITimeLine timeLineUI;

    protected override void Start()
    {
        this.ShowFPS(false);
    }

    protected override void LoadComponents()
    {
        this.MappingComponent();
    }

    private void MappingComponent()
    {
        if (this.scoreUI != null && this.textFPS != null && this.timeLineUI != null) return;
        this.scoreUI = GetComponentInChildren<UIScore>(true);
        this.textFPS = GetComponent<TextFPS>();
        this.timeLineUI = GetComponentInChildren<UITimeLine>(true);
    }

    public void UpdateUIScore(int score, RatingType ratingType)
    {
        this.scoreUI.UpdateScore(score, ratingType);
    }

    public void ShowFPS(bool enable)
    {
        this.textFPS.ShowFPS(enable);
    }

    public void UpdateTimeLineBarFill(float currentTimeLine, float maxTimeLine)
    {
        this.timeLineUI.UpdateTimeLineBarFill(currentTimeLine, maxTimeLine);
    }
}