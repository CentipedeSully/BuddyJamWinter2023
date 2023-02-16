using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SullysToolkit;

public class VirtualCameraHandler : MonoSingleton<VirtualCameraHandler>
{
    //Declarations
    [SerializeField] private CinemachineVirtualCamera _vCameraReference;


    //Monobehaviors
    protected override void InitializeAdditionalFields()
    {
        if (_vCameraReference == null)
            _vCameraReference = GetComponent<CinemachineVirtualCamera>();
    }


    //utilities
    public void SetNewFollowTarget(Transform newTargetTransform)
    {
        _vCameraReference.Follow = newTargetTransform.transform;
    }


}
