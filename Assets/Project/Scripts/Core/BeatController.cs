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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        this.beatInterval = 60f / bpm;
        this.nextBeatTime = Time.time + beatInterval;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!this.isPlay) return;

        if (Time.time >= this.nextBeatTime)
        {
            OnBeat?.Invoke();
            this.nextBeatTime += beatInterval;
        }
    }

    public void SetupBeatMusic(bool active)
    {
        this.isPlay = active;

        this.nextBeatTime = Time.time + beatInterval;

        if (active)
            musicSource.Play();
        else
            musicSource.Stop();
    }
}
