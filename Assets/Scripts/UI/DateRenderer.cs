using Time;
using TMPro;
using UnityEngine;

public class DateRenderer : MonoBehaviour
{
    private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float date = TimeManager.Current.GetNumberRaw();
        date = (Mathf.Round(date));
        text.text = date+" SECONDS";
    }


}
