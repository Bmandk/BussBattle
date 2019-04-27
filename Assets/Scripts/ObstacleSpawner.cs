using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public Vector2 CurrentPosition = new Vector3(0f, 0f);
    public float SpawnTimer = 2f;

    float SpawnRate = -1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnRate < 0)
        {
            GameObject ObjectObstacle = GameObject.Instantiate(Obstacle, CurrentPosition, Quaternion.identity) as GameObject;
            //CurrentPosition += new Vector3(1f, 0f, 0f);
            SpawnRate = SpawnTimer;
        }

        SpawnRate = SpawnRate - Time.deltaTime;
    }
}
