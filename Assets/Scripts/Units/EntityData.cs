using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour {
    [Header("Unit")]
    [Tooltip("Health of the Object")]
    public float currentHealth = 90;
    public float maxHealth = 100;
    public string owner = "Jonas"; //TODO Use real players
}
