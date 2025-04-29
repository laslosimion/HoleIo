using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("OnCollisionEnter");
    }
}