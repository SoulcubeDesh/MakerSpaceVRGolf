using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public Ball ball;


    public Hole currentHole;
    public GameObject player;

    public List<Hole> allHoles = new List<Hole>();

    public static HoleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartNewHole(allHoles[0]);
    }

    public Hole getNextHole()
    {
       int currentIndex = allHoles.IndexOf(currentHole);
        int newIndex = currentIndex + 1;
        if (newIndex >= allHoles.Count)
        {
            newIndex = 0;
        }

        return allHoles[newIndex];
    }

    public void CompleteHole()
    {
        currentHole = getNextHole();
        StartNewHole(currentHole);
    }

    public void StartNewHole(Hole hole)
    {
        currentHole = hole;
        player.transform.position = hole.startingPoint.playerSpawnPoint.position;
        ball.transform.position = hole.startingPoint.ballSpawnPoint.position;
    }
}
