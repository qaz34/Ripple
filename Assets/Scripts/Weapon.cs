using UnityEngine;
using System.Collections;
[System.Serializable]
public struct Sounds
{
    public AudioClip fire;
    public AudioClip reload;
    public AudioSource audioSource;
}
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public Sounds sounds;
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
            sounds.audioSource.PlayOneShot(sounds.reload, .05f);
            reloading = true;
            StartCoroutine(Reloading());
        }
    }
    public virtual GameObject Fire(Transform playerLoc, bool pressed)
    {
        if (bullets == 0 && capacity != 0)
        {              
            Reload();
            return null;
        }
        if (Time.time - lastFired > triggerDelay)
        {
            sounds.audioSource.PlayOneShot(sounds.fire, .05f);
           GameObject bullet = Instantiate<GameObject>(ammunition);
			bullet.layer = playerLoc.gameObject.layer;
            bullet.transform.position = playerLoc.position;
            lastFired = Time.time;
            bullets--;
            return bullet;
        }
        return null;
    }
}
