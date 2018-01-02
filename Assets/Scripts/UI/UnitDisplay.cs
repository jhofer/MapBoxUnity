using ProgressBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisplay : MonoBehaviour {

	public EntityData data;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var unitData = GetComponent<UnitDisplay>().data;
        var healthProcentag = unitData.currentHealth / unitData.maxHealth * 100;
        GetComponentInChildren<ProgressBarBehaviour>().Value = healthProcentag;
    }
}
