using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    Vector3 MoveSpeed = new Vector3(2f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.position += MoveSpeed * Time.deltaTime);
    }
}
