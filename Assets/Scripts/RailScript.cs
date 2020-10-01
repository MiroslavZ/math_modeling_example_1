using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailScript : MonoBehaviour
{
    public bool BoxIsOnTheRail;

    //скрипт для фиксации соприкосновения груза с рейкой
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Box")
        {
            BoxIsOnTheRail = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.name == "Box")
        {
            BoxIsOnTheRail = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.name == "Box")
        { 
            BoxIsOnTheRail = false;
        }
    }
}
