using UnityEngine;
using System.Collections;

public class SaltRifle : Weapon
{
    public int burst;
    int burstDone;
    public override GameObject Fire(Transform playerLoc, bool pressed)
    {
        if (!pressed || burst >= burstDone)
        {
            if (!pressed)
                burstDone = 1;

            GameObject bullet = base.Fire(playerLoc, pressed);
            if (bullet != null)
            {
                burstDone++;
                bullet.GetComponent<Rigidbody>().AddForce(playerLoc.forward * speed, ForceMode.Impulse);
            }
            if(bullets <= 0 && !reloading)
            {
                Reload();
            }
        }
        return null;
    }
}
