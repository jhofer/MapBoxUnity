using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UnitListUpdater : MonoBehaviour {


	[SerializeField]
	public GameObject itemPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var units = GameManager.instance.GetPlayerUnits();


		var uiItem = GetComponentsInChildren<UnitDisplay>().Select(u=>u.data);




		foreach (var item in units) {
			if(!uiItem.Select(p=>p.Id).Contains(item.Id)){
				var newItem = Instantiate(itemPrefab);
				newItem.GetComponent<UnitDisplay> ().data = item;
				newItem.transform.parent = gameObject.transform;
			}
		}

		foreach (var item in uiItem) {
			if(!units.Select(p=>p.Id).Contains(item.Id)){
				Destroy(item.gameObject);
			}
		}

	}
}
