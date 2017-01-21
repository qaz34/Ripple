using UnityEngine;
using System.Collections;

public class BulletWave : MonoBehaviour
{
    WaveSim ws;
    public float intensity = 1;
    bool shot;
    // Use this for initialization
    void Start()
    {
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
        shot = false;
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {      
            ws.Disturb(intensity, transform.position);
    }
}
