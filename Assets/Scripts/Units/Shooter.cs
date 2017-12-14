using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<AimSystem>().Locked)
        {
            var shootingAnimator = GetComponentInChildren<IShootingAnimation>();
            shootingAnimator.Fire();

        }
    }
}
