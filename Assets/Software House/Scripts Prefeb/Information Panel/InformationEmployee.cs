using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationEmployee : MonoBehaviour
{
    #region Singleton
    public static InformationEmployee instance;

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

    public EmployeeCard employeeCard;

    public void SetCardUI(EmployeeData employeeData)
    {
        employeeCard.SetCardUI(employeeData);
        employeeCard.InformationMode();
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}
