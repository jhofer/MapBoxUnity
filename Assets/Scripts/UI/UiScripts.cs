using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class UiScripts : MonoBehaviour {




    public void ClickOnUnit(UnitDisplay unit)
    {
        var data = GetComponent<UnitDisplay>().data;
    }

    public void ClickOnMoveToPlayer(CameraMovement cam) {
        cam.moveToPlayer = true;
        Debug.Log("klic");
    }



}
