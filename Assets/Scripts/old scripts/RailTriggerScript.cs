using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ChangeTriggerValue(true);
    }

    private void OnTriggerStay(Collider other)
    {
        ChangeTriggerValue(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeTriggerValue(false);
    }

    void ChangeTriggerValue(bool value)
    {
        GetComponentInParent<RailScript>().BoxIsOnTheRail = value;
    }
}
