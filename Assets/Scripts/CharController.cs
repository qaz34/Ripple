using UnityEngine;
using System.Collections;
[System.Serializable]
public struct PlayerSounds
{
    public AudioClip step;
    public AudioSource audioSource;
}
[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
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
		transform.rotation = Quaternion.Euler (90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal" + player.ToString()), Input.GetAxis("Vertical" + player.ToString()), 0).normalized * Time.deltaTime * speed;
        print(move);
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
        Vector3 aim = new Vector3(Input.GetAxis("AimHorizontal" + player.ToString()), Input.GetAxis("AimVertical" + player.ToString()));
		if (aim.normalized.magnitude != 0)
			transform.rotation = Quaternion.LookRotation (aim.normalized);
		else if (move.normalized.magnitude != 0)
			transform.rotation = Quaternion.LookRotation (move.normalized);
        if (GetComponent<WeaponControler>() != null)
        {
            if (Input.GetAxis("Fire1" + player.ToString()) != 0)
            {
                GetComponent<WeaponControler>().Fire(pressed);
                pressed = true;
            }
            else if (Input.GetAxis("Fire1" + player.ToString()) == 0)
            {
                pressed = false;
            }
            if (Input.GetButtonDown("Swap" + player.ToString()))
            {
                GetComponent<WeaponControler>().Equip((int)Input.GetAxisRaw("Swap" + player.ToString()));
            }
        }
    }
}
