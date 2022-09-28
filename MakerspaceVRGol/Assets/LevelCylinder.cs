using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCylinder : MonoBehaviour
{
    public MeshDestroy meshDestroy;

    public string levelName;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Club" && !LevelSelect.Instance.levelSelected)
        {
            OnSelect();
        }
    }

    public void OnSelect()
    {
        LevelSelect.Instance.levelSelected = true;
        StartCoroutine(LevelLoadDelay());
        meshDestroy.DestroyMesh();

        
    }

    
}
