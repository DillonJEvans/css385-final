using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : MonoBehaviour
{
    private GameObject player;
    //Dps Revision
    private float last_damage_time;
    public float damage_cooldown = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Midas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time > last_damage_time + damage_cooldown)
        {
            last_damage_time = Time.time;
            // Check if the collider belongs to an enemy
            if (other.gameObject.CompareTag("Player"))
            {
                player.GetComponent<PlayerController2>().TakeDamage();
            }
        }
    }
}
