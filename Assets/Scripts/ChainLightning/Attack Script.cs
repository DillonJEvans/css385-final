using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackScript : MonoBehaviour
{
    public List<GameObject> targets;
    bool target_state;
    Vector3 mouse_pos;
    Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    public GameObject lightning_bolt;
    public GameObject lightning_aoe;
    public float attack_cooldown;  //cooldown between attacks
    public float attack_time;      //time of effect for attack (minimum 1 frame)
    float last_attack_time; //time of last attack (must be set by attack)
    // Start is called before the first frame update
    void Start()
    {
        target = transform;
        target_state = true;
    }

    // Update is called once per frame
    void Update()
    {
        // RotationMovement();
        if (Time.time > last_attack_time + attack_cooldown)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                lightning_bolt.SetActive(true);
                last_attack_time = Time.time;
            }
            else if (Input.GetKey(KeyCode.X))
            {
                lightning_aoe.SetActive(true);
                last_attack_time = Time.time;
            }
        }
        else if (Time.time > last_attack_time + attack_time)
        {
            lightning_bolt.SetActive(false);
            lightning_aoe.SetActive(false );
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            target_state = !target_state;
            foreach (GameObject target in targets)
            {
                target.SetActive(target_state);
            }
        }
    }

    private void RotationMovement()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
