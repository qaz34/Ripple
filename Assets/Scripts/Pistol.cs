using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
    public override GameObject Fire(Transform playerLoc)
    {
        GameObject bullet = base.Fire(playerLoc);
        if (bullet != null)
            bullet.GetComponent<Rigidbody>().AddForce(playerLoc.forward * speed, ForceMode.Impulse);
        return null;
    }
}
