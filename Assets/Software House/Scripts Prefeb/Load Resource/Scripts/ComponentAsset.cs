using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComponentAsset : MonoBehaviour
{
    #region Singleton
    public static ComponentAsset instance;
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

    private void Start()
    {
        componentGenre = Resources.LoadAll<Component>("Component/1.Genre").ToList();
        componentTheme = Resources.LoadAll<Component>("Component/2.Theme").ToList();
        componentCamera = Resources.LoadAll<Component>("Component/3.Camera").ToList();
        componentGraphic = Resources.LoadAll<Component>("Component/4.Graphic").ToList();
        componentFeature = Resources.LoadAll<Component>("Component/5.Feature").ToList();
        componentPlatform = Resources.LoadAll<Component>("Component/6.Platform").ToList();

        CombineAsset();
    }

    public List<Component> componentGenre;
    public List<Component> componentTheme;
    public List<Component> componentCamera;
    public List<Component> componentGraphic;
    public List<Component> componentFeature;
    public List<Component> componentPlatform;

    public List<Component> componentAsset;

    private void CombineAsset()
    {
        foreach (Component component in componentGenre)
        {
            componentAsset.Add(component);
        }

        foreach (Component component in componentTheme)
        {
            componentAsset.Add(component);
        }

        foreach (Component component in componentCamera)
        {
            componentAsset.Add(component);
        }

        foreach (Component component in componentGraphic)
        {
            componentAsset.Add(component);
        }

        foreach (Component component in componentFeature)
        {
            componentAsset.Add(component);
        }

        foreach (Component component in componentPlatform)
        {
            componentAsset.Add(component);
        }
    }

    public Component GetComponentAsset(string componentID)
    {
        return componentAsset.Find(e => e.componentID == componentID);
    }

    public Sprite GetComponentIcon(string componentID)
    {
        return componentAsset.Find(e => e.componentID == componentID).icon;
    }

    public List<ComponentData> GetAllComponentData()
    {
        List<ComponentData> component_list = new List<ComponentData>();

        foreach (var item in componentAsset)
        {
            component_list.Add(new ComponentData(item, 1, 0));
        }

        return component_list;
    }

    public Dictionary<string, ComponentData> GetStarterComponentGenreDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentGenre)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    public Dictionary<string, ComponentData> GetStarterComponentThemeDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentTheme)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    public Dictionary<string, ComponentData> GetStarterComponentCameraDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentCamera)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    public Dictionary<string, ComponentData> GetStarterComponentGraphicDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentGraphic)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    public Dictionary<string, ComponentData> GetStarterComponentFeatureDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentFeature)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    public Dictionary<string, ComponentData> GetStarterComponentPlatformDict()
    {
        Dictionary<string, ComponentData> component_dict = new Dictionary<string, ComponentData>();

        foreach (var item in componentPlatform)
        {
            component_dict.Add(item.componentID, new ComponentData(item, 1, 0));
        }

        return component_dict;
    }

    // Random

    public List<ComponentData> RandomComponentGenre(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentGenre.Count - 1);

            ComponentData componentData = new ComponentData(componentGenre[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    public List<ComponentData> RandomComponentTheme(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentTheme.Count - 1);

            ComponentData componentData = new ComponentData(componentTheme[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    public List<ComponentData> RandomComponentCamera(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentCamera.Count - 1);

            ComponentData componentData = new ComponentData(componentCamera[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    public List<ComponentData> RandomComponentGraphic(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentGraphic.Count - 1);

            ComponentData componentData = new ComponentData(componentGraphic[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    public List<ComponentData> RandomComponentFeature(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentFeature.Count - 1);

            ComponentData componentData = new ComponentData(componentFeature[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    public List<ComponentData> RandomComponentPlatform(int maxSkill, int maxLevel)
    {
        List<ComponentData> component_list = new List<ComponentData>();

        for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
        {
            int rnd = Random.Range(0, componentPlatform.Count - 1);

            ComponentData componentData = new ComponentData(componentPlatform[rnd], 0, Random.Range(1, maxLevel + 1));

            int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

            if (index >= 0)
            {
                component_list[index].productLevel += componentData.productLevel;
            }
            else
            {
                component_list.Add(componentData);
            }
        }

        return component_list;
    }

    //public List<ComponentData> RandomComponentDataList_Product(int maxSkill, int maxLevel)
    //{
    //    List<ComponentData> component_list = new List<ComponentData>();

    //    for (int i = 0; i < Random.Range(1, maxSkill + 1); i++)
    //    {
    //        int rnd = Random.Range(0, componentAsset.Count - 1);

    //        ComponentData componentData = new ComponentData(componentAsset[rnd], 0, Random.Range(1, maxLevel + 1));

    //        int index = component_list.FindIndex(e => e.componentID == componentData.componentID);

    //        if (index >= 0)
    //        {
    //            component_list[index].productLevel += componentData.productLevel;
    //        }
    //        else
    //        {
    //            component_list.Add(componentData);
    //        }
    //    }
    //    return component_list;
    //}
}
