using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 10;

    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}
