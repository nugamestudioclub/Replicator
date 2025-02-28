using UnityEngine;
using Time;
using UnityEngine.UI;
using UI;
using UnityEngine.Events;

public class CreateRobotButton : MonoBehaviour
{
    [SerializeField]
    private string id = "_RobCreate0";
    private Button btn;
    [SerializeField]
    private CooldownButtonComponent cooldownButton;

    [SerializeField]
    [Tooltip("In Seconds")]
    private float duration = 25f;
    private Time.Time _dur;
    private Operation op;
    [SerializeField]
    private UnityEvent _onComplete;
    [SerializeField]
    private GameOperation operation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = cooldownButton.GetComponent<Button>();    
        _dur = new Time.Time(duration);
        btn.onClick.AddListener(SubmitOperation);
    }

    private void SubmitOperation()
    {
        _onComplete.RemoveListener(OnComplete);
        _onComplete.AddListener(OnComplete);
        op = new Operation(id, Time.TimeManager.Current, Time.TimeManager.Current + _dur, _onComplete);
        Time.TimeManager.QueueOperation(op);
    }

    private void OnComplete()
    {
        op = null;
        GameManager.HandleOperation(operation);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownButton.Interactable = op is null;
        if (op != null)
        {
            cooldownButton.coverPercent = 1 - op.GetProgress(Time.TimeManager.Current);
        }
    }
}
