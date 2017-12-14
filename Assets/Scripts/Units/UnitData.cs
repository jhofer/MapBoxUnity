using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : EntityData {
    
    [Header("Attack")]
    [Tooltip("Time after which next shot will be fired")]
    public float fireDelay = 0.1f;
   
    [Tooltip("Attackable Objects (tags)")]
    public string[] attackable = new string[] { "Unit" };// "Building"

    [Header("Turret values")]
    public float range = 20;
    public float SpeedTurn = 50;
    public float HorizontalConstraint = 360;
    public float UpConstraint = 90;
    public float DownConstraint = -90;


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
