using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BALLTEST : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * force);

            HoleManager.Instance.MakeStroke();
        }
        
    }

    
}
