using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class BeatEvent : UnityEvent<int, int>
{

}

public class Conductor : MonoBehaviour
{
    //public BeatEvent onBeat;

    private int _beatNumber;

    [SerializeField] private float _bpm;
    [SerializeField] private float _offset;
    private float _dsptimesong;
    private float _crotchet;
    private float _lastBeat;

    [SerializeField] private AudioClip song;

    private AudioSource audioSource;

    public float songPosition { get; private set; }

    public static Conductor Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();
        _crotchet = 60f / _bpm;

        //foreach (FrameAnimator fa in GameObject.FindObjectsOfType<FrameAnimator>())
        //{
        //    onBeat.AddListener(fa.OnBeat);
        //}
    }

    void StartSong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
        _dsptimesong = (float)AudioSettings.dspTime;
        _lastBeat = -_crotchet;
        _beatNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSong(song);
        }

        if (audioSource.isPlaying)
        {
            songPosition = ((float)AudioSettings.dspTime - _dsptimesong) * audioSource.pitch - _offset;

            if (songPosition > _lastBeat + _crotchet)
            {
                _lastBeat += _crotchet;
                OnBeat();
            }
        }
    }

    private void OnBeat()
    {
        _beatNumber++;
        foreach (IBeat b in FindObjectsOfType<MonoBehaviour>().OfType<IBeat>())
        {
            b.OnBeat((_beatNumber - 1) % 4 + 1, _beatNumber);
        }
        //onBeat.Invoke((_beatNumber - 1) % 4 + 1, _beatNumber);
    }
}