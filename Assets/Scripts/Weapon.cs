using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public float triggerDelay;
    protected float lastFired;
    protected int bullets;
    public float speed;
    public int capacity;
    public GameObject ammunition;
    void Start()
    {
        capacity = bullets;
    }
    public virtual GameObject Fire()
    {   
        GameObject bullet = Instantiate<GameObject>(ammunition);
        bullet.transform.position = transform.position;

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInParent<Collider>());
        return bullet;
    }
}
