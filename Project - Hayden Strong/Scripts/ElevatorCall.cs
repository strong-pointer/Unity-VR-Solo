using System.Collections;
using UnityEngine;

public class ElevatorCall : MonoBehaviour
{
    public Vector3 elevatorPositionToMoveTo;
    public GameObject Elevator;

    public Vector3 doorLeftPositionToMoveTo;
    public Vector3 doorRightPositionToMoveTo;
    public GameObject doorLeft;
    public GameObject doorRight;

    // Amount of time modifier the doors take to open close
    public float lerpDuration = 3;

    private Coroutine routine;
    Vector3 startPositionR;
    Vector3 startPositionL;

    // Enters the trigger area (~2m in front of elevator)
    void OnTriggerEnter(Collider other)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(LerpDoors(doorRightPositionToMoveTo, doorLeftPositionToMoveTo, elevatorPositionToMoveTo));
    }

    private void Start()
    {
        startPositionR = doorRight.transform.position;
        startPositionL = doorLeft.transform.position;
    }

    private void Update()
    {
        // If the doors are open, and the elevator is not there, close the doors
        if (doorRight.transform.position == doorRightPositionToMoveTo && Elevator.transform.position != elevatorPositionToMoveTo)
        {
            StopCoroutine(routine);
            StartCoroutine(CloseDoors());
        }
    }

    IEnumerator CloseDoors()
    {
        Vector3 currentPosR, currentPosL;
        currentPosR = doorRight.transform.position;
        currentPosL = doorLeft.transform.position;
        // Closing the doors
        float timeElapsed = 0;
        while (timeElapsed < 1)
        {
            doorRight.transform.position = Vector3.Lerp(currentPosR, startPositionR, timeElapsed / 1);
            doorLeft.transform.position = Vector3.Lerp(currentPosL, startPositionL, timeElapsed / 1);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        doorRight.transform.position = startPositionR;
        doorLeft.transform.position = startPositionL;
    }

    // Moving the Doors
    IEnumerator LerpDoors(Vector3 targetPositionR, Vector3 targetPositionL, Vector3 targetPosition)
    {
        float timeElapsed = 0;
        // Check to see if elevator is at door called, if not, move elevator to floor, then open doors on that floor
        if (Elevator.transform.position != elevatorPositionToMoveTo)
        {
            Vector3 startPosition = Elevator.transform.position;
            while (timeElapsed < lerpDuration)
            {
                Elevator.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            Elevator.transform.position = targetPosition;
        }


        timeElapsed = 0;
        Vector3 startPositionRn = doorRight.transform.position;
        Vector3 startPositionLn = doorLeft.transform.position;

        // Opening Doors
        while (timeElapsed < 1)
        {
            doorRight.transform.position = Vector3.Lerp(startPositionRn, targetPositionR, timeElapsed / 1);
            doorLeft.transform.position = Vector3.Lerp(startPositionLn, targetPositionL, timeElapsed / 1);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        doorRight.transform.position = targetPositionR;
        doorLeft.transform.position = targetPositionL;

        // CLOSE DOORS AFTER 5 SECONDS OF BEING OPENED
            // This is just the counter
        float duration = 5f; 
        timeElapsed = 0;
        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime / duration;
            yield return null;
        }

            // Actually closing the doors
        timeElapsed = 0;
        while (timeElapsed < 1)
        {
            doorRight.transform.position = Vector3.Lerp(targetPositionR, startPositionR, timeElapsed / 1);
            doorLeft.transform.position = Vector3.Lerp(targetPositionL, startPositionL, timeElapsed / 1);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        doorRight.transform.position = startPositionR;
        doorLeft.transform.position = startPositionL;
    }
}
