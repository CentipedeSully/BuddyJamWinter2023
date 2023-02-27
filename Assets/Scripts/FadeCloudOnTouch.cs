using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCloudOnTouch : MonoBehaviour
{
    //Declarations


    //Monobehavior
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TogglePropVisibility>().FadeObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TogglePropVisibility>().ShowObject();
        }
    }


    //Utilites



}
