using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float timeToMove, Transform endPosition)
    {
        StartCoroutine(Movement(timeToMove, endPosition));
    }

    private IEnumerator Movement(float timeToMove, Transform endPosition)
    {
        float startXPosition = transform.position.x;
        float endXPosition = endPosition.position.x;
        float songStartPosition = Conductor.Instance.SongPosition;
        float songEndPosition = songStartPosition + Conductor.Instance.Crotchet * timeToMove;
        float xPosition = 0;
        while (true)
        {
            // y = xPosition
            // x = currentSongPosition
            // y0 = startXPosition
            // y1 = endXPosition
            // x0 = songStartPostion
            // x1 = songEndPosition
            float currentSongPosition = Conductor.Instance.SongPosition;
            xPosition = (startXPosition * (songEndPosition - currentSongPosition) + endXPosition * (currentSongPosition - songStartPosition)) / (songEndPosition - songStartPosition);
            _rb.MovePosition(new Vector2(xPosition, _rb.position.y));
            yield return 0;
        }
    }
}
