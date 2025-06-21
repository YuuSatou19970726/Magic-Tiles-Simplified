using UnityEngine;
using UnityEngine.UI;

public class UIInGame : CustomMonoBehaviour
{
    private UIScore uIScore;
    private TextFPS textFPS;

    [Header("Music Info")]
    [SerializeField] private Image timeLineBarFill;

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
        if (this.uIScore != null && this.textFPS != null) return;
        this.uIScore = GetComponentInChildren<UIScore>(true);
        this.textFPS = GetComponent<TextFPS>();
    }

    public void UpdateUIScore(int score, string scoreType)
    {
        this.uIScore.UpdateScore(score, scoreType);
    }

    public void ShowFPS(bool enable)
    {
        this.textFPS.ShowFPS(enable);
    }

    public void UpdateTimeLineBarFill(float currentTimeLine, float maxTimeLine)
    {
        this.timeLineBarFill.fillAmount = currentTimeLine / maxTimeLine;
    }
}