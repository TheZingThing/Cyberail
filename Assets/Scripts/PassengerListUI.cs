using UnityEngine;
using UnityEngine.UI;

public class PassengerListUI : MonoBehaviour
{

    [SerializeField] private Transform targetPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Image passengerImage;

    [SerializeField] private Color orange;
    [SerializeField] private Color red;
    [SerializeField] private Color green;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {

        transform.position = cam.WorldToScreenPoint(targetPos.position) + offset;

    }

    public void UpdatePassenger(PassengerType type)
    {
        switch(type)
        {
            case PassengerType.Orange:
                passengerImage.color = orange;
                break;

            case PassengerType.Red:
                passengerImage.color = red;
                break;

            case PassengerType.Green:
                passengerImage.color = green;
                break;
        }
    }
}
