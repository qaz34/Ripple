using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public float reloadTime;
    public float triggerDelay;
    protected float lastFired;
    protected int bullets;
    public float speed;
    public int capacity;
    public GameObject ammunition;
    bool reloading;
    void Start()
    {
        bullets = capacity;
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        bullets = capacity;
        reloading = false;
    }
    public void Reload()
    {
        if (!reloading)
        {
            reloading = true;
            StartCoroutine(Reloading());
        }
    }
    public virtual GameObject Fire()
    {
        if (bullets == 0 && capacity != 0)
        {
            Reload();
            return null;
        }
        if (Time.time - lastFired > triggerDelay)
        {
            GameObject bullet = Instantiate<GameObject>(ammunition);
            bullet.transform.position = transform.position;
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponentInParent<Collider>());
            lastFired = Time.time;
            bullets--;
            return bullet;
        }
        return null;
    }
}
