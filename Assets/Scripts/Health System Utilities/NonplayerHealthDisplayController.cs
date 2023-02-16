using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonplayerHealthDisplayController : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject _healthBarObject;
    [SerializeField] private HealthBehavior _healthBehaviorRef;


    //Monobehaviors
    private void Awake()
    {
        if (_healthBehaviorRef == null)
        {
            _healthBehaviorRef = GetComponent<HealthBehavior>();
            if (_healthBehaviorRef == null)
            {
                Debug.LogError("NonplayerHealthDisplayError: Null healthBehavior reference. Be sure to either fill the reference manually or " +
                    "keep this script attached to a gameobject with a healthBehavior");
            }   
        }
        if (_healthBarObject == null)
            Debug.LogError("NonplayerHealthDisplayError: Null healthBar Object reference.");
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    //Utilities
    public void UpdateHealthBar()
    {
        if (_healthBarObject != null && _healthBehaviorRef != null)
        {
            float healthRemaining = CalculateNormalizedHealthScore();
            //Debug.Log("health remaining: " + healthRemaining);

            _healthBarObject.transform.localScale = new Vector3(healthRemaining, _healthBarObject.transform.localScale.y, _healthBarObject.transform.localScale.z);
        }
    }

    private float CalculateNormalizedHealthScore()
    {
        if (_healthBehaviorRef.GetMaxHealth() != 0)
            return (float)_healthBehaviorRef.GetCurrentHealth() / (float)_healthBehaviorRef.GetMaxHealth();
        else return 0;
    }


}
