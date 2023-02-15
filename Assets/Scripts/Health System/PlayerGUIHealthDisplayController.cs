using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUIHealthDisplayController : MonoBehaviour
{
    //Declarations
    [Header("Display Settings")]
    [SerializeField] private HealthBehavior _healthReference;
    [Tooltip("The first index of this collection is considered to be the FIRST heart of the health display")]
    [SerializeField] private List<GameObject> _healthUnits;


    //Monobehaviors
    private void Awake()
    {
        if (_healthReference == null)
            _healthReference = GetComponent<HealthBehavior>();
    }

    //Utilites
    private void EmptyHearts()
    {
        foreach (GameObject heart in _healthUnits)
            heart.SetActive(false);
    }

    private void FillToAmount(int value)
    {
        if (value >= 0)
        {
            EmptyHearts();

            if (value == 0)
                return;

            else if (value >= _healthUnits.Count)
            {
                foreach (GameObject heart in _healthUnits)
                    heart.SetActive(true);
            }
            else
            {
                int count = 0;
                while (count < value)
                {
                    _healthUnits[count].SetActive(true);
                    count++;
                }
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        FillToAmount(_healthReference.GetCurrentHealth());
    }

}
