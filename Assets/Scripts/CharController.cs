using UnityEngine;
using System.Collections;
using InControl;

[System.Serializable]
public struct PlayerSounds
{
    public AudioClip step;
    public AudioSource audioSource;
}
[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    public InputDevice device;
    public PlayerSounds sounds;
    public float speed = 10;
    CharacterController m_cc;
    WaveSim ws;
    Vector3 move;
    float timer = 0;
    public float stepFreq = 0.2f;
    public float stepIntensity = 0.5f;
    public int player = 1;
    bool pressed = false;
    // Use this for initialization
    void Start()
    {
        m_cc = GetComponent<CharacterController>();
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
         

        move = device.LeftStick.Vector * Time.deltaTime * speed;
        m_cc.Move(move);
        transform.position.Set(transform.position.x, transform.position.y, 0);
        if (move.magnitude > 0)
        {
            timer += Time.deltaTime;
        }
        if (timer > stepFreq)
        {
            sounds.audioSource.PlayOneShot(sounds.step);
            timer = 0;
            ws.Disturb(stepIntensity, transform.position);
        }
        Vector3 aim = device.RightStick.Vector;
        if (aim.normalized.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(aim.normalized);
        else if (move.normalized.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(move.normalized);
        if (GetComponent<WeaponControler>() != null)
        {
            if (device.RightTrigger.IsPressed)
            {
                GetComponent<WeaponControler>().Fire(pressed);
                pressed = true;
            }
            else if (device.RightTrigger.WasReleased)
            {
                pressed = false;
            }
            if (device.LeftBumper.WasPressed || device.RightBumper.WasPressed)
            {
                GetComponent<WeaponControler>().Equip((int)(device.RightBumper.Value-device.LeftBumper.Value));
            }
        }
    }

    void OnDestroy()
    {
        ws.Disturb(10, transform.position);
    }
}
