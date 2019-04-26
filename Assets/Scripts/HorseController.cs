using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float duckShrinkMultiplier;
    [SerializeField] private float timeToUnduck;

    private Rigidbody2D rb;

    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rb.position + collider.offset, Vector2.down, collider.bounds.extents.y + 0.1f);

        if (hitInfo.collider?.CompareTag("Ground") == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //float distanceRemoved = collider.size.y * duckShrinkMultiplier;
            //collider.size = new Vector2(collider.size.x, distanceRemoved);
            //collider.offset = new Vector2(collider.offset.x, collider.offset.y - distanceRemoved/2);
            float scaleChanged = transform.localScale.y * duckShrinkMultiplier;
            transform.localScale = new Vector3(transform.localScale.x, scaleChanged, transform.localScale.z);
            rb.MovePosition(rb.position + Vector2.down * scaleChanged);
            StartCoroutine(Unduck(scaleChanged, timeToUnduck));
        }
    }

    private IEnumerator Unduck(float distanceRemoved, float time)
    {
        yield return new WaitForSeconds(time);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + distanceRemoved, transform.localScale.z);
        //collider.size = new Vector2(collider.size.x, collider.size.y + distanceRemoved);
        //collider.offset = new Vector2(collider.offset.x, collider.offset.y + distanceRemoved / 2);
    }
}
