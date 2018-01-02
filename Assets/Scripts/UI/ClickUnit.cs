using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUnit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Click);
    }
	
	// Update is called once per frame
	void Click() {
       var data= GetComponent<UnitDisplay>().data;
        GameManager.instance.ClearUnitSelection();
        GameManager.instance.AddSelection(data);
	}

}
