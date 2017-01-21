using UnityEngine;
using System.Collections;

public class ARBullet : MonoBehaviour
{
    WaveSim ws;
    public float intensity = 1;
    [Tooltip("0 == 100%, 1 == 50%, 2 = 33% ect")]
    public int max = 1;
    // Use this for initialization
    void Start()
    {
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && Random.Range(0,max) == 0)
        {
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        ws.Disturb(intensity, transform.position);
    }
}
