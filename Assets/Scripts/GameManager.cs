using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StopState { Stopping, Idle, Going, Waiting }

public class GameManager : MonoBehaviour, IBeat
{
    public bool isInLevel;

    private bool moveStop = false;

    public int currentLevel = 0;

    public float scrollSpeed;
    public float currentScrollSpeed = 0;
    public float stoppingTime;
    public float waitingTime;
    public float goingTime;

    public Transform stopTarget;
    public Transform goingTarget;

    public GameObject stop;

    public HorseController horse;

    private Vector2 stopStartPosition;

    private StopState stopState = StopState.Waiting;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        stopStartPosition = stop.transform.position;
        currentScrollSpeed = scrollSpeed;
    }

    void Start()
    {
        Conductor.Instance?.StartSong(currentLevel);
        isInLevel = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeat(int beatNumber, int totalBeatNumber, int songEnd)
    {
        switch (stopState)
        {
            case StopState.Stopping:
                break;
            case StopState.Idle:
                break;
            case StopState.Going:
                if (totalBeatNumber == songEnd)
                {
                    currentLevel++;
                    if (currentLevel > Conductor.Instance.songs.Count - 1)
                    {
                        SceneManager.LoadScene("WinScene");
                    }
                    Conductor.Instance.StartSong(currentLevel);
                    stopState = StopState.Waiting;
                    stop.transform.position = stopStartPosition;
                    horse.currentLives = horse.startLives;
                    isInLevel = true;
                }
                break;
            case StopState.Waiting:
                if (totalBeatNumber == songEnd)
                {
                    StartCoroutine(ChangeLevelSequence());
                }
                break;
            default:
                break;
        }
    }

    public IEnumerator ChangeLevelSequence()
    {
        currentLevel++;
        Conductor.Instance.StartSong(currentLevel);
        isInLevel = false;
        stopState = StopState.Stopping;

        float timeForCheckpoint = Conductor.Instance.Crotchet * (Conductor.Instance.songs[currentLevel].beatsAmount - 2);
        float startTime = Time.time;
        float endTime = timeForCheckpoint * stoppingTime;
        float xStartPosition = stop.transform.position.x;
        float xEndPosition = stopTarget.position.x;

        while (stop.transform.position.x > xEndPosition)
        {
            stop.transform.position = new Vector2(Mathf.Lerp(xStartPosition, xEndPosition, (Time.time - startTime) / endTime), stop.transform.position.y);
            yield return 0;
        }

        stopState = StopState.Idle;
        currentScrollSpeed = 0;
        startTime = Time.time;
        endTime = Time.time + timeForCheckpoint * waitingTime;
        
        while (Time.time < endTime)
        {
            yield return 0;
        }

        currentScrollSpeed = 1;
        stopState = StopState.Going;
        startTime = Time.time;
        endTime = timeForCheckpoint * goingTime;
        xStartPosition = stop.transform.position.x;
        xEndPosition = goingTarget.position.x;

        while (stop.transform.position.x > xEndPosition)
        {
            stop.transform.position = new Vector2(Mathf.Lerp(xStartPosition, xEndPosition, (Time.time - startTime) / endTime), stop.transform.position.y);
            yield return 0;
        }
    }

    //public IEnumerator ChangeLevelSequence()
    //{
    //    float timeForStop = Conductor.Instance.Crotchet * (Conductor.Instance.songs[currentLevel].beatsAmount - 1);
    //    Debug.Log(timeForStop);
    //    isInLevel = false;
    //    moveStop = true;
    //    currentScrollSpeed = stopMoveSpeed;
    //    float startTime = Time.time;
    //    float endTime = Time.time + stopSlowTime;

    //    while (currentScrollSpeed > 0)
    //    {
    //        currentScrollSpeed = Mathf.Lerp(scrollSpeed, 0, (Time.time - startTime) / stopSlowTime);
    //        yield return 0;
    //    }

    //    yield return new WaitForSeconds(stopTime);

    //    startTime = Time.time;
    //    endTime = Time.time + stopSlowTime;

    //    while (currentScrollSpeed < scrollSpeed)
    //    {
    //        currentScrollSpeed = Mathf.Lerp(0, scrollSpeed, (Time.time - startTime) / stopSlowTime);
    //        yield return 0;
    //    }

    //    //yield return new WaitForSeconds(stopStartTime);
    //    moveStop = false;
    //    isInLevel = true;
    //    horse.currentLives = horse.startLives;
    //}
}
