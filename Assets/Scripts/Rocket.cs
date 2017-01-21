using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	WaveSim ws;
   	public Explosion pulsePrefab, explosionPrefab;
	public float interval;
	float timer;
	// Use this for initialization
    void Start()
    {
		ws = GameObject.FindGameObjectWithTag ("Wave").GetComponent<WaveSim> ();   
    }
    void OnCollisionEnter(Collision col)
    {
		GameObject go = Instantiate (explosionPrefab.gameObject);
		go.transform.position = transform.position;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
		ws.Disturb (0.1f, transform.position);
		if((timer+=Time.deltaTime) > interval)
		{
			GameObject go = Instantiate (pulsePrefab.gameObject);
			go.transform.position = transform.position;
			timer = 0;
		}
    }
}
