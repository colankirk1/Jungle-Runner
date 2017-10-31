using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour {

    private bool hasTriggered = false;

    public bool damage()
    {
        if (!hasTriggered)
        {
            return true;
        }
        return false;
    }
}
