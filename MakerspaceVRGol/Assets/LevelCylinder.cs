using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCylinder : MonoBehaviour
{
    public MeshDestroy meshDestroy;

    public string levelName;
    public float rotSpeed;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "club" && !LevelSelect.Instance.levelSelected)
        {
            OnSelect();
        }
    }

    public void OnSelect()
    {
        LevelSelect.Instance.levelSelected = true;
        LevelSelect.Instance.StartCoroutine(LevelSelect.Instance.LevelLoadDelay(levelName));
        meshDestroy.DestroyMesh();

        
    }
    private void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
    }

}
