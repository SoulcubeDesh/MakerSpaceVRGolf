using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.tag == "Hole")
        {
            EndingHole endingHole = other.GetComponent<EndingHole>();
            HoleManager.Instance.CompleteHole();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter" + collision.gameObject.name);
    }
}
