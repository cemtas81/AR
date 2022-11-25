using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CamChange : MonoBehaviour
{
    public ARCameraManager camManager;
    public ARSession aRSession;
    public ARSessionOrigin origin;
    public void CamChange1()
    {
        camManager.requestedFacingDirection = CameraFacingDirection.User;
        aRSession.Reset();
        origin.enabled = false;
    }
    public void CamChange2()
    {
        camManager.requestedFacingDirection = CameraFacingDirection.World;
        aRSession.Reset();
        origin.enabled = true;
    }

}
