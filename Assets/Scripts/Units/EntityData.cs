﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EntityData : MonoBehaviour, IDestroyable {
    [Header("Unit")]
    [Tooltip("Health of the Object")]
    public float currentHealth = 90;
    public float maxHealth = 100;
    public string owner = "Jonas"; //TODO Use real players
	private Guid id= Guid.NewGuid();

	public Guid Id {
		get { return id; } 
	}

    public void ApplyDamage(float damage)
    {
        var newHealth = currentHealth - damage;
        if (newHealth > 0)
        {
            currentHealth = newHealth;
        }
        else
        {
            currentHealth = 0;
        }
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }
}
