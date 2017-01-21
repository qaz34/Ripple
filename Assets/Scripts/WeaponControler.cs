using UnityEngine;
using System.Collections.Generic;

public class WeaponControler : MonoBehaviour
{
	public string weapName
	{
		get {
			return weapons[equipWeapon].GetComponent<Weapon>().gunName;
		}
	}
    public List<GameObject> weapons = new List<GameObject>();
    public int equipWeapon = 0;
    GameObject equipWep;
    // Use this for initialization
    void Start()
    {
        equipWep = Instantiate(weapons[equipWeapon]);
        equipWep.transform.parent = transform;
    }
    public void Fire(bool pressed)
    {
        equipWep.GetComponent<Weapon>().Fire(transform, pressed);
    }
    // Update is called once per frame
    public void Equip(int weapon)
    {    
		equipWeapon = weapon;
        Destroy(equipWep);
        equipWep = Instantiate(weapons[weapon]);
        equipWep.transform.parent = transform;
    }
}
