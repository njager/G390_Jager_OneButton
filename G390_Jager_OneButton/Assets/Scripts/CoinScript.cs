using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Update is called once per frame ; rotates coin
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }

}
