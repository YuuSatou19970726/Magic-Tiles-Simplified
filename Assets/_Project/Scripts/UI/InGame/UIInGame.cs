using UnityEngine;
using UnityEngine.UI;

public class UIInGame : CustomMonoBehaviour
{
    private UIScore scoreUI;
    private TextFPS textFPS;
    private UITimeLine timeLineUI;
    private UICombo comboUI;

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
        if (this.scoreUI != null && this.textFPS != null && this.timeLineUI != null && this.comboUI != null) return;
        this.scoreUI = GetComponentInChildren<UIScore>(true);
        this.textFPS = GetComponent<TextFPS>();
        this.timeLineUI = GetComponentInChildren<UITimeLine>(true);
        this.comboUI = GetComponentInChildren<UICombo>(true);
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

    public void ShowComboUI(int count)
    {
        if (!this.comboUI.gameObject.activeSelf)
            this.comboUI.gameObject.SetActive(true);

        this.comboUI.UpdateComboCount(count);
    }

    public void HideComboUI()
    {
        this.comboUI.gameObject.SetActive(false);
        this.comboUI.UpdateComboCount(0);
    }
}