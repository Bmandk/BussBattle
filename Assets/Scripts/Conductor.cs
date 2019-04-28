using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class Song
{
    public AudioClip clip;
    public float bpm;
    public float offset;
    public int beatsAmount;
}

public class Conductor : MonoBehaviour
{
    public List<Song> songs = new List<Song>();

    private Song currentSong;
    [SerializeField] private int _beatNumber;
    
    private float _dsptimesong;
    private float _lastBeat;

    private AudioSource audioSource;

    public float SongPosition { get; private set; }
    public float Crotchet { get; private set; }
    public float BPM { get => currentSong.bpm; }

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
    }

    public void StartSong(int level)
    {
        audioSource.Stop();
        currentSong = songs[level];
        _dsptimesong = (float)AudioSettings.dspTime;
        Crotchet = 60f / (BPM * audioSource.pitch);
        _lastBeat = -currentSong.offset;
        _beatNumber = 1;
        audioSource.clip = currentSong.clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            SongPosition = ((float)AudioSettings.dspTime - _dsptimesong) * audioSource.pitch - currentSong.offset;

            if (SongPosition > _lastBeat + Crotchet)
            {
                _lastBeat += Crotchet;
                OnBeat();
            }
        }
    }

    private void OnBeat()
    {
        _beatNumber++;
        foreach (IBeat b in FindObjectsOfType<MonoBehaviour>().OfType<IBeat>())
        {
            b.OnBeat((_beatNumber - 1) % 4 + 1, _beatNumber, currentSong.beatsAmount);
        }
        //onBeat.Invoke((_beatNumber - 1) % 4 + 1, _beatNumber);
    }
}