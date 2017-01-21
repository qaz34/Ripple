using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public float reloadTime;
    public float triggerDelay;
    protected float lastFired;
    public int bullets;
    public float speed;
    public int capacity;
    public GameObject ammunition;
    bool reloading;
    void Awake()
    {
        Debug.Log("weapons live");
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
    public virtual GameObject Fire(Transform playerLoc)
    {
        if (bullets == 0 && capacity != 0)
        {
            Reload();
            return null;
        }
        if (Time.time - lastFired > triggerDelay)
        {
            GameObject bullet = Instantiate<GameObject>(ammunition);
            bullet.transform.position = playerLoc.position;
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), playerLoc.GetComponent<Collider>());
            lastFired = Time.time;
            bullets--;
            return bullet;
        }
        return null;
    }
}
