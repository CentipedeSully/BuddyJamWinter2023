using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDisableSFX : MonoBehaviour
{
    public GameObject ObjectToDisable;

    private void OnDisable()
    {
        if (ObjectToDisable != null)
        {
            ObjectToDisable.SetActive(false);
        }
    }
}
