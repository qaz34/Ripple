using UnityEngine;
using System.Collections;

public class Pulser : MonoBehaviour
{
    public float interval = .5f;
    public float intensity = 1;
    float timer;
    WaveSim ws;
    // Use this for initialization
    void Start()
    {
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer -= interval;
            ws.Disturb(intensity, transform.position);
        }
    }
}
