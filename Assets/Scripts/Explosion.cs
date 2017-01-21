using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class Explosion : MonoBehaviour {

	public float initialScale = 0.0f;
	public float intensity = 10.0f;
	public float duration = 0.25f;
	public float maxScale = 1.0f;
	float totalTime = 0.0f;

	void Start () {
		transform.localScale = transform.localScale = Vector3.one * initialScale;
		GameObject.FindGameObjectWithTag ("Wave").GetComponent<WaveSim> ().Disturb (intensity, transform.position);
	}

	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.one * Mathf.Lerp (initialScale, maxScale, (totalTime += Time.deltaTime) / duration);
		if (totalTime > duration) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			Destroy (other.gameObject);
		}
	}
}
