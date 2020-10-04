using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private LeverHandler handler;
    [SerializeField] private GameObject handleAnchor;

    private bool turnedOn = false;
    private bool trainNearLever = false;

    private void Update()
    {
        if(trainNearLever)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {

                handler.LeverFlipped();

                AudioManager.AM.Play("Boop");

                if (turnedOn)
                {
                    handleAnchor.transform.rotation = Quaternion.Euler(-40f, 0f, 0f);
                    turnedOn = false;
                }
                else
                {
                    handleAnchor.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
                    turnedOn = true;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Train"))
        {
            trainNearLever = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            trainNearLever = false;
        }
    }
}
