using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindBubblesQuest : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isNorthEastDresserChecked = false;
    [SerializeField] private bool _isBedChecked = false;
    [SerializeField] private bool _isSouthWestDresserChecked = false;
    [SerializeField] private bool _isSideBedDresserChecked = false;
    [SerializeField] private bool _isFootBedDresserChecked = false;
    [SerializeField] private CheckFriendsQuester _prevFriendsQuestRef;
    [Header("Events")]
    public UnityEvent OnEverythingChecked;

    //Monobehaviors



    //utilites
    private bool IsEverythingChecked()
    {
        if (_isNorthEastDresserChecked && _isBedChecked && _isFootBedDresserChecked && _isSideBedDresserChecked && _isSouthWestDresserChecked && _prevFriendsQuestRef.IsQuestCompleted())
            return true;
        else return false;
    }

    public void CheckBed()
    {
      
            _isBedChecked = true;
            if (IsEverythingChecked())
                OnEverythingChecked?.Invoke();

    }

    public void CheckNEDreser()
    {
  
            _isNorthEastDresserChecked = true;
            if (IsEverythingChecked())
                OnEverythingChecked?.Invoke();
   
         
    }

    public void CheckSWDresser()
    {
    
            _isSouthWestDresserChecked = true;
            if (IsEverythingChecked())
                OnEverythingChecked?.Invoke();
       
    }

    public void CheckFootDresser()
    {
       
            _isFootBedDresserChecked = true;
            if (IsEverythingChecked())
                OnEverythingChecked?.Invoke();
       
    }

    public void CheckSideDresser()
    {
      
            _isSideBedDresserChecked = true;
            if (IsEverythingChecked())
                OnEverythingChecked?.Invoke();
        
       
    }



}
