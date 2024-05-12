using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTrigger : MonoBehaviour
{
    public Tilemap door_;
    public Tile door_tile_;
    public int id_;
    public bool double_sided_ = true;
    public bool delayed_close = false;
    private float close_time = 0.5f;
    private float activation_time;
    bool open = false;
    public List<Vector3Int> door_tile_positions_;

    private void Start()
    {
        //Debug.Log("Door " + id_ + " starts at: " + door_.origin);

    }

    private void Update()
    {
        if (delayed_close && Time.time > close_time + activation_time)
        {
            Debug.Log("Door Triggered " + id_);
            door_.GetComponent<TilemapCollider2D>().enabled = open;
            open = !open;
            foreach (Vector3Int pos in door_tile_positions_)
            {
                door_.SetTile(pos, door_tile_);
            }
            delayed_close = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Door Triggered" + id_);
        door_.GetComponent<TilemapCollider2D>().enabled = open;
        open = !open;
        if (open)
        {
            foreach (Vector3Int pos in door_tile_positions_)
            {
                door_.SetTile(pos, null);
            }
        }
        else if (double_sided_)
        {
            foreach (Vector3Int pos in door_tile_positions_)
            {
                door_.SetTile(pos, door_tile_);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!double_sided_)
        {
            delayed_close = true;
            activation_time = Time.time;
        }
    }
}
