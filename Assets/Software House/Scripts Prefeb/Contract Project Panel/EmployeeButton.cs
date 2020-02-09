using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeButton : MonoBehaviour
{
    public Image image;
    public TMP_Text employeeName;

    public Button ShowInformation;

    private void Start()
    {
        ShowInformation.onClick.AddListener(() => { });
    }

    public void Set(EmployeeData employeeData)
    {
        image.sprite = CharacterAsset.instance.GetCharacterIcon(employeeData.characterID);
        employeeName.text = employeeData.name;
    }
}
