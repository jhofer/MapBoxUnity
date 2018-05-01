using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public PlayerData(string name, Guid id, float credits=0)
    {
        Name = name;
        Id = id;
        Credits = credits;
    }
    public HashSet<EntityData> unitSelection = new HashSet<EntityData>();
    public HashSet<Vector2d> buildingSelection = new HashSet<Vector2d>();
   
    public string Name { get; private set; }
    public Guid Id { get; private set; }
    public float Credits { get; private set; }
}
