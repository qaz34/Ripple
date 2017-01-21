using UnityEngine;
using System.Collections;

public class BulletWave : MonoBehaviour
{
    WaveSim ws;
    public float intensity = 1;
    // Use this for initialization
    void Start()
    {
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
    }
    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Player") {
			Destroy (col.gameObject);
		}
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {      
            ws.Disturb(intensity, transform.position);
    }
}
