using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocomotionChange : MonoBehaviour
{
    public GameObject XRrig;
    public GameObject ray;
    public bool rayOn;
    public string ScriptOffName;
    public string ScriptOnName;

    //public UnityEvent<Collider> onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            (XRrig.GetComponent(ScriptOffName) as MonoBehaviour).enabled = false;
            (XRrig.GetComponent(ScriptOnName) as MonoBehaviour).enabled = true;

            // turn off ray (turning on walking)
            if (!rayOn)
            {
                ray.SetActive(false);
            }
            // turn on ray (turning on teleporting)
            else
            {
                ray.SetActive(true);
            }
        }
    }
}
