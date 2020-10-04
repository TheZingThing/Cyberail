using System.Collections;
using UnityEngine;

public class TrainController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform moveDirection;
    [SerializeField] private PassengerListUI storedPassengersUI;
    [SerializeField] private GameObject trainVisual;

    private float moveInput;
    private bool movingForward;

    private bool lastMoveDirection;

    public bool CanTurn { get; set; } = true;

    private PassengerType storedPassenger = PassengerType.None;

    private void Start()
    {
        storedPassengersUI.gameObject.SetActive(false);
    }

    private void Update()
    {

        moveInput = Input.GetAxis("Vertical");

        if (moveInput > 0)
            movingForward = true;
        else
            movingForward = false;

        if (movingForward != lastMoveDirection)
            CanTurn = true;

        transform.position += moveDirection.forward * moveInput * moveSpeed * Time.deltaTime;

        trainVisual.transform.rotation = Quaternion.Slerp(trainVisual.transform.rotation, Quaternion.LookRotation(moveDirection.forward) * Quaternion.Euler(0f, -90f, 0f), Time.deltaTime * 4f);
    }

    public void TurnPlayer(TurnDirection direction, Vector3 newPos)
    {
        var rotateDir = Vector3.zero;

        if (CanTurn)
        {
            switch (direction)
            {
                case TurnDirection.Left:
                {

                    if (movingForward)
                    {
                        rotateDir = new Vector3(0f, -90f, 0f);
                        moveDirection.Rotate(rotateDir);
                    }
                    else
                    {
                        rotateDir = new Vector3(0f, 90f, 0f);
                        moveDirection.Rotate(rotateDir);
                    }

                }
                break;

                case TurnDirection.Right:
                {

                    if (movingForward)
                    {
                        rotateDir = new Vector3(0f, 90f, 0f);
                        moveDirection.Rotate(rotateDir);
                    }
                    else
                    {
                        rotateDir = new Vector3(0f, -90f, 0f);
                        moveDirection.Rotate(rotateDir);
                    }

                }
                break;
                    
            }

            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);

            if (direction == TurnDirection.Stop)
                lastMoveDirection = !movingForward;
            else
                lastMoveDirection = movingForward;

            CanTurn = false;
        }

    }

    public PassengerType AddPassenger(PassengerType type)
    {
        if (storedPassenger == PassengerType.None)
        {
            storedPassenger = type;
            storedPassengersUI.gameObject.SetActive(true);
            storedPassengersUI.UpdatePassenger(storedPassenger);
            AudioManager.AM.Play("Boop");
            return PassengerType.None;
        }
        else
            return type;
    }

    public PassengerType RemovePassenger()
    {
        if (storedPassenger != PassengerType.None)
        {
            var temp = storedPassenger;
            storedPassenger = PassengerType.None;
            storedPassengersUI.gameObject.SetActive(false);
            storedPassengersUI.UpdatePassenger(storedPassenger);
            AudioManager.AM.Play("Boop");
            return temp;
        }
        else
            return PassengerType.None;
    }
}
