using UnityEngine;

/// <summary>
/// Rotates the object to look at the player.
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    // Reference to the player's camera
    private Camera playerCamera;

    /// <summary>
    /// Initializes the reference to the player's camera.
    /// </summary>
    private void Start()
    {
        playerCamera = Camera.main;
    }

    /// <summary>
    /// Rotates the object to look at the player's camera.
    /// </summary>
    private void Update()
    {
        if (playerCamera != null)
        {
            transform.LookAt(playerCamera.transform);
            transform.Rotate(Vector3.up, 180);
        }
    }
}
