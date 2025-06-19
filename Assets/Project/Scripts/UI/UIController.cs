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

    public void RestartTheGame() => StartCoroutine(this.ChangeImageAlpha(1f, 1f, GameManager.Instance.RestartScene));

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
