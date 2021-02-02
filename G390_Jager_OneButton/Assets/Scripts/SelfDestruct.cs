using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float timeToLive = 20f;
    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

}
