﻿using UnityEngine;
using System.Collections.Generic;

public class WeaponControler : MonoBehaviour {
    public List<Weapon> weapons = new List<Weapon>();    
    public int equipWeapon;
	// Use this for initialization
	void Start () {
	
	}
	public void Fire()
    {
        weapons[equipWeapon].Fire();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
