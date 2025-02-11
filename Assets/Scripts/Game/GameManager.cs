using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Gets the Robot Count in the current instance. (Do not run in awake).
    /// </summary>
    public static int RobotCount { get { return instance.robotCount; } }


    private static GameManager instance;
    private int robotCount = 0;


    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBot()
    {
        this.robotCount++;
    }
}
