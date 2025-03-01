using TMPro;
using UnityEngine;

/// <summary>
/// Basic counter to allow prefix text combined with variables. Prefix+[variable value].
/// </summary>
public class Counter : MonoBehaviour
{
    private TMP_Text displayText;

    [SerializeField]
    private string prefix = "<b>Robots:</b>";
    [SerializeField]
    private string variableCounter = "Bots";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = prefix + GameManager.GetVariable(variableCounter);
    }
}
