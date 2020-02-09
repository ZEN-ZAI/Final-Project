using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentPanel : MonoBehaviour
{
    #region Singleton
    public static RecruitmentPanel instance;

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
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public Transform content;
    public GameObject employeeCard;

    public Button refreshButton;

    void Start()
    {
        RefreshEmployeeCardUI();

        refreshButton.onClick.AddListener(() =>
        {
            EmployeeManager.instance.RandomRecruitmentEmployee(5);
            RefreshEmployeeCardUI();
        });
    }

    public void RefreshEmployeeCardUI()
    {
        RemoveAllEmployeeCardUI();
        AddEmployeeCardUI();
    }

    public void RemoveAllEmployeeCardUI()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void AddEmployeeCardUI()
    {
        foreach (EmployeeData employeeData in EmployeeStructure.instance.GetRecruitmentEmployeeDataList())
        {
            GameObject temp_employeeCard = Instantiate(employeeCard, content);

            temp_employeeCard.GetComponent<EmployeeCard>().SetCardUI(employeeData);
        }
    }
}
