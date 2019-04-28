using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Obstacle
{
    public int warningBeats;

    public float timeToMove;

    public Transform spawnpoint;

    public GameObject prefab;

    public AudioClip[] warningSounds;
    public AudioClip beatSound;
}

public class ObstacleSpawner : MonoBehaviour, IBeat
{
    public List<Obstacle> obstacles = new List<Obstacle>();

    public Transform beatPoint;

    private double _nextWarning = Mathf.Infinity;
    private double _nextSpawn = Mathf.Infinity;

    private AudioSource audioSource;

    private Obstacle currentObstacle;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_nextWarning < AudioSettings.dspTime)
        {
            _nextWarning = Mathf.Infinity;
            _nextSpawn = AudioSettings.dspTime + Conductor.Instance.Crotchet * currentObstacle.warningBeats - currentObstacle.timeToMove * Conductor.Instance.Crotchet;
        }

        if (_nextSpawn < AudioSettings.dspTime)
        {
            _nextSpawn = Mathf.Infinity;
            GameObject obstacle = Instantiate(currentObstacle.prefab, currentObstacle.spawnpoint.position, currentObstacle.spawnpoint.rotation);
            ObstacleBehaviour o = obstacle.GetComponent<ObstacleBehaviour>();
            o?.Initialize(currentObstacle.timeToMove, beatPoint);
        }
    }

    public void OnBeat(int beatNumber, int totalBeatNumber, int songEnd)
    {
        if (!GameManager.Instance.isInLevel || totalBeatNumber + 4 > songEnd)
            return;

        if (beatNumber == 4)
        {
            Debug.Log("Spawning");
            currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
            _nextWarning = AudioSettings.dspTime + Conductor.Instance.Crotchet;
            audioSource.clip = currentObstacle.warningSounds[(GameManager.Instance.currentLevel / 2)];
            audioSource.PlayScheduled(_nextWarning);
        }
    }
}