using UnityEngine;
using System.Collections.Generic;

public class WeaponControler : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public int equipWeapon;
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
        equipWeapon = equipWeapon + weapon;
        if (equipWeapon < 0)
            equipWeapon = weapons.Count - 1;
        else
            equipWeapon = equipWeapon % weapons.Count;
        Destroy(equipWep);
        equipWep = Instantiate(weapons[equipWeapon]);
        equipWep.transform.parent = transform;
    }
}
