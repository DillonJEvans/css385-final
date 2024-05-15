using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit?" + gameObject.name + " on " + collision.gameObject.name);
        //check if collision is a pylon & not parent
        if (collision.gameObject.layer == 7) //&& collision.gameObject != gameObject.transform.parent.gameObject)
        {
            Debug.Log("Hit!");
            collision.gameObject.GetComponent<PylonScript>().ActivateEffect();
        }
        //if (collision.gameObject.layer == 8 && collision.gameObject != gameObject.transform.parent.gameObject)
        //{
            //collision.gameObject.GetComponent<EnemyController>().TakeDamage();
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage();
        }
    }
}
