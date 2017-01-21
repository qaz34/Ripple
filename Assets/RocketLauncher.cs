using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon
{

    public override GameObject Fire(Transform playerLoc, bool pressed)
    {
        if (!pressed)
        {
            GameObject bullet = base.Fire(playerLoc, pressed);
            if (bullet != null)
                bullet.GetComponent<Rigidbody>().AddForce(playerLoc.forward * speed, ForceMode.Impulse);
        }
        return null;
    }
}
