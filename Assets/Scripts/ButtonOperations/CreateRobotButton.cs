using UnityEngine;
using Time;
using UnityEngine.UI;
using UI;
using UnityEngine.Events;
using Unity.Collections;

/// <summary>
/// Button UI that on press runs an operation, then on complete runs the operation.
/// </summary>
public class CreateRobotButton : MonoBehaviour
{
    [SerializeField]
    private string id = "_RobCreate0";
    private Button btn;
    [SerializeField]
    private CooldownButtonComponent cooldownButton;

    [SerializeField]
    private string durationVariable = "BotBuildDuration";
    private Time.Time _dur;
    private Operation op;
    [SerializeField]
    private UnityEvent _onComplete;
    [SerializeField]
    private GameOperation operation;
    [SerializeField]
    private string matterCostVariable;
    [SerializeField]
    private string matterVariable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn = cooldownButton.GetComponent<Button>();    
        _dur = new Time.Time(GameManager.GetVariable(durationVariable));
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
        cooldownButton.Interactable = op is null && GameManager.GetVariable(matterCostVariable) <= GameManager.GetVariable(matterVariable);
        if (op != null)
        {
            cooldownButton.coverPercent = 1 - op.GetProgress(Time.TimeManager.Current);
        }
    }
}
