using UnityEngine;

// big invisible trigger under the level
// if you fall into it you go back to the last checkpoint
public class DeadPlane : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Respawn();
            return;
        }

        // the box can fall too
        PushableBox box = other.GetComponent<PushableBox>();
        if (box != null)
        {
            box.ResetBox();
        }
    }
}
