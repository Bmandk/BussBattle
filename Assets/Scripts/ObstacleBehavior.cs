using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObstacleBehavior : MonoBehaviour
{
    public float MoveSpeed = 2f;

    Vector2 MoveLeft = new Vector2(-10f, 0f);
    Rigidbody2D RB = GetComponent<Rigidbody2D>;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RB.MovePosition(RB.position += MoveLeft * MoveSpeed * Time.fixedDeltaTime);
    }
}
