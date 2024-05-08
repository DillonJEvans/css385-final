using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public void ActivateEffect()
    {
        Debug.Log("Enemy Hit");
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
