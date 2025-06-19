using UnityEngine;

public class BeatController : MonoBehaviour
{
    public AudioSource musicSource;
    public float bpm = 60f;

    private float beatInterval;
    private float nextBeatTime;

    public delegate void BeatAction();
    public static event BeatAction OnBeat;

    //remove when close project
    public bool isPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.beatInterval = 60f / bpm;
        this.nextBeatTime = Time.time + beatInterval;

        if (this.isPlay)
            this.musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isPlay) return;

        if (Time.time >= this.nextBeatTime)
        {
            OnBeat?.Invoke();
            this.nextBeatTime += beatInterval;
        }
    }
}
