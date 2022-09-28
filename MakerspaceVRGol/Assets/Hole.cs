using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public StartingPoint startingPoint;
    public EndingHole endingHole;

    public int strokeCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHole()
    {
        strokeCount = 0;
    }
}
