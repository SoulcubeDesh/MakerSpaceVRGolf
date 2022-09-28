using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public Ball ball;


    public Hole currentHole;
    public GameObject player;
    public GameObject clubHead;
    public Transform clubHeadPos;

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

    public bool ballMoving;
    public float strokeDelayTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartHole();
        }

        if (ballMoving)
        {
            strokeDelayTimer += Time.deltaTime;

            if (strokeDelayTimer > 1 && ball.rb.velocity.magnitude < 0.1f)
            {
                Debug.Log(ball.rb.velocity.magnitude);
                finishStroke();
            }
        }
    }

    public Vector3 playerSpawnOffset;
    public void finishStroke()
    {
        Debug.Log("inishStroke");
        ballMoving = false;
        ball.rb.velocity = Vector3.zero;

        Vector3 newPlayerPos = ball.transform.position + playerSpawnOffset;
        player.transform.position = newPlayerPos;
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

        ball.rb.velocity = Vector3.zero;
        ball.rb.angularVelocity = Vector3.zero;

        clubHead.transform.position = clubHeadPos.position;

        hole.StartHole();
    }

    public void RestartHole()
    {
        StartNewHole(currentHole); 
    }

    public void MakeStroke()
    {
        strokeDelayTimer = 0;
        ballMoving = true;
        currentHole.strokeCount++;
    }
}
