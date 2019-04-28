using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Obstacle
{
    public int warningBeats;

    public float timeToMove;

    public Transform spawnpoint;

    public GameObject prefab;
    public GameObject warningPrefab;

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
            Instantiate(currentObstacle.warningPrefab, currentObstacle.warningPrefab.transform.position, Quaternion.identity);
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
            currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
            _nextWarning = AudioSettings.dspTime + Conductor.Instance.Crotchet;
            _nextSpawn = _nextWarning + Conductor.Instance.Crotchet * (currentObstacle.warningBeats - 1) - currentObstacle.timeToMove * Conductor.Instance.Crotchet;
            audioSource.clip = currentObstacle.warningSounds[(GameManager.Instance.currentLevel / 2)];
            audioSource.PlayScheduled(_nextWarning);
        }
    }
}