using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeCard : MonoBehaviour
{
    public Image employeeImage;

    public TMP_Text employeeName;

    public Transform rarityPanel;
    public Transform jobIconPanel;
    public Transform jobLevelPanel;

    public TMP_Text employeeRace;
    public TMP_Text employeeSalary;
    public TMP_Text employeeMood;
    public TMP_Text employeeStamina;

    public Button hireButton;

    [SerializeField] private EmployeeData employeeData;

    private void Start()
    {

        if (hireButton != null)
        {
            hireButton.onClick.AddListener(() => Hire());

            if (employeeData.employeeStatus == EmployeeStatus.MyEmployee)
            {
                hireButton.interactable = false;
                hireButton.image.color = Color.gray;
            }
        }
    }

    public void SetCardUI(EmployeeData employeeData)
    {

        this.employeeData = employeeData;

        employeeName.text = employeeData.name;
        employeeSalary.text = employeeData.salary+"";
        employeeMood.text = employeeData.mood_max+"";
        employeeStamina.text = employeeData.stamina_max+"";

        Character character = CharacterAsset.instance.GetCharacter(employeeData.characterID);

        employeeImage.sprite = character.sprite;
        employeeRace.text = "Move Spd " + character.speed;

        for (int i = 0; i < 3; i++)
        {
            if (i < employeeData.jobs.Count)
            {
                jobIconPanel.GetChild(i).GetComponent<Image>().sprite = JobAsset.instance.GetJobSprite(employeeData.jobs[i].jobType);
                jobLevelPanel.GetChild(i).GetComponent<TMP_Text>().text = "Lv."+employeeData.jobs[i].level.ToString();
            }
            else
            {
                jobIconPanel.GetChild(i).gameObject.SetActive(false);
                jobLevelPanel.GetChild(i).gameObject.SetActive(false);
            }
        }


        for (int i = 0; i < 5; i++)
        {
            if (i < employeeData.jobs.Count-1)
            {
                rarityPanel.gameObject.SetActive(true);
            }
            else
            {
                rarityPanel.gameObject.SetActive(false);
            }
        }
    }

    public void Hire()
    {
        EmployeeManager.instance.HireEmployee(employeeData);
        MessageSystem.instance.UpdateMessage("Hire Employee: " + employeeData.employeeID);

        hireButton.interactable = false;
        hireButton.image.color = Color.gray;
    }

    public void InformationMode()
    {
        hireButton.gameObject.SetActive(false);
    }

    private void DestroyCard()
    {
        Destroy(this.gameObject);
    }
}
