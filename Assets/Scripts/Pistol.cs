using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
    public override GameObject Fire()
    {


        if (bullets != 0 && lastFired - Time.time > triggerDelay)
        {
            lastFired = Time.time;

            GameObject bullet = base.Fire();
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
            bullets--;
        }
        return null;
    }
}
