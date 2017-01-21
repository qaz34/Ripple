using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

[System.Serializable]
public struct PlayerSounds
{
    public AudioClip step;
    public AudioSource audioSource;
}
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponControler))]
public class CharController : MonoBehaviour
{
    public InputDevice device;
    public PlayerSounds sounds;
    public float speed = 10;
    CharacterController m_cc;
    WaveSim ws;
	Text t;
	WeaponControler wc;
    Vector3 move;
    float timer = 0;
    public float stepFreq = 0.2f;
    public float stepIntensity = 0.5f;
    public int player = 1;

    bool pressed = false;
    // Use this for initialization
    void Start()
    {
		wc = GetComponent<WeaponControler> ();
        m_cc = GetComponent<CharacterController>();
        ws = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSim>();
        transform.rotation = Quaternion.Euler(90, 0, 0);
		t = GameObject.FindGameObjectWithTag("P" + player + "WeapText").GetComponent<Text>();
		t.text = wc.weapName;
    }

	void Awake()
	{
		
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
            if (device.RightBumper.IsPressed)
            {
                GetComponent<WeaponControler>().Fire(pressed);
                pressed = true;
            }
            else if (device.RightBumper.WasReleased)
            {
                pressed = false;
            }
            if (device.AnyButton.WasPressed)
            {
                int wep = -1;
                if (device.Action1.WasPressed)
                    wep = 0;
                else if (device.Action2.WasPressed)
                    wep = 1;
                else if (device.Action3.WasPressed)
                    wep = 2;
                else if (device.Action4.WasPressed)
                    wep = 3;
				if (wep != -1) {
					GetComponent<WeaponControler> ().Equip (wep);
				}

            }
			t.text = wc.weapName;
            //if (device.Action2.WasPressed || device.Action1.WasPressed)
            //         {
            //             GetComponent<WeaponControler>().Equip((int)(device.Action2.Value-device.Action1.Value));
            //         }
        }
    }

    void OnDestroy()
    {
        ws.Disturb(10, transform.position);
    }
}
