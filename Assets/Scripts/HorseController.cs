using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _duckShrinkMultiplier;
    [SerializeField] private float _timeToUnduck;

    private Rigidbody2D _rb;

    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        Conductor.Instance.onBeat.AddListener(OnBeat);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(_rb.position + _collider.offset, Vector2.down, _collider.bounds.extents.y + 0.1f);

        if (hitInfo.collider?.CompareTag("Ground") == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //float distanceRemoved = collider.size.y * duckShrinkMultiplier;
            //collider.size = new Vector2(collider.size.x, distanceRemoved);
            //collider.offset = new Vector2(collider.offset.x, collider.offset.y - distanceRemoved/2);
            float scaleChanged = transform.localScale.y * _duckShrinkMultiplier;
            transform.localScale = new Vector3(transform.localScale.x, scaleChanged, transform.localScale.z);
            _rb.MovePosition(_rb.position + Vector2.down * scaleChanged);
            StartCoroutine(Unduck(scaleChanged, _timeToUnduck));
        }
    }

    private IEnumerator Unduck(float distanceRemoved, float time)
    {
        yield return new WaitForSeconds(time);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + distanceRemoved, transform.localScale.z);
        //collider.size = new Vector2(collider.size.x, collider.size.y + distanceRemoved);
        //collider.offset = new Vector2(collider.offset.x, collider.offset.y + distanceRemoved / 2);
    }

    private void OnBeat(int beatNumber, int totalBeatNumber)
    {

    }
}
