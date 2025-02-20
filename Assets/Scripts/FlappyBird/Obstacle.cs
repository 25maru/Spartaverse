using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float highPosY = 1f;
    [SerializeField] private float lowPosY = -1f;
    [SerializeField] private float holeSizeMin = 1f;
    [SerializeField] private float holeSizeMax = 3f;
    [SerializeField] private Transform topObstacle;
    [SerializeField] private Transform bottomObstacle;
    [SerializeField] private float widthPadding = 4f;

    private FlappyGameManager gameManager;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize * 0.5f;

        topObstacle.localPosition = new Vector3(0, halfHoleSize);
        bottomObstacle.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void Start()
    {
        gameManager = FlappyGameManager.Instance;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlappyPlayer player = collision.GetComponent<FlappyPlayer>();

        if (player != null) gameManager.AddScore(1);
    }
}
