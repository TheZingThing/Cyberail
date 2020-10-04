using UnityEngine;

public class StationController : MonoBehaviour
{

    [SerializeField] private PassengerType passengerRequired;
    [SerializeField] private PassengerType storedPassenger;
    [SerializeField] private SpriteRenderer passengerIcon;

    [SerializeField] private Color orange;
    [SerializeField] private Color red;
    [SerializeField] private Color green;

    private bool trainInStation = false;
    private TrainController train;

    public bool HasRequiredPassenger { get; set; } = false;

    private void Start()
    {
        UpdatePassengerIcon();

        if (passengerRequired == PassengerType.None)
            HasRequiredPassenger = true;
    }

    private void Update()
    {
        if(trainInStation)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (storedPassenger != PassengerType.None)
                {

                    storedPassenger = train.AddPassenger(storedPassenger);
                    UpdatePassengerIcon();

                }
                else
                {
                    storedPassenger = train.RemovePassenger();
                    UpdatePassengerIcon();
                }

                if (passengerRequired != PassengerType.None)
                {
                    if (storedPassenger == passengerRequired)
                        HasRequiredPassenger = true;
                    else
                        HasRequiredPassenger = false;
                }
            }
        }
    }

    private void UpdatePassengerIcon()
    {
        switch (storedPassenger)
        {
            case PassengerType.Orange:
            passengerIcon.color = orange;
            break;

            case PassengerType.Red:
            passengerIcon.color = red;
            break;

            case PassengerType.Green:
            passengerIcon.color = green;
            break;

            case PassengerType.None:
            passengerIcon.color = Color.clear;
            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Train"))
        {
            trainInStation = true;
            train = other.GetComponent<TrainController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            trainInStation = false;
            train = null;
        }
    }

}
