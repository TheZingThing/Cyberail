using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] private Color orange;
    [SerializeField] private Color red;
    [SerializeField] private Color green;

    [SerializeField] private SpriteRenderer passengerIcon;
    [SerializeField] private PassengerType storedPassenger;
    [SerializeField] private LeverHandler handler;

    private bool trainNearButton = false;
    private TrainController train;

    private void Start()
    {
        UpdatePassengerIcon();
    }

    private void Update()
    {
        if (trainNearButton)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (storedPassenger != PassengerType.None)
                {

                    storedPassenger = train.AddPassenger(storedPassenger);
                    UpdatePassengerIcon();

                    if (storedPassenger == PassengerType.None)
                        handler.LeverFlipped();

                }
                else
                {
                    storedPassenger = train.RemovePassenger();
                    UpdatePassengerIcon();

                    if (storedPassenger != PassengerType.None)
                        handler.LeverFlipped();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Train"))
        {
            trainNearButton = true;
            train = other.GetComponent<TrainController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            trainNearButton = false;
            train = null;
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
}
