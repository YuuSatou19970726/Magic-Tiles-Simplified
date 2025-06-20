using UnityEngine;

public class UIInGame : CustomMonoBehaviour
{
    private UIScore uIScore;
    private TextFPS textFPS;

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
}
