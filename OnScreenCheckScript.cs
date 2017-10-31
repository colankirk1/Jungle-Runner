using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenCheckScript : MonoBehaviour {

    private GameObject temp;    //a temporary to keep track of the current object on the destroyer
    public HazardSpawnerScript hazardSpawner;
    public float waitTime = 0;

    //Spawn the next background on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        temp = collision.gameObject;
        switch (temp.tag)
        {
            case "HazardSpawnNext":
                hazardSpawner.callSpawn(waitTime);
                break;
        }
    }

    public void addWait(float x)
    {
        waitTime += x;
        if (waitTime > 0.3f)
            waitTime = 0.3f;
    }
}
