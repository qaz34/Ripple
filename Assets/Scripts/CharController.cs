﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    public float speed = 10;
    CharacterController m_cc;
    WaveSim ws;
    Vector3 move;
    float timer = 0;
    public float stepFreq = 0.2f;
    public float stepIntensity = 0.5f;
    // Use this for initialization
    void Start()
    {
        m_cc = GetComponent<CharacterController>();
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * Time.deltaTime * speed;
        m_cc.Move(move);
        transform.position.Set(transform.position.x, transform.position.y, 0);
        if (move.magnitude > 0)
        {
            timer += Time.deltaTime;
        }
        if (timer > stepFreq)
        {
            timer = 0;
            ws.Disturb(stepIntensity, transform.position);
        }
        if (move.normalized.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(move.normalized);
        if (GetComponent<WeaponControler>() != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GetComponent<WeaponControler>().Fire();
            }
        }
    }
}
