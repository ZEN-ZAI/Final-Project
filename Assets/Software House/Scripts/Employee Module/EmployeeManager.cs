using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmployeeManager : MonoBehaviour
{
    #region Singleton
    public static EmployeeManager instance;
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

    public List<StateController> employee_list;
    public Transform employeeRoot;
    public Transform tempCharacter;

    public void GenerateStaterCharacter()
    {
        EmployeeData employeeData = new EmployeeData();

        employeeData.characterID = 1;
        employeeData.name = "TED";
        employeeData.employeeID = "emp"+0;

        employeeData.mood_max = UnityEngine.Random.Range(80, 150);
        employeeData.stamina_max = UnityEngine.Random.Range(100, 150);

        JobData jobData1 = new JobData();
        JobData jobData2 = new JobData();
        JobData jobData3 = new JobData();

        jobData1.jobType = JobType.Programmer;
        jobData2.jobType = JobType.Graphic;
        jobData3.jobType = JobType.Game_Writer;

        jobData1.level = 2;
        jobData2.level = 2;
        jobData3.level = 2;

        employeeData.AddJob(jobData1);
        employeeData.AddJob(jobData2);
        employeeData.AddJob(jobData3);

        //employeeData.natures = RandomNature(maxNature, maxLevelNature);
        employeeData.stateAI = AIAsset.instance.StarterStateAI().ToString();

        employeeData.salary = 0;

        HireEmployee(employeeData);
    }

    public void RandomRecruitmentEmployee(int count)
    {
        EmployeeStructure.instance.ClearRecruitmentEmployeeData();

        for (int i = 0; i < count; i++)
        {
            EmployeeStructure.instance.AddRecruitmentEmployeeData(GenerateEmployeeData(1, 2, 1, 5));
        }
    }

    public int GetPowerByEmployeeID(string employeeID,ComponentData componentData)
    {
        EmployeeData employeeData = EmployeeStructure.instance.GetMyEmployeeData(employeeID);
        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);

        if (component.job.Contains(employeeData.jobs[0].jobType))
        {
            return employeeData.jobs[0].level;
        }
        else
        {
            return 0;
        }
    }

    public void HireEmployee(EmployeeData employeeData)
    {
        print("Hire: " + employeeData.name + " [ID:" + employeeData.employeeID + "]");
        employeeData.MyEmployee();
        EmployeeStructure.instance.AddEmployeeData(employeeData);

        Character character = CharacterAsset.instance.GetCharacter(employeeData.characterID);
        GameObject new_employee = GenerateEmployee(character, employeeData);
        employee_list.Add(new_employee.GetComponent<StateController>());
    }

    public void BuildEmployee(List<EmployeeData> employee_list)
    {
        foreach (EmployeeData employeeData in employee_list)
        {
            Character character = CharacterAsset.instance.GetCharacter(employeeData.characterID);

            GameObject tempEmployee = GenerateEmployee(character, employeeData);
            tempEmployee.GetComponent<Transform>().position = new Vector3(employeeData.x, employeeData.y, employeeData.z);
            tempEmployee.GetComponent<Transform>().localRotation = new Quaternion(0, employeeData.y, 0, 0);

            this.employee_list.Add(tempEmployee.GetComponent<StateController>());
        }
    }

    public GameObject GenerateEmployee(Character character, EmployeeData employeeData)
    {
        GameObject employee_temp = Instantiate(character.characterPrefab, employeeRoot);
        employee_temp.AddComponent<StateController>();

        employee_temp.GetComponent<StateController>().character = character;
        employee_temp.GetComponent<StateController>().employeeData = employeeData;

        employee_temp.AddComponent<CapsuleCollider>().center = new Vector3(0,7,0);
        employee_temp.GetComponent<CapsuleCollider>().radius = 4.5f;
        employee_temp.GetComponent<CapsuleCollider>().height = 15;

        //employee_temp.name = employee_temp.name.Replace("(Clone)", "").Trim();
        employee_temp.name = employeeData.employeeID.ToString();
        employee_temp.layer = LayerMask.NameToLayer("Character");

        return employee_temp;
    }

    public EmployeeData GenerateEmployeeData(int maxComponent, int maxLevelComponent, int maxNature, int maxLevelNature)
    {
        EmployeeData employeeData = new EmployeeData();

        employeeData.characterID = CharacterAsset.instance.RandomCharacterID();
        employeeData.employeeID = "emp"+UnityEngine.Random.Range(1, 9999999);
        employeeData.name = "Emp:"+ employeeData.employeeID;

        employeeData.employeeStatus = EmployeeStatus.Recruitment;

        employeeData.mood_max = UnityEngine.Random.Range(80,151);
        employeeData.stamina_max = UnityEngine.Random.Range(100, 151);

        for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
        {
            employeeData.AddJob(new JobData().RandomJobData(5));
        }

        employeeData.salary = (employeeData.GetAllJobLevel() * 5000);

        employeeData.stateAI = AIAsset.instance.StarterStateAI().ToString();
        
        return employeeData;
    }
}