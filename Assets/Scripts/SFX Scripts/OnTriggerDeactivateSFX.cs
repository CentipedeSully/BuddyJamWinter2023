using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDeactivateSFX : MonoBehaviour
{
    public GameObject ObjectToDeactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectToDeactivate.SetActive(false);
        }
    }
}