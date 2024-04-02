using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asetroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 0.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamra;

    private float timer;

    private void Start()
    {
        mainCamra = Camera.main;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SpawnAsteroid();
            timer = secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0,4);
        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch(side)
        {
            case 0:
                // Left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new(1,Random.Range(-1,1));
                break;

            case 1:
                // Right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new(-1,Random.Range(-1,1));
                break;

            case 2:
                // Button
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new(Random.Range(-1,1),1);
                break;

            case 3:
                // Top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new(Random.Range(-1,1),-1);
                break;
        }

        Vector3 worldSpwanPoint = mainCamra.ViewportToWorldPoint(spawnPoint);
        worldSpwanPoint.z = 0;

        GameObject selectedAsteroid = asetroidPrefabs[Random.Range(0,asetroidPrefabs.Length)];
        GameObject asteroidInstance = Instantiate(selectedAsteroid,worldSpwanPoint,Quaternion.Euler(0,0,Random.Range(0,360)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(forceRange.x,forceRange.y);
    }
}