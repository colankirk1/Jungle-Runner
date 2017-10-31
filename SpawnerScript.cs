using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject[] thingsToSpawn;
    public float minDelay;
    public float maxDelay;
    public float belowSpawner;
    public float aboveSpawner;
    public bool clouds;
    public bool backgroundPlatform;

    //change random initial spawn based on what kind of spawner
    void Start()
    {
        if (clouds)
        {
            Instantiate(thingsToSpawn[Random.Range(0, thingsToSpawn.Length)], new Vector3(Random.Range(-7, 7), Random.Range(transform.position.y - belowSpawner, transform.position.y + aboveSpawner)), Quaternion.identity);
        }
        else if (backgroundPlatform)
        {
            Instantiate(thingsToSpawn[Random.Range(0, thingsToSpawn.Length)], new Vector3(Random.Range(-7, 5), Random.Range(transform.position.y - belowSpawner, transform.position.y + aboveSpawner)), Quaternion.identity);
        }

        Invoke("Spawn", Random.Range(0, 2));
    }

    void Spawn()
    {
        Instantiate(thingsToSpawn[Random.Range(0, thingsToSpawn.Length)], new Vector3(transform.position.x, Random.Range(transform.position.y-belowSpawner, transform.position.y+aboveSpawner)), Quaternion.identity);
        Invoke("Spawn", Random.Range(minDelay, maxDelay));
    }

    
}