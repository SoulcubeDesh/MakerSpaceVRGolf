using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect Instance;
    public bool levelSelected;

    private void Awake()
    {
        Instance = this;
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator LevelLoadDelay(string levelName)
    {
        yield return new WaitForSeconds(2);
        LoadLevel(levelName);
    }
}
