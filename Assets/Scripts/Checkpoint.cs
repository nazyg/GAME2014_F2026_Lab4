using UnityEngine;

// when the player touches the sign it becomes the new respawn point
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Color activeColor = Color.green;

    private bool isActive;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetCheckpoint(transform.position);
            isActive = true;

            // turn the sign green so you know it saved
            GetComponent<SpriteRenderer>().color = activeColor;
            Debug.Log("Checkpoint saved: " + name);
        }
    }
}
