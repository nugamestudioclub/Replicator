using UnityEngine;
using FMODUnity;

/// <summary>
/// /// Handles changes in the music system over time.
/// </summary>

public class MusicManager : MonoBehaviour
{
    // layer trigger booleans
    [SerializeField]
    private bool matterLayerIsTriggered = false;
    [SerializeField]
    private bool botLayerIsTriggered = false;

    private float matterNumber;
    private float botNumber;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        matterNumber = GameManager.GetVariable("Matter");
        botNumber = GameManager.GetVariable("Bots");
        
        if (!matterLayerIsTriggered && matterNumber>5)
        {
            matterLayerIsTriggered = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Set Parameter Events/Set_MatterGet_True");
        }

        if (!botLayerIsTriggered && botNumber>1)
        {
            botLayerIsTriggered = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Set Parameter Events/Set_FirstBotCreated_True");
        }
    }
}
