using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAnim : MonoBehaviour
{
    public float destroyTime = 0f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
