using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
    public override GameObject Fire()
    {
        GameObject bullet = base.Fire();
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        return null;
    }
}
