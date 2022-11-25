using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MyManager : MonoBehaviour
{
    public PlacementWithDraggingDroppingController plc;
   public ARTrackedImageManager ARTrackedImageManager;
    
   
    public void EraseObj()
    { 
         plc.placedObject.SetActive(false);
         plc.placedObject = null;
    }
    public void Del()
    {
        ARTrackedImageManager.trackedImagePrefab.SetActive(false);
        ARTrackedImageManager.trackedImagePrefab = null;
    }
}
