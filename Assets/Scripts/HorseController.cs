using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorseController : MonoBehaviour
{
    private bool _inputLocked;

    public int startLives;
    public int currentLives;

    //[SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDistance;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _duckDistance;
    [SerializeField] private float _duckTime;
    [SerializeField] private float _dodgeDistance;
    [SerializeField] private float _dodgeTime;
    [SerializeField] private float _inputThresholdTime;
    [SerializeField] private float _successLockTime;
    [SerializeField] private float _failLockTime;

    private Rigidbody2D _rb;

    private BoxCollider2D _collider;

    public SpriteRenderer s;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        currentLives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSongPosition = Conductor.Instance.SongPosition;
        bool isWithinBeat = ((currentSongPosition % Conductor.Instance.Crotchet) > Conductor.Instance.Crotchet - _inputThresholdTime * Conductor.Instance.Crotchet &&
            (currentSongPosition % Conductor.Instance.Crotchet) < Conductor.Instance.Crotchet + _inputThresholdTime * Conductor.Instance.Crotchet);

        if (isWithinBeat)
            s.color = Color.red;
        else
            s.color = Color.white;

        if (!_inputLocked)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(_rb.position + _collider.offset, Vector2.down, _collider.bounds.extents.y + 0.1f);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isWithinBeat)
                {
                    LockInput(_successLockTime);
                    StartCoroutine(Jump());
                }
                else
                {
                    LockInput(_failLockTime);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (isWithinBeat)
                {
                    LockInput(_successLockTime);
                    StartCoroutine(Duck());
                }
                else
                {
                    LockInput(_failLockTime);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (isWithinBeat)
                {
                    LockInput(_successLockTime);
                    StartCoroutine(Dodge());
                }
                else
                {
                    LockInput(_failLockTime);
                }
            }
        }
    }

    private IEnumerator Duck()
    {
        _collider.size = new Vector2(_collider.size.x, _collider.size.y - _duckDistance * 2);
        _collider.offset = new Vector2(_collider.offset.x, _collider.offset.y + _duckDistance);
        _rb.MovePosition(_rb.position + Vector2.down * _duckDistance);

        yield return new WaitForSeconds(_duckTime * Conductor.Instance.Crotchet);

        _collider.offset = new Vector2(_collider.offset.x, _collider.offset.y - _duckDistance);
        _collider.size = new Vector2(_collider.size.x, _collider.size.y + _duckDistance * 2);
        _rb.MovePosition(_rb.position + Vector2.up * _duckDistance);
    }

    private IEnumerator Jump()
    {
        _rb.MovePosition(_rb.position + Vector2.up * _jumpDistance);
        yield return new WaitForSeconds(_jumpTime * Conductor.Instance.Crotchet);
        _rb.MovePosition(_rb.position + Vector2.down * _jumpDistance);
    }

    private IEnumerator Dodge()
    {
        _rb.MovePosition(_rb.position + Vector2.right * _dodgeDistance);
        yield return new WaitForSeconds(_dodgeTime * Conductor.Instance.Crotchet);
        _rb.MovePosition(_rb.position + Vector2.left * _dodgeDistance);
    }

    private void LockInput(float time)
    {
        _inputLocked = true;
        StartCoroutine(WaitForLockInput(time));
    }

    private IEnumerator WaitForLockInput(float time)
    {
        yield return new WaitForSeconds(time * Conductor.Instance.Crotchet);
        _inputLocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            currentLives--;
            if (currentLives <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
            else
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
