using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCylinderTools : MonoBehaviour
{
    public List<GameObject> levelCylinders = new List<GameObject>();
    public float radius;

    public void GenerateCylinders()
    {
        float angle = 0;
        float angleSize = 360f / (levelCylinders.Count);
        Debug.Log("AS" + angleSize);

        for (int i = 0; i < levelCylinders.Count; i++)
        {
            Debug.Log(angle);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = Mathf.Sin(angle * Mathf.Deg2Rad);
            Debug.Log(new Vector2(x, z));

            Vector3 pos = new Vector3(x * radius, 1, z * radius);
            levelCylinders[i].transform.position = pos;

            angle += angleSize;
        }
    }
}
