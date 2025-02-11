using TMPro;
using UnityEngine;

public class RobotCounter : MonoBehaviour
{
    private TMP_Text displayText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = "<b>Robots:</b>" + GameManager.RobotCount.ToString();
    }
}
