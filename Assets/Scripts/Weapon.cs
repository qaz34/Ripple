using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    int bullets;
    public float speed;
    public int capacity;
    public GameObject ammunition;
    public virtual GameObject Fire()
    {
        GameObject bullet = Instantiate<GameObject>(ammunition);
        bullet.transform.position = transform.position;

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInParent<Collider>());
        return bullet;
    }
}
