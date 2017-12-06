using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {

    [Header("Unit")]
    [Tooltip("Health of the Object")]
    public float health = 100;

    [Header("Attack")]
    [Tooltip("Time after which next shot will be fired")]
    public float fireDelay = 0.1f;
    [Tooltip("Range of the Turret")]
    public float range = 20;


    [Header("Ammo")]
    [Tooltip("Current available ammo of the gun")]
    public int ammo;
    [Tooltip("Magzine size of the Gun")]
    public int magzineSize = 50;
    [Tooltip("Totale Ammo available for the GUN")]
    public int totalAmmo = 500;
    [Tooltip("Time taken to reload the Gun")]
    public float reloadTime = 2f;
    [Tooltip("Damage done by the bullet")]
    public float ammoDamage = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
