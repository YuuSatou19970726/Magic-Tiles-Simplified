using UnityEngine;

public class BeatController : MonoBehaviour
{
    public AudioSource musicSource;
    [Range(30f, 180f)]
    public float bpm = 60f;

    private float beatInterval;
    private float nextBeatTime;

    public delegate void BeatAction();
    public static event BeatAction OnBeat;

    private bool isPlay = false;

    private float maxTimeLine;

    // Update is called once per frame
    private void Update()
    {
        if (!this.isPlay) return;

        float currentTimeLine = this.musicSource.time;
        UIController.Instance.inGameUI.UpdateTimeLineBarFill(currentTimeLine, maxTimeLine);

        bool checkTimeLine = currentTimeLine + 10f > maxTimeLine;

        if (Time.time >= this.nextBeatTime && !checkTimeLine)
        {
            OnBeat?.Invoke();
            this.nextBeatTime += this.beatInterval;
        }

        if (currentTimeLine >= this.maxTimeLine)
            GameManager.Instance.ShowGameCompleted();
    }

    public void SetupBeatMusic(bool active)
    {
        this.isPlay = active;

        this.beatInterval = 60f / bpm;
        this.nextBeatTime = Time.time + this.beatInterval;

        if (active)
            musicSource.Play();
        else
            musicSource.Stop();

        this.maxTimeLine = this.musicSource.clip.length;
        UIController.Instance.inGameUI.UpdateTimeLineBarFill(0, maxTimeLine);
    }
}
