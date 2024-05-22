using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_DroneScript : MonoBehaviour
{
    public float spawn_cooldown;  //cooldown between attacks
    float last_spawn_time; //time of last attack (must be set by attack)
    public int drone_limit;
    public List<GameObject> drones = new List<GameObject>();
    public GameObject drone_prefab;

    int count;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > last_spawn_time + spawn_cooldown)
        {
            if (Input.GetKey(KeyCode.E) && drones.Count < drone_limit)
            {
                //create drone
                GameObject newdrone = Instantiate(drone_prefab, gameObject.transform.position, gameObject.transform.rotation);
                drones.Add(newdrone);
                newdrone.GetComponent<PylonScript>().drones = drones;
                newdrone.name = "drone" + count++;
                last_spawn_time = Time.time;
            }
            if (Input.GetKey(KeyCode.Q) && drones.Count < drone_limit)
            {
                //create drone
                GameObject newdrone = Instantiate(drone_prefab, gameObject.transform.position, gameObject.transform.rotation);
                drones.Add(newdrone);
                newdrone.GetComponent<PylonScript>().drones = drones;
                newdrone.name = "drone" + count++;
                newdrone.GetComponent<PylonNav>().target_player = true;
                last_spawn_time = Time.time;
            }
        }
    }
}
