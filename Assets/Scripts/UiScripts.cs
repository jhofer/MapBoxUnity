using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class UiScripts : MonoBehaviour {


    [SerializeField]
    CameraMovement cam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        cam.moveToPlayer = true;
        Debug.Log("klic");
    }
}
