using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour {

    public Transform player;
	
	void LateUpdate () {
         transform.position = new Vector3(player.position.x + 7.5f, 0, -10);
    }
}
