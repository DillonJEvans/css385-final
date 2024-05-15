using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonScript : MonoBehaviour
{
    public GameObject aoe_field;
    public bool aoe_active;            //debugging tool & condition check
    public float activation_cooldown;  //cooldown between activation from time end
    public float activation_time;      //time of effect (minimum 1 frame; 1 frame future use case for explosions)
    float last_activation_time;        //time of last effect (must be set on last activation)

    void Start()
    {
    }

    void Update()
    {
        //deactivation checks
        if (Time.time > last_activation_time + activation_time && aoe_active)
        {
            Debug.Log("Deactivate");
            aoe_field.SetActive(false);
            aoe_active = false;
            last_activation_time = Time.time;
        }
    }

    public void ActivateEffect()
    {
        Debug.Log("hit " + gameObject.name);
        //if hit by attack (layer check on collider itself); activate effect (assuming no activation cooldown)
        //Secondary case of already being active extending use case
        //if (Time.time > last_activation_time + activation_cooldown || active) {
            //aoe_field.SetActive(true);
            //aoe_active = true;
            //last_activation_time = Time.time;
        //}

        //activation rules:
        //Should only activate once due to proper usage being with enter2d
        aoe_field.SetActive(true);
        aoe_active = true;
        last_activation_time = Time.time;
    }
}
