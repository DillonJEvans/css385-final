using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class PlayerRotator : MonoBehaviour
{
    private PlayerController player;


    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.lookDirection != Vector2.zero)
        {
            transform.up = player.lookDirection;
        }
    }
}
