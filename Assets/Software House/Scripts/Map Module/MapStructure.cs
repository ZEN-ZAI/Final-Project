using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class MapStructure
    {
        public List<ConstructData> ground_layer = new List<ConstructData>();

        public Dictionary<string, ConstructData> wall_layer = new Dictionary<string, ConstructData>();
        public Dictionary<string, ConstructData> furniture_layer = new Dictionary<string, ConstructData>();
        public Dictionary<string, ConstructData> relax_layer = new Dictionary<string, ConstructData>();
        public Dictionary<string, ConstructData> work_layer = new Dictionary<string, ConstructData>();

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Dictionary<string, object> result_ground_layer = new Dictionary<string, object>();
            for (int i = 0; i < ground_layer.Count; i++)
            {
                result_ground_layer.Add(i + "", ground_layer[i].ToDictionary());
            }
            result["ground_layer"] = result_ground_layer;


            Dictionary<string, object> result_wall_layer = new Dictionary<string, object>();
            foreach (var item in wall_layer)
            {
                result_wall_layer.Add(item.Key, item.Value.ToDictionary());
            }
            result["wall_layer"] = result_wall_layer;


            Dictionary<string, object> result_furniture_layer = new Dictionary<string, object>();
            foreach (var item in furniture_layer)
            {
                result_furniture_layer.Add(item.Key, item.Value.ToDictionary());
            }
            result["furniture_layer"] = result_furniture_layer;


            Dictionary<string, object> result_relax_layer = new Dictionary<string, object>();
            foreach (var item in relax_layer)
            {
                result_relax_layer.Add(item.Key, item.Value.ToDictionary());
            }
            result["relax_layer"] = result_relax_layer;


            Dictionary<string, object> result_work_layer = new Dictionary<string, object>();
            foreach (var item in work_layer)
            {
                result_work_layer.Add(item.Key, item.Value.ToDictionary());
            }

            result["work_layer"] = result_work_layer;

            return result;
        }
    }
}

public class MapStructure : MonoBehaviour
{
    #region Singleton
    public static MapStructure instance;

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

    [SerializeField] private Data.MapStructure mapStructure;
    public bool set;

    public void Set(string json)
    {
        if (json == null)
        {
            mapStructure = new Data.MapStructure();
        }
        else
        {
            mapStructure = JsonConvert.DeserializeObject<Data.MapStructure>(json);
        }

        set = true;
    }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(mapStructure);
    }

    public void SetGroundLayer(List<ConstructData> ground_layer)
    {
        mapStructure.ground_layer = ground_layer;
    }

    public void SetWallLayer(List<ConstructData> wall_layer)
    {
        mapStructure.wall_layer.Clear();
        foreach (var item in wall_layer)
        {
            mapStructure.wall_layer.Add(item.constructID + "" + item.x + "" + item.z, item);
        }
    }

    public void SetFurnitureLayer(List<ConstructData> furniture_layer)
    {
        mapStructure.furniture_layer.Clear();
        foreach (var item in furniture_layer)
        {
            mapStructure.furniture_layer.Add(item.constructID + "" + item.x + "" + item.z, item);
        }
    }

    public void SetRelaxLayer(List<ConstructData> relax_layer)
    {
        mapStructure.relax_layer.Clear();
        foreach (var item in relax_layer)
        {
            mapStructure.relax_layer.Add(item.constructID + "" + item.x + "" + item.z, item);
        }
    }

    public void SetWorkLayer(List<ConstructData> work_layer)
    {
        mapStructure.work_layer.Clear();
        foreach (var item in work_layer)
        {
            mapStructure.work_layer.Add(item.constructID + "" + item.x + "" + item.z, item);
        }
    }

    public List<ConstructData> GetGroundLayer()
    {
        return mapStructure.ground_layer;
    }

    public List<ConstructData> GetFurnitureLayer()
    {
        List<ConstructData> temp = new List<ConstructData>();
        foreach (var item in mapStructure.furniture_layer)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public List<ConstructData> GetWallLayer()
    {
        List<ConstructData> temp = new List<ConstructData>();
        foreach (var item in mapStructure.wall_layer)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public List<ConstructData> GetRelaxLayer()
    {
        List<ConstructData> temp = new List<ConstructData>();
        foreach (var item in mapStructure.relax_layer)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public List<ConstructData> GetWorkLayer()
    {
        List<ConstructData> temp = new List<ConstructData>();
        foreach (var item in mapStructure.work_layer)
        {
            temp.Add(item.Value);
        }

        return temp;
    }

    public Data.MapStructure GetMapStructure()
    {
        return mapStructure;
    }
}