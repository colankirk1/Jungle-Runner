using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for moving objects around instead of instantiating them
public class ScenerySpawnerScript : MonoBehaviour {

    public GameObject[] thingsToSpawn;

    //ID of each background object
    private int offset = 1;

    public void callSpawn()
    {
        thingsToSpawn[(Random.Range(0, thingsToSpawn.Length/2) * 2) + offset].transform.position = new Vector2(transform.position.x, transform.position.y);
        if (offset == 0)
            offset = 1;
        else
            offset = 0;
    }
}
