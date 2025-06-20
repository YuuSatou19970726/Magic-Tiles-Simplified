using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private BeatController beatController;

    private int score = 0;

    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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

    public void GameCompleted()
    {

    }

    public void UpdateScore(int score, string typeScore)
    {
        this.score += score;

        UIController.Instance.UpdateScore(this.score, typeScore);
    }
}
