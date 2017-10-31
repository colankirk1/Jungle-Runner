using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawnerScript : MonoBehaviour {

    public GameObject[] obj;      //the object to spawn
    private GameObject temp;      //a temporary to keep track of the current object on the destroyer
    private float largeGroundSize;
    private float largeVolcanoSize;
    public ScenerySpawnerScript scenerySpawner;
    
    //aquire sprite sizes
    public void Start()
    {
        largeGroundSize = (obj[0].GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer).bounds.size.x * 3;
        largeVolcanoSize = (obj[1].GetComponent<SpriteRenderer>().bounds.size.x * 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        temp = collision.gameObject;
        switch (temp.tag)
        {
            case "BaseGround":
                temp.transform.position = new Vector2(temp.transform.position.x + largeGroundSize*6, temp.transform.position.y);
                break;
            case "SlowBackgroundObject":
                Destroy(temp);
                Instantiate(obj[1], new Vector2(transform.position.x + largeVolcanoSize, temp.transform.position.y), Quaternion.identity);
                break;
            case "BackgroundSpawnNext":
                scenerySpawner.callSpawn();
                break;
            default:
                Destroy(temp);
                break;
        }
    }
}
