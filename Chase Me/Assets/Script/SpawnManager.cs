using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject pickUp;
    private float xRange = 23;
    private float zRange = 23;

    private float spawnDelay = 1;
    private float pickupSpawnRate = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RandomSpawnPosition", spawnDelay, pickupSpawnRate);
    }

    void RandomSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), transform.position.y, Random.Range(-zRange, zRange));
        GameObject newPickUp = Instantiate(pickUp, spawnPosition, Quaternion.identity);
        StartCoroutine(DestroyAfterSpawn(newPickUp, 6));
    }


    IEnumerator DestroyAfterSpawn(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
