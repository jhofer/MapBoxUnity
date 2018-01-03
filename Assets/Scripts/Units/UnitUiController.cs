using ProgressBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUiController : MonoBehaviour {

    private UnitData unitData;
    // Use this for initialization
    void Start () {
        unitData = GetComponent<UnitData>();
	}
	
	// Update is called once per frame
	void Update () {
        var progressBar = GetComponentInChildren<ProgressBarBehaviour>();
        progressBar.Value = unitData.HealthPercentage;
    }
}
