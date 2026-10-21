using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthRestoreAmount = 20;
    [SerializeField] AudioClip pickupSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null && playerHealth.CompareTag("Player"))
        {
            playerHealth.Heal(healthRestoreAmount);
            if (pickupSFX != null)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            }
            Destroy(gameObject);
        }
    }
}
