using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentProcess : MonoBehaviour
{
    public Image componentIcon;
    public Image background;

    public GameObject componentNamePanel;
    public TMP_Text componentName;
    public TMP_Text componentLevel;

    [SerializeField] private ComponentData componentData;

    public void Display(ComponentData componentData)
    {
        this.componentData = componentData;

        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);

        componentNamePanel.gameObject.SetActive(true);
        componentIcon.sprite = component.icon;
        componentName.text = component.name.Replace("_","");
        componentLevel.text = componentData.developLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (componentData != null && componentLevel.text != componentData.developLevel.ToString())
        {
            UpLevel();
        }
    }

    public void UpLevel()
    {
        componentLevel.text = componentData.developLevel.ToString();
    }

    public void DownLevel()
    {
        componentLevel.text = componentData.developLevel.ToString();
    }
}
