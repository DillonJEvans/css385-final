using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class MeleeBehavior : MonoBehaviour
{
    public string playerTag = "Player";
    public float damagePerSecond = 2f;


    private Health player = null;
    private float timeLastDamaged = -1f;


    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (!playerObject)
        {
            Debug.Log(name + " failed to find the player (no object with the tag \"" + playerTag + "\").");
        }
        if (!playerObject.TryGetComponent(out player))
        {
            Debug.Log(name + " failed to find the player's health component.");
        }
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (player && collider.gameObject == player.gameObject)
        {
            player.Damage(damagePerSecond, ref timeLastDamaged);
        }
    }
}
