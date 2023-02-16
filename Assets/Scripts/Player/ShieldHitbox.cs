using UnityEngine;

public class ShieldHitbox : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        playerController.OnShieldCollisionEnter(other);
    }
}
