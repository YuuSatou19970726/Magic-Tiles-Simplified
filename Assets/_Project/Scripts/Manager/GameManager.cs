using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private BeatController beatController;

    private int score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        this.beatController = FindObjectsByType<BeatController>(FindObjectsSortMode.None).FirstOrDefault();
    }

    private void Start()
    {
        this.beatController.SetupBeatMusic(true);
    }

    public void GameOver()
    {
        this.beatController.SetupBeatMusic(false);
        UIController.Instance.ShowGameOverUI(this.score);
    }

    public void ShowGameCompleted()
    {
        UIController.Instance.ShowGameCompleteUI();
    }

    public void UpdateScore(int score, RatingType ratingType)
    {
        this.score += score;

        UIController.Instance.inGameUI.UpdateUIScore(this.score, ratingType);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
