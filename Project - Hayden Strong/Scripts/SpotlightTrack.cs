using UnityEngine;
using System.Collections.Generic;

public class SpotlightTrack : MonoBehaviour
{
    public GameObject lightSource;
    public GameObject rotator;
    public GameObject holder;

    public GameObject lightSource2;
    public GameObject rotator2;
    public GameObject holder2;

    private GameObject personToTrack;
    private Quaternion rot1, rot2, rot3, rot4;
    List<GameObject> stack = new List<GameObject>();

    private void Start()
    {
        rot1 = rotator.transform.rotation;
        rot2 = holder.transform.rotation;

        rot3 = rotator2.transform.rotation;
        rot4 = holder2.transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (lightSource.activeSelf && lightSource2.activeSelf)
        {
            rotator.transform.LookAt(personToTrack.transform.position);
            rotator.transform.eulerAngles = new Vector3(0, rotator.transform.eulerAngles.y, 0);

            holder.transform.LookAt(personToTrack.transform.position);

            rotator2.transform.LookAt(personToTrack.transform.position);
            rotator2.transform.eulerAngles = new Vector3(0, rotator2.transform.eulerAngles.y, 0);

            holder2.transform.LookAt(personToTrack.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent") || other.CompareTag("Player"))
        {
            lightSource.SetActive(true);
            lightSource2.SetActive(true);
            personToTrack = other.gameObject;
            stack.Add(personToTrack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stack.Remove(other.gameObject);
        // When the list is empty (no one in the atrium), turn off the spotlight
        if (stack.Count == 0)
        {
            lightSource.SetActive(false);
            lightSource2.SetActive(false);
            holder.transform.rotation = rot2;
            rotator.transform.rotation = rot1;

            holder2.transform.rotation = rot4;
            rotator2.transform.rotation = rot3;
        }
        // If the list is not empty (someone is in the room), do not turn off the light, but rather track the next in line
        else
        {
            personToTrack = stack[stack.Count - 1];
        }
    }
}
