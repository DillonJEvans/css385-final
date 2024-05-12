using UnityEngine;


public class CameraFollower : MonoBehaviour
{
    public PlayerController player;
    [Range(-0.5f, 0.5f)]
    public float lookDirectionWeight;
    [Min(0f)]
    public float floatiness;
    public bool stop;


    private Vector3 velocity = Vector3.zero;


    private void FixedUpdate()
    {
        if (!stop)
        {
            Vector3 target = TargetPosition();
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, floatiness);
        }
    }


    private Vector3 TargetPosition()
    {
        return new(
            player.transform.position.x + lookDirectionWeight * player.lookDirection.x,
            player.transform.position.y + lookDirectionWeight * player.lookDirection.y,
            transform.position.z
        );
    }
}
