using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth is null)
            return;

        playerHealth.Crash();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}