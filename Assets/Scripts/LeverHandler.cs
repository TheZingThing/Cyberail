using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CornerChange
{
    [SerializeField] private Corner corner;
    [SerializeField] private TurnDirection onDirection;
    [SerializeField] private TurnDirection offDirection;

    public Corner Corner { get => corner; set => corner = value; }
    public TurnDirection OnDirection { get => onDirection; set => onDirection = value; }
    public TurnDirection OffDirection { get => offDirection; set => offDirection = value; }
}

public class LeverHandler : MonoBehaviour
{
    [SerializeField] private bool turnedOn = false;
    [SerializeField] List<GameObject> raisedTracks = new List<GameObject>();
    [SerializeField] List<GameObject> loweredTracks = new List<GameObject>();
    [SerializeField] List<CornerChange> cornerChanges = new List<CornerChange>();

    public void Start()
    {
        foreach (GameObject go in loweredTracks)
        {
            go.transform.position = new Vector3(go.transform.position.x, -5f, go.transform.position.z);
        }

        foreach (CornerChange change in cornerChanges)
        {
            change.Corner.TurnDir = change.OffDirection;
        }
    }

    public void LeverFlipped()
    {
        if (!turnedOn)
            LeverTurnedOn();
        else
            LeverTurnedOff();
    }

    private void LeverTurnedOff()
    {

        StopAllCoroutines();
        foreach(GameObject go in loweredTracks)
        {
            StartCoroutine(LerpMovement(go.transform, new Vector3(go.transform.position.x, -5f, go.transform.position.z)));
        }

        foreach (GameObject go in raisedTracks)
        {
            StartCoroutine(LerpMovement(go.transform, new Vector3(go.transform.position.x, 0f, go.transform.position.z)));
        }

        foreach(CornerChange change in cornerChanges)
        {
            change.Corner.TurnDir = change.OffDirection;
        }

        turnedOn = false;
    }

    private void LeverTurnedOn()
    {

        StopAllCoroutines();
        foreach (GameObject go in loweredTracks)
        {
            StartCoroutine(LerpMovement(go.transform, new Vector3(go.transform.position.x, 0f, go.transform.position.z)));
        }

        foreach (GameObject go in raisedTracks)
        {
            StartCoroutine(LerpMovement(go.transform, new Vector3(go.transform.position.x, -5f, go.transform.position.z)));
        }

        foreach (CornerChange change in cornerChanges)
        {
            change.Corner.TurnDir = change.OnDirection;
        }

        turnedOn = true;
    }

    private IEnumerator LerpMovement(Transform target, Vector3 newPos)
    {
        while(Vector3.Distance(target.position, newPos) > 0.01)
        {
            target.position = Vector3.Lerp(target.position, newPos, Time.deltaTime * 6f);
            yield return null;
        }

        target.position = newPos;
        yield return null;
    }

}
