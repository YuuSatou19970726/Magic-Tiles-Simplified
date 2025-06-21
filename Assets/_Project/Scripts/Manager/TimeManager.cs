using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;
    public static TimeManager Instance => instance;

    [SerializeField] private float resumeRate = 3;
    [SerializeField] private float pauseRate = 7;

    private float timeAdjustRate;
    private float targetTimeScale = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Mathf.Abs(Time.timeScale - this.targetTimeScale) > 0.05f)
        {
            float adjustRate = Time.unscaledDeltaTime * this.timeAdjustRate;
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, adjustRate);
        }
        else
        {
            Time.timeScale = this.targetTimeScale;
        }
    }

    public void PauseTime(float changeRate = 7)
    {
        this.timeAdjustRate = this.pauseRate;
        this.targetTimeScale = 0;
    }

    public void Resume(float changeRate = 3)
    {
        this.timeAdjustRate = this.resumeRate;
        this.targetTimeScale = 1;
    }
}
