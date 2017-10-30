using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamveeController : MonoBehaviour, ISelectable, IMovable {

    public Vector3 target;

    public void Move(Vector3 target)
    {
        this.target = target;
    }

    public void Select()
    {
        Debug.Log("Klick Hamvee");
        GameManager.instance.AddSelection(gameObject);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        NavigationUtils.MoveToTarget(transform, target);
	}

  
  
}
