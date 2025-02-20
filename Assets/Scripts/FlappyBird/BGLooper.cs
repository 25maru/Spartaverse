using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour
{
    [SerializeField] private int backgroundCount = 5;
    [SerializeField] private int obstacleCount = 0;
    [SerializeField] private Vector3 obstacleLastPosition = Vector3.zero;

    private void Start()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("트리거 됨: " + collision.name);

        if (collision.CompareTag("Background"))
        {
            float width = ((BoxCollider2D)collision).size.x * collision.transform.localScale.x;
            Vector3 pos = collision.transform.position;

            pos.x += width * backgroundCount;
            collision.transform.position = pos;

            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
