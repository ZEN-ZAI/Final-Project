using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyEmployeePanel : MonoBehaviour
{
    #region Singleton
    public static MyEmployeePanel instance;

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

    public Image employeeImage;
    public Image jobImage;

    public Image expBar;

    public TMP_Text employeeName;
    public TMP_Text employeeRace;
    public TMP_Text employeeSalary;
    public TMP_Text employeeMood;
    public TMP_Text employeeStamina;

    public TMP_Text jobName;
    public TMP_Text jobLevel;
    public TMP_Text jobExp;

    public List<Button> jobButtons;
    public List<TMP_Text> jobButtonText;

    [SerializeField] private EmployeeData employeeData;

    public Sprite button1;
    public Sprite button2;

    public Transform content;
    public GameObject myEmployeePropCard;

    private GameObject tempCharacter;

    // Start is called before the first frame update
    void Start()
    {
        //Remove Charecter

        for (int i = 0; i < EmployeeManager.instance.tempCharacter.childCount; i++)
        {
            Destroy(EmployeeManager.instance.tempCharacter.GetChild(i).gameObject);
        }

        RefreshMyEmployeePropCard();
        Set(EmployeeStructure.instance.GetMyEmployeeData("emp0"));
    }

    private void Update()
    {
        if (employeeData != null)
        {
            employeeMood.text = employeeData.mood_current + "/" + employeeData.mood_max;
            employeeStamina.text = employeeData.stamina_current + "/" + employeeData.stamina_max;

            jobLevel.text = "Lv." + employeeData.GetPrimaryJob().level;

            jobExp.text = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp() * 100 + "% (" + employeeData.GetPrimaryJob().exp + "/" + employeeData.GetPrimaryJob().GetFullExp() + ")";
            expBar.fillAmount = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp();
        }
    }

    public void Set(EmployeeData employeeData)
    {
        this.employeeData = employeeData;

        if (tempCharacter != null)
        {
            Destroy(tempCharacter.gameObject);
            tempCharacter = null;
        }

        Character character = CharacterAsset.instance.GetCharacter(employeeData.characterID);

        tempCharacter = Instantiate(character.characterPrefab, EmployeeManager.instance.tempCharacter);
        tempCharacter.layer = LayerMask.NameToLayer("Character");

        employeeName.text = employeeData.name;
        employeeRace.text = character.race;
        employeeSalary.text = employeeData.salary.ToString();
        employeeMood.text = employeeData.mood_current + "/"+ employeeData.mood_max;
        employeeStamina.text = employeeData.stamina_current + "/" + employeeData.stamina_max;

        employeeImage.sprite = character.icon;

        jobName.text = employeeData.GetPrimaryJob().jobType.ToString().Replace("_", " ");
        jobLevel.text = "Lv." +employeeData.GetPrimaryJob().level;
        jobImage.sprite = JobAsset.instance.GetJobSprite(employeeData.GetPrimaryJob().jobType);

        jobExp.text = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp() * 100 + "% (" + employeeData.GetPrimaryJob().exp + "/" + employeeData.GetPrimaryJob().GetFullExp() + ")";
        expBar.fillAmount = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp();

        for (int i = 0; i < jobButtons.Count; i++)
        {
            jobButtons[i].image.sprite = button1;
            jobButtons[i].gameObject.SetActive(false);
        }

        jobButtons[employeeData.indexJob].image.sprite = button2;

        if (employeeData.jobs.Count >= 1)
        {
            jobButtons[0].gameObject.SetActive(true);
            jobButtons[0].onClick.AddListener(() =>
            {
                SwicthJob(0);
                ButtonDefault();
                jobButtons[0].image.sprite = button2;
            });

            jobButtonText[0].text = employeeData.jobs[0].jobType.ToString().Replace("_", " ");
        }

        if (employeeData.jobs.Count >= 2)
        {
            jobButtons[1].gameObject.SetActive(true);
            jobButtons[1].onClick.AddListener(() =>
            {
                SwicthJob(1);
                ButtonDefault();
                jobButtons[1].image.sprite = button2;
            });

            jobButtonText[1].text = employeeData.jobs[1].jobType.ToString().Replace("_", " ");
        }

        if (employeeData.jobs.Count >= 3)
        {
            jobButtons[2].gameObject.SetActive(true);
            jobButtons[2].onClick.AddListener(() =>
            {
                SwicthJob(2);
                ButtonDefault();
                jobButtons[2].image.sprite = button2;
            });

            jobButtonText[2].text = employeeData.jobs[2].jobType.ToString().Replace("_", " ");
        }


    }

    private void ButtonDefault()
    {
        for (int i = 0; i < jobButtons.Count; i++)
        {
            jobButtons[i].image.sprite = button1;
        }
    }

    public void SwicthJob(int index)
    {
        MessageSystem.instance.UpdateMessage(employeeData.name + " Swicth to " + employeeData.jobs[index].jobType.ToString());
        employeeData.SetJob(index);

        jobName.text = employeeData.GetPrimaryJob().jobType.ToString().Replace("_", " ");
        jobLevel.text = "Lv." + employeeData.GetPrimaryJob().level;
        jobImage.sprite = JobAsset.instance.GetJobSprite(employeeData.GetPrimaryJob().jobType);

        jobExp.text = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp()*100+"% ("+ employeeData.GetPrimaryJob().exp + "/" + employeeData.GetPrimaryJob().GetFullExp()+")";
        expBar.fillAmount = (float)employeeData.GetPrimaryJob().exp / employeeData.GetPrimaryJob().GetFullExp();
    }

    private void RefreshMyEmployeePropCard()
    {
        RemoveAll();
        AddMyEmployeePropCard();
    }

    private void RemoveAll()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void AddMyEmployeePropCard()
    {
        foreach (var item in EmployeeStructure.instance.GetMyEmployeeDataList())
        {
            GameObject slotEmployee_temp = Instantiate(myEmployeePropCard, content);
            slotEmployee_temp.GetComponent<SlotEmployee>().Set(item);
            //slotEmployee_temp.GetComponent<SlotEmployee>().ShowCardMode();
            slotEmployee_temp.GetComponent<SlotEmployee>().InformationMode();
        }
    }
}
