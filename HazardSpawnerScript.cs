using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnerScript : MonoBehaviour
{

    public GameObject[] thingsToSpawn;
    private int random;
    private int slidesInARow;
    private int jumpsInARow;

    void Start()
    {
        random = 0;
        slidesInARow = 0;
        jumpsInARow = 0;
        callSpawn(0);
    }

    //spawns object when called by another gameObject
    public void callSpawn(float wait)
    {
        StartCoroutine(spawnWait(wait));
    }

    IEnumerator spawnWait(float x)
    {
        yield return new WaitForSeconds(x);

        //prevent multiple of the same hazard spawning in a row
        if (slidesInARow == 2)
        {
            random = Random.Range(2, thingsToSpawn.Length);
        }
        else if (jumpsInARow == 3)
        {
            random = Random.Range(0, 1);
        }
        else
        {
            random = Random.Range(0, thingsToSpawn.Length);
        }

        Instantiate(thingsToSpawn[random], new Vector3(transform.position.x, transform.position.y), Quaternion.identity);

        if (random == 0 || random == 1)
        {
            slidesInARow += 1;
            jumpsInARow = 0;
        }
        else
        {
            slidesInARow = 0;
            jumpsInARow += 1;
        }
    }
}
