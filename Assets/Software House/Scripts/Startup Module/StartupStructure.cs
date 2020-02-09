using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Data
{
    [Serializable]
    public class StartupStructure
    {
        public Dictionary<string, ComponentData> genreResearch = new Dictionary<string, ComponentData>();
        public Dictionary<string, ComponentData> graphicResearch = new Dictionary<string, ComponentData>();
        public Dictionary<string, ComponentData> themeResearch = new Dictionary<string, ComponentData>();
        public Dictionary<string, ComponentData> cameraResearch = new Dictionary<string, ComponentData>();
        public Dictionary<string, ComponentData> featureResearch = new Dictionary<string, ComponentData>();
        public Dictionary<string, ComponentData> platformResearch = new Dictionary<string, ComponentData>();

        public Dictionary<string, Product> products = new Dictionary<string, Product>();

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Dictionary<string, object> result_genreResearch = new Dictionary<string, object>();
            foreach (var item in genreResearch)
            {
                result_genreResearch.Add(item.Key, item.Value.ToDictionary());
            }

            Dictionary<string, object> result_graphicResearch = new Dictionary<string, object>();
            foreach (var item in graphicResearch)
            {
                result_graphicResearch.Add(item.Key, item.Value.ToDictionary());
            }

            Dictionary<string, object> result_themeResearch = new Dictionary<string, object>();
            foreach (var item in themeResearch)
            {
                result_themeResearch.Add(item.Key, item.Value.ToDictionary());
            }

            Dictionary<string, object> result_cameraResearch = new Dictionary<string, object>();
            foreach (var item in cameraResearch)
            {
                result_cameraResearch.Add(item.Key, item.Value.ToDictionary());
            }

            Dictionary<string, object> result_featureResearch = new Dictionary<string, object>();
            foreach (var item in featureResearch)
            {
                result_featureResearch.Add(item.Key, item.Value.ToDictionary());
            }

            Dictionary<string, object> result_platformResearch = new Dictionary<string, object>();
            foreach (var item in platformResearch)
            {
                result_platformResearch.Add(item.Key, item.Value.ToDictionary());
            }

            result["genreResearch"] = result_genreResearch;
            result["graphicResearch"] = result_graphicResearch;
            result["themeResearch"] = result_themeResearch;
            result["cameraResearch"] = result_cameraResearch;
            result["featureResearch"] = result_featureResearch;
            result["platformResearch"] = result_platformResearch;

            Dictionary<string, object> result_product = new Dictionary<string, object>();
            foreach (var item in products)
            {
                result_product.Add(item.Key, item.Value.ToDictionary());
            }

            result["products"] = result_product;

            return result;
        }
    }
}

public class StartupStructure : MonoBehaviour
{
    #region Singleton
    public static StartupStructure instance;

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

    [SerializeField] private Data.StartupStructure startupStructure;

    public bool set;

    public void Set(string json)
    {
        try
        {
            startupStructure = JsonConvert.DeserializeObject<Data.StartupStructure>(json);
        }
        catch (Exception ex)
        {
            MessageSystem.instance.UpdateMessage(ex.ToString());
        }

        set = true;
    }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(startupStructure);
    }

    public void AddProduct(Product product)
    {
        startupStructure.products.Add(product.name, product);
    }

    public void RemoveProduct(string productName)
    {
        startupStructure.products.Remove(productName);
    }

    public Product GetProduct(string productName)
    {
        if (startupStructure.products.ContainsKey(productName))
        {
            return startupStructure.products[productName];
        }
        else
        {
            return null;
        }
    }

    public Data.StartupStructure GetStartupStructure()
    {
        return startupStructure;
    }
    
    public List<Product> GetProductList()
    {
        return startupStructure.products.Values.ToList();
    }

    public List<ComponentData> GetGenreResearchList()
    {
        return startupStructure.genreResearch.Values.ToList();
    }

    public List<ComponentData> GetFeatureResearchList()
    {
        return startupStructure.featureResearch.Values.ToList();
    }

    public List<ComponentData> GetPlatformResearchList()
    {
        return startupStructure.platformResearch.Values.ToList();
    }

    public List<ComponentData> GetGraphicResearchList()
    {
        return startupStructure.graphicResearch.Values.ToList();
    }

    public List<ComponentData> GetCameraResearchList()
    {
        return startupStructure.cameraResearch.Values.ToList();
    }

    public List<ComponentData> GetThemeResearchLst()
    {
        return startupStructure.themeResearch.Values.ToList();
    }

    public void AddComponent(ComponentData componentData)
    {
        Component component = ComponentAsset.instance.GetComponentAsset(componentData.componentID);

        if (component.componentType == ComponentType.Genre)
        {
            startupStructure.genreResearch.Add(componentData.componentID, componentData);
        }
        else if (component.componentType == ComponentType.Theme)
        {
            startupStructure.themeResearch.Add(componentData.componentID, componentData);
        }
        else if (component.componentType == ComponentType.Camera)
        {
            startupStructure.cameraResearch.Add(componentData.componentID, componentData);
        }
        else if (component.componentType == ComponentType.Graphic)
        {
            startupStructure.graphicResearch.Add(componentData.componentID, componentData);
        }
        else if (component.componentType == ComponentType.Feature)
        {
            startupStructure.featureResearch.Add(componentData.componentID, componentData);
        }
        else if (component.componentType == ComponentType.Platform)
        {
            startupStructure.platformResearch.Add(componentData.componentID, componentData);
        }
    }

    public ComponentData GetResearchComponentData(string componentID)
    {
        if (componentID[0] == '1')
        {
            return startupStructure.genreResearch[componentID];
        }
        else if (componentID[0] == '2')
        {
            return startupStructure.themeResearch[componentID];
        }
        else if (componentID[0] == '3')
        {
            return startupStructure.cameraResearch[componentID];
        }
        else if (componentID[0] == '4')
        {
            return startupStructure.graphicResearch[componentID];
        }
        else if (componentID[0] == '5')
        {
            return startupStructure.featureResearch[componentID];
        }
        else if (componentID[0] == '6')
        {
            return startupStructure.platformResearch[componentID];
        }
        else
        {
            print("GetResearchComponentData: " + componentID + " is Null");
            return null;
        }
    }

    public int GetProductCount()
    {
        return startupStructure.products.Count;
    }
}
