using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private float timeToLive = 60f;
    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

}
