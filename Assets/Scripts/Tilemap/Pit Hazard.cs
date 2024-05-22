using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitHazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            PlayerController2 player = collision.gameObject.GetComponent<PlayerController2>();
            player.currentHealth = 1;
            player.TakeDamage();
        }
        else if (collision.gameObject.layer == 8)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.currentHealth = 1;
            enemy.TakeDamage(1);
        }
    }
}
