using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    bool destroyed;
    float explosion_length = 0.5f;
    float explosion_time;

    float exposure_limit;
    float exposure_time;
    private void Update()
    {
        if (destroyed && Time.time > explosion_length + explosion_time)
        {
            Destroy(gameObject);
        }
    }

    //Needs revision for final version (based on continuous exposure and blast all within)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            Debug.Log("landmine Trigger");
            collision.gameObject.GetComponent<EnemyController>().TakeDamage();
            Explode();
        }
        else if (collision.gameObject.layer == 9)
        {
            Debug.Log("landmine Trigger");
            collision.gameObject.GetComponent<PlayerController2>().TakeDamage();
            Explode();
        }
    }

    private void Explode()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        destroyed = true;
        explosion_time = Time.time;
    }
}
