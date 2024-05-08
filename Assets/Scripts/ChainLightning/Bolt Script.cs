using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if collision is a pylon & not parent
        if (collision.gameObject.layer == 7 && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<PylonScript>().ActivateEffect();
        }
        if (collision.gameObject.layer == 8 && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<EnemyScript>().ActivateEffect();
        }
    }
}
