using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentCard : MonoBehaviour
{
    public TMP_Text componentTypeText;
    public Transform content;

    public GameObject slotComponent;
    public GameObject selectEmployeeButton;

    [SerializeField] private int slotMax;

    [SerializeField] private CategorieType categorieType = CategorieType.None;
    [SerializeField] private ComponentType componentType = ComponentType.None;
    [SerializeField] private List<ComponentData> componentData_list = new List<ComponentData>();

    public void CreateMode(CategorieType categorieType, ComponentType componentType, List<ComponentData> componentData,int slotMax)
    {
        this.categorieType = categorieType;
        this.componentType = componentType;
        this.slotMax = slotMax;
        this.componentData_list = componentData;
        this.componentTypeText.text = componentType.ToString();

        RemoveAllSlotComponent();
        AddSlotComponent_CreateMode();
    }

    public void ProductMode(CategorieType categorieType, ComponentType componentType, List<ComponentData> componentData)
    {
        this.categorieType = categorieType;
        this.componentType = componentType;
        this.componentData_list = componentData;
        this.componentTypeText.text = componentType.ToString();

        RemoveAllSlotComponent();
        AddSlotComponent_ProductMode();
    }

    public List<ComponentData> GetComponentData()
    {
        return componentData_list;
    }

    public void RefreshComponentCard_CreateMode()
    {
        RemoveAllSlotComponent();
        AddSlotComponent_CreateMode();
    }

    private void AddSlotComponent_CreateMode()
    {
        for (int i = 0; i < slotMax; i++)
        {
            if (i + 1 <= componentData_list.Count)
            {
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();
                slotComponent_temp.CreateProductMode(componentData_list[i], componentData_list, RefreshComponentCard_CreateMode);
            }
            else
            {
                Instantiate(selectEmployeeButton, content).GetComponent<SelectComponentButton>().Set(categorieType, componentType, componentData_list, RefreshComponentCard_CreateMode, slotMax);
            }
        }
    }

    private void AddSlotComponent_ProductMode()
    {
        for (int i = 0; i < slotMax; i++)
        {
            if (i + 1 <= componentData_list.Count)
            {
                SlotComponent slotComponent_temp = Instantiate(slotComponent, content).GetComponent<SlotComponent>();
                slotComponent_temp.ProcessMode(componentData_list[i]);
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
