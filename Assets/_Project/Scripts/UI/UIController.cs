using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : CustomMonoBehaviour
{
    private static UIController instance;
    public static UIController Instance => instance;

    [SerializeField] private GameObject[] UIElements;

    [Header("Fade Image")]
    [SerializeField] private Image fadeImage;

    public UIInGame inGameUI { get; private set; }
    private UIGameOver gameOverUI;
    private UIGameComplete gameCompleteUI;

    public void QuitTheGame() => Application.Quit();
    public void StartTheGame() => StartCoroutine(this.StartGameSequence());
    public void ReturnMainMenu() => StartCoroutine(this.ChangeImageAlpha(1f, 1f, GameManager.Instance.ReturnMainMenu));

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        base.Awake();
    }

    protected override void Start()
    {
        StartCoroutine(this.ChangeImageAlpha(0, 1.5f, null));
    }

    protected override void LoadComponents()
    {
        this.MappingComponent();
    }

    private void MappingComponent()
    {
        if (this.inGameUI != null && this.gameOverUI != null && this.gameCompleteUI != null) return;
        this.inGameUI = GetComponentInChildren<UIInGame>(true);
        this.gameOverUI = GetComponentInChildren<UIGameOver>(true);
        this.gameCompleteUI = GetComponentInChildren<UIGameComplete>(true);
    }

    public void SwitchTo(GameObject uiToSwitchOn)
    {
        foreach (GameObject go in this.UIElements)
            go.SetActive(false);

        uiToSwitchOn.SetActive(true);
    }

    public void ShowGameOverUI(int score)
    {
        this.SwitchTo(this.gameOverUI.gameObject);
        this.gameOverUI.ShowGameOverScore(score);
    }

    public void ShowGameCompleteUI(int score)
    {
        this.SwitchTo(this.gameCompleteUI.gameObject);
        this.gameCompleteUI.ShowGameCompleteScore(score);
    }

    public void RestartTheGame()
    {
        this.SwitchTo(this.inGameUI.gameObject);
        GameManager.Instance.RestartScene();
    }

    private IEnumerator ChangeImageAlpha(float targetAlpha, float duration, System.Action onComplete)
    {
        float time = 0;
        Color currentColor = this.fadeImage.color;
        float startAlpha = currentColor.a;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            this.fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        this.fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        //Call the impletion method if it exits
        onComplete?.Invoke();
    }

    private IEnumerator StartGameSequence()
    {
        yield return null;
        this.SwitchTo(this.inGameUI.gameObject);
        GameManager.Instance.GameStart();

        StartCoroutine(this.ChangeImageAlpha(0, 1f, null));
    }

    [ContextMenu("Assign Audio To Button")]
    public void AssignAudioListenersToButton()
    {
        UIButton[] buttons = FindObjectsByType<UIButton>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var button in buttons)
        {
            button.AssignAudioSource();
        }
    }
}
