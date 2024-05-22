using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public int damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " Hit: " + collision.gameObject.name);
        if (collision.gameObject.layer == 8 && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        else if (collision.gameObject.layer == 7)// && collision.gameObject != gameObject.transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<PylonScript>().ActivateEffect(gameObject.name);
        }
        else if (collision.gameObject.layer == 13)
        {
            collision.gameObject.GetComponent<Landmine>().Explode();
        }
    }
}
