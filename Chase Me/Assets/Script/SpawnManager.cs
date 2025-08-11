using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject pickUp;
    public GameObject enemy;
    private float xRange = 23;
    private float zRange = 23;

    private float spawnDelay = 1;
    private float pickupSpawnRate = 5;
    private float enemySpawnRate = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RandomSpawnPosition", spawnDelay, pickupSpawnRate);
        InvokeRepeating("RandomSpawnEnemy", spawnDelay, enemySpawnRate);
    }

    void RandomSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), transform.position.y, Random.Range(-zRange, zRange));
        GameObject newPickUp = Instantiate(pickUp, spawnPosition, Quaternion.identity);
        StartCoroutine(DestroyAfterSpawn(newPickUp, 6));
    }

    void RandomSpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), transform.position.y, Random.Range(-zRange, zRange));
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    IEnumerator DestroyAfterSpawn(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
