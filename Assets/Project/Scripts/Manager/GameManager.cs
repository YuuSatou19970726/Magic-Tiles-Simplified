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
        this.beatController.isPlay = true;
    }

    public void GameOver()
    {
        this.beatController.isPlay = false;
        UIController.Instance.ShowGameOverUI(this.score);
    }

    public void GameCompleted()
    {

    }
}
