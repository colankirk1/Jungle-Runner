using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    private Transform player;
    private float offset = 0;
    private float spriteSize;
    public float speedPercentage;
    public bool twoPartLoop;     //if second background image

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(twoPartLoop)
         spriteSize = GetComponent<SpriteRenderer>().bounds.size.x;
        //offset required to make the image spawn where it is in the game view
        offset = transform.position.x-player.transform.position.x + (player.position.x * (1-speedPercentage));
    }
	
    //Move the background objects a percentage of the player's distance to create pseudo-parallax
	void Update () {
        transform.position = new Vector2((player.position.x * speedPercentage) + offset, transform.position.y);
    }

    //If it goes offscreen, send it to the back
    void OnBecameInvisible()
    {
        if (twoPartLoop) {
            offset += spriteSize * 2;
        }
    }
}
