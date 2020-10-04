using UnityEngine;

public class Corner : MonoBehaviour
{

    [SerializeField] private TurnDirection turnDirection;

    private bool trainInStation = false;
    private GameObject train;

    public TurnDirection TurnDir { get => turnDirection; set => turnDirection = value; }

    private void Update()
    {
        if (trainInStation)
        {
            Vector3 pos = new Vector3(train.transform.position.x, transform.position.y, train.transform.position.z);

            if (Vector3.Distance(transform.position, pos) < 0.25f)
            {

                train.GetComponent<TrainController>().TurnPlayer(TurnDir, transform.position);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Train"))
        {
            trainInStation = true;
            train = other.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            other.GetComponent<TrainController>().CanTurn = true;
            trainInStation = false;
            train = null;
        }
    }
}

