using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InformationComponent : MonoBehaviour
{
    #region Singleton
    public static InformationComponent instance;

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

    public Image componentIcon;
    public TMP_Text componentLevel;
    public TMP_Text componentName;
    public TMP_Text description;
    public Slider sliderEXP;
    public TMP_Text exp;
    public Transform job;

    public Button upLevelButton;
    public Button downLevelButton;

    public Transform levelModePanel;
    public Transform expModePanel;

    private bool researchMode;

    [SerializeField] ComponentData componentData;

    public void SetResearchMode(ComponentData componentData)
    {
        this.componentData = componentData;
        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);
        Job();

        researchMode = true;
        componentIcon.sprite = component.icon;
        componentName.text = component.componentName;
        description.text = component.description;

        levelModePanel.gameObject.SetActive(false);
        expModePanel.gameObject.SetActive(true);
    }

    public void SetProductMode(ComponentData componentData)
    {
        this.componentData = componentData;
        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);
        Job();

        researchMode = false;
        //componentIcon.sprite = component.icon;
        //componentName.text = component.name;
        //description.text = component.description;
        //componentLevel.text = "Lv." + componentData.productLevel;

        //levelModePanel.gameObject.SetActive(true);
        //expModePanel.gameObject.SetActive(false);

        //upLevelButton.onClick.AddListener(()=> { componentData.UpProductLevel(); UpdateLevel(); });
        //downLevelButton.onClick.AddListener(() => { componentData.DownProductLevel(); UpdateLevel();



        componentIcon.sprite = component.icon;
        componentName.text = component.componentName;
        description.text = component.description;

        levelModePanel.gameObject.SetActive(false);
        expModePanel.gameObject.SetActive(true);
    }

    private void Job()
    {
        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);
        for (int i = 0; i < job.childCount; i++)
        {
            job.GetChild(i).gameObject.SetActive(true);

            if (i < component.job.Count)
            {
                job.GetChild(i).GetChild(0).GetComponent<Image>().sprite = JobAsset.instance.GetJobSprite(component.job[i]);
            }
            else
            {
                job.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateEXP()
    {
        sliderEXP.value = (float)componentData.researchExp / componentData.researchMaxExp;

        if (componentData.researchExp == 0)
        {
            exp.text = "Lv." + componentData.researchLevel+" (0%)";
        }
        else
        {
            float temp = (float)componentData.researchExp / componentData.researchMaxExp * 100;
            exp.text = "Lv." + componentData.researchLevel + " ("+ (int)temp + "%)";
        }
    }

    public void UpdateLevel()
    {
        //componentLevel.text = "Lv." + componentData.productLevel;

        sliderEXP.value = (float)componentData.developLevel / componentData.productLevel;

        if (componentData.developLevel == 0)
        {
            exp.text = "Lv." + componentData.developLevel + " (0%)";
        }
        else
        {
            float temp = (float)componentData.developLevel / componentData.productLevel * 100;
            exp.text = "Lv." + componentData.developLevel + " (" + (int)temp + "%)";
        }
    }

    void Update()
    {
        if (researchMode)
        {
            UpdateEXP();
        }
        else
        {
            UpdateLevel();
        }
    }
}
