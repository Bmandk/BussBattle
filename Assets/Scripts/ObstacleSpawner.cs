using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public float SpawnRate = -1f;
    public Vector3 CurrentPosition = new Vector3(0f, 0f, 0f);

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
            SpawnRate = 2f;
        }

        SpawnRate = SpawnRate - Time.deltaTime;
    }
}
