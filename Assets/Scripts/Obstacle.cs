using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameManager gameManager;

    private Vector3 pointToMove;

    private void Start()
    {
        pointToMove = pointB.position;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, pointToMove) > 0.25)
            transform.position = Vector3.MoveTowards(transform.position, pointToMove, Time.deltaTime * moveSpeed);
        else
        {
            if (pointToMove.Equals(pointA.position))
                pointToMove = pointB.position;
            else
                pointToMove = pointA.position;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Train"))
        {
            gameManager.GameOver(collision.gameObject);
        }
    }

}
