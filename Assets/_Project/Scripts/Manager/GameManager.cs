using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private BeatController beatController;

    private int score = 0;
    private int countCombo = 0;

    private bool isStart = false;

    public void ReturnMainMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        this.beatController = FindObjectsByType<BeatController>(FindObjectsSortMode.None).FirstOrDefault();
    }

    public void GameStart()
    {
        this.isStart = true;
        this.beatController.SetupBeatMusic(true);
    }

    public void GameOver()
    {
        if (!this.isStart) return;
        this.isStart = false;
        this.beatController.SetupBeatMusic(false);
        UIController.Instance.ShowGameOverUI(this.score);
    }

    public void ShowGameCompleted()
    {
        this.isStart = false;
        this.beatController.SetupBeatMusic(false);
        UIController.Instance.ShowGameCompleteUI(this.score);
    }

    public void UpdateScore(int score, RatingType ratingType)
    {
        this.score += score;

        if (ratingType == RatingType.MISS || ratingType == RatingType.GOOD)
        {
            this.countCombo = 0;
            UIController.Instance.inGameUI.HideComboUI();
        }

        this.countCombo++;

        if (this.countCombo >= 3)
            UIController.Instance.inGameUI.ShowComboUI(this.countCombo);

        int pointPlus = 1;

        if (pointPlus >= 50 && pointPlus < 100)
            pointPlus = 2;
        else if (pointPlus >= 100 && pointPlus < 200)
            pointPlus = 3;
        else if (pointPlus >= 200)
            pointPlus = 4;

        UIController.Instance.inGameUI.UpdateUIScore(this.score * pointPlus, ratingType);
    }

    public void RestartScene()
    {
        this.score = 0;
        this.countCombo = 0;

        var tiles = FindObjectsByType<TileController>(FindObjectsSortMode.None);
        foreach (var tile in tiles)
        {
            if (tile.isActive)
                tile.ReturnTile();
        }
        this.isStart = true;
        this.GameStart();
    }
}
