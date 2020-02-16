using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIControllerUI : MonoBehaviour
{
    #region Singleton
    public static AIControllerUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public TMP_Text work;
    public TMP_Text aiState;
    public TMP_Text aiAction;

    public Transform panel;
    public Button buttonRelex;
    public Button buttonWork;

    public Button buttonClosePanel;

    public bool active;

    [SerializeField] private EmployeeData employeeData;
    [SerializeField] private StateController stateController;

    private void Start()
    {
        buttonClosePanel.onClick.AddListener(() => ClosePanel());
        buttonRelex.onClick.AddListener(() => ChangeToRelex());
        buttonWork.onClick.AddListener(() => ChangeToWork());
    }

    public void Set(StateController stateController)
    {
        panel.gameObject.SetActive(true);
        active = true;

        this.stateController = stateController;
        this.employeeData = stateController.employeeData;

        work.text = employeeData.workID;
        aiState.text = employeeData.stateAI;
        aiAction.text = employeeData.actionAI;
    }

    public void ChangeToRelex()
    {
        stateController.current_StateAI.ExitActions(stateController);
        stateController.current_StateAI = AIAsset.instance.FindState("RandomToRelexObject (StateAI)");
    }

    public void ChangeToWork()
    {
        stateController.current_StateAI.ExitActions(stateController);
        stateController.current_StateAI = AIAsset.instance.FindState("RandomToWorkObject (StateAI)");
    }

    public void ClosePanel()
    {
        active = false;
        employeeData = null;
        stateController = null;
        panel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (employeeData != null)
        {
            work.text = employeeData.workID;
            aiState.text = employeeData.stateAI;
            aiAction.text = employeeData.actionAI;
        }
    }
}
