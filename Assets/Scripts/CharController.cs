using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D m_rb;
    Vector3 move;
    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        move *= Time.deltaTime * speed;
        move = transform.position + move;

    }
    void FixedUpdate()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        m_rb.velocity = move * Time.fixedDeltaTime * speed;
    }
}
