using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public List<KeyCode> upKeys = new() { KeyCode.W, KeyCode.UpArrow };
    public List<KeyCode> downKeys = new() { KeyCode.S, KeyCode.DownArrow };
    public List<KeyCode> leftKeys = new() { KeyCode.A, KeyCode.LeftArrow };
    public List<KeyCode> rightKeys = new() { KeyCode.D, KeyCode.RightArrow };


    public void Update()
    {
        Vector3 movement = GetMovement() * speed;
        transform.localPosition += movement * Time.deltaTime;
    }


    private Vector2 GetMovement()
    {
        Vector2 movement = Vector2.zero;
        if (upKeys.Any(key => Input.GetKey(key)))
        {
            movement.y++;
        }
        if (downKeys.Any(key => Input.GetKey(key)))
        {
            movement.y--;
        }
        if (leftKeys.Any(key => Input.GetKey(key)))
        {
            movement.x--;
        }
        if (rightKeys.Any(key => Input.GetKey(key)))
        {
            movement.x++;
        }
        movement.Normalize();
        return movement;
    }
}
