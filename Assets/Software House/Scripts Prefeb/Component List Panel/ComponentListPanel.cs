using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ComponentListPanel : MonoBehaviour
{
    #region Singleton
    public static ComponentListPanel instance;

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

    public TMP_Text header;
    public Transform content;
    public GameObject slotComponent;

    // Target
    [SerializeField] private CategorieType categorieType = CategorieType.None;
    [SerializeField] private ComponentType componentType = ComponentType.None;
    [SerializeField] private List<ComponentData> component_list;
    [SerializeField] private Action refreshConponentCard;
    [SerializeField] private int maxComponentContent;
    // --

    // Start is called before the first frame update
    void Start()
    {
        RefreshSlotComponent();
    }

    public void Set(CategorieType categorieTypeMode, ComponentType componentType, List<ComponentData> component_list, Action refreshConponentCard,int maxComponentContent)
    {
        this.categorieType = categorieTypeMode;
        this.componentType = componentType;
        this.component_list = component_list;
        this.refreshConponentCard = refreshConponentCard;
        this.maxComponentContent = maxComponentContent;
        header.text = componentType.ToString() + " List";

        RefreshSlotComponent();
    }

    public void RefreshSlotComponent()
    {
        RemoveAllSlotComponent();
        AddSlotComponent();
    }

    private void AddSlotComponent()
    {
        if (categorieType == CategorieType.Game && componentType == ComponentType.Genre)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetGenreResearchList();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp,component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
        else if (categorieType == CategorieType.Game && componentType == ComponentType.Platform)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetPlatformResearchList();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp, component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
        else if (categorieType == CategorieType.Game && componentType == ComponentType.Feature)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetFeatureResearchList();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp, component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
        else if (categorieType == CategorieType.Game && componentType == ComponentType.Graphic)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetGraphicResearchList();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp, component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
        else if (categorieType == CategorieType.Game && componentType == ComponentType.Camera)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetCameraResearchList();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp, component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
        else if (categorieType == CategorieType.Game && componentType == ComponentType.Theme)
        {
            List<ComponentData> componentDatas = StartupStructure.instance.GetThemeResearchLst();

            for (int i = 0; i < componentDatas.Count; i++)
            {
                ComponentData componentData_temp = componentDatas[i];
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();

                if (!component_list.Contains(componentDatas[i]) && component_list.Count < maxComponentContent)
                {
                    slotComponent_temp.ComponentListAddMode(componentData_temp, component_list, refreshConponentCard);
                }
                else
                {
                    slotComponent_temp.DisplayOnlyMode(componentData_temp);
                }
            }
        }
    }

    private void RemoveAllSlotComponent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}