using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform groundRoot;
    public Transform wallRoot;
    public Transform furnitureRoot;
    public Transform relaxRoot;
    public Transform workRoot;

    private MapBuilder mapBuilder;
    private MapEditor mapEditor;
    private JsonLoader jsonLoader;

    private void Start()
    {
        mapBuilder = GetComponent<MapBuilder>();
        mapEditor = GetComponent<MapEditor>();
        jsonLoader = new JsonLoader();

        mapBuilder.SetRoot();
    }

    public void LoadMapStarter()
    {
        MapStructure.instance.Set(jsonLoader.LoadMapJson("Starter"));
        Build();
    }

    public void MapEditorMoveMode()
    {
        mapEditor.MoveMode();
    }

    public void MapEditorRotateMode()
    {
        mapEditor.RotateMode();
    }

    public void MapEditorDeleteMode()
    {
        mapEditor.DeleteMode();
    }

    public void MapEditorSetActive(bool state)
    {
        mapEditor.SetActive(state);
    }

    public void Build()
    {
        mapBuilder.BuildMap(MapStructure.instance.GetGroundLayer());
        mapBuilder.BuildMap(MapStructure.instance.GetFurnitureLayer());
        mapBuilder.BuildMap(MapStructure.instance.GetWallLayer());
        mapBuilder.BuildMap(MapStructure.instance.GetRelaxLayer());
        mapBuilder.BuildMap(MapStructure.instance.GetWorkLayer());

        Invoke("Placment", 0.5f);
    }

    public void Placment()
    {
        foreach (ConstructControl item in mapBuilder.construct_list)
        {
            item.FindGround();
        }

        foreach (ConstructControl item in mapBuilder.construct_list)
        {
            item.Placement();
        }
    }

    public void RemoveGround()
    {
        for (int i = 0; i < groundRoot.transform.childCount; i++)
        {
            Destroy(groundRoot.GetChild(i).gameObject);
        }
    }

    public void RemoveMap()
    {
        for (int i = 0; i < groundRoot.transform.childCount; i++)
        {
            Destroy(groundRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < wallRoot.transform.childCount; i++)
        {
            Destroy(wallRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < furnitureRoot.transform.childCount; i++)
        {
            Destroy(furnitureRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < relaxRoot.transform.childCount; i++)
        {
            Destroy(relaxRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < workRoot.transform.childCount; i++)
        {
            Destroy(workRoot.GetChild(i).gameObject);
        }

        mapBuilder.construct_list.Clear();
    }

    public void GenerateGround(int row, int column)
    {
        mapBuilder.GenerateGround(row, column);

        Invoke("Placment", 0.5f);
    }

    private GameObject construct_temp;

    public void Test_BuyConstruct(Construct construct)
    {
        mapEditor.CancelPlacement();
        if (construct.constructType == ConstructType.Ground)
        {
            construct_temp = mapBuilder.GenerateConstruct(construct, new Vector3(0, -10, 0), new ConstructData(), groundRoot);
        }
        else if (construct.constructType == ConstructType.Wall)
        {
            construct_temp = mapBuilder.GenerateConstruct(construct, new Vector3(0, 20, 0), new ConstructData(), wallRoot);
        }
        else if (construct.constructType == ConstructType.Furniture)
        {
            construct_temp = mapBuilder.GenerateConstruct(construct, new Vector3(0, 20, 0), new ConstructData(), furnitureRoot);
        }
        else if (construct.constructType == ConstructType.Relax)
        {
            construct_temp = mapBuilder.GenerateConstruct(construct, new Vector3(0, 20, 0), new ConstructData(), relaxRoot);
        }
        else if (construct.constructType == ConstructType.Work)
        {
            construct_temp = mapBuilder.GenerateConstruct(construct, new Vector3(0, 20, 0), new ConstructData(), workRoot);
        }

        if (construct_temp == null)
        {
            return;
        }

        SetSelectConstruct(construct_temp);
    }

    public void SetSelectConstruct(GameObject construct)
    {
        mapEditor.SetSelectConstruct(construct);
    }

    public GameObject GetSelectConstruct()
    {
        return mapEditor.GetSelectConstruct();
    }

    public void UpdateMapLayer()
    {
        List<ConstructData> ground_layer = new List<ConstructData>();
        List<ConstructData> wall_layer = new List<ConstructData>();
        List<ConstructData> furniture_layer = new List<ConstructData>();
        List<ConstructData> relax_layer = new List<ConstructData>();
        List<ConstructData> work_layer = new List<ConstructData>();

        for (int i = 0; i < groundRoot.childCount; i++)
        {
            ground_layer.Add(groundRoot.GetChild(i).GetComponent<ConstructSlot>().constructData);
        }

        for (int i = 0; i < wallRoot.childCount; i++)
        {
            wall_layer.Add(wallRoot.GetChild(i).GetComponent<ConstructSlot>().constructData);
        }

        for (int i = 0; i < furnitureRoot.childCount; i++)
        {
            furniture_layer.Add(furnitureRoot.GetChild(i).GetComponent<ConstructSlot>().constructData);
        }

        for (int i = 0; i < relaxRoot.childCount; i++)
        {
            relax_layer.Add(relaxRoot.GetChild(i).GetComponent<ConstructSlot>().constructData);
        }

        for (int i = 0; i < workRoot.childCount; i++)
        {
            work_layer.Add(workRoot.GetChild(i).GetComponent<ConstructSlot>().constructData);
        }

        MapStructure.instance.SetGroundLayer(ground_layer);
        MapStructure.instance.SetWallLayer(wall_layer);
        MapStructure.instance.SetFurnitureLayer(furniture_layer);
        MapStructure.instance.SetRelaxLayer(relax_layer);
        MapStructure.instance.SetWorkLayer(work_layer);
    }

    public Transform FindNearRelaxObject(Transform transform)
    {
        Dictionary<float, Transform> relaxObject_Dic = new Dictionary<float, Transform>();

        for (int i = 0; i < relaxRoot.childCount; i++)
        {
            ConstructSlot constructSlot = relaxRoot.GetChild(i).GetComponent<ConstructSlot>();
            Transform transformConstruct = relaxRoot.GetChild(i);

            if (constructSlot.construct.constructType == ConstructType.Relax)
            {
                float distance = Vector3.Distance(transformConstruct.position, transform.position);

                if (!relaxObject_Dic.ContainsKey(distance))
                {
                    relaxObject_Dic.Add(distance, transformConstruct);
                }
            }
        }

        if (relaxObject_Dic.Count > 0)
        {
            float keyR = relaxObject_Dic.Min(e => e.Key);
            return relaxObject_Dic[keyR];
        }
        else
        {
            return transform;
        }
    }

    public Transform FindNearWorkObject(Transform transform)
    {
        Dictionary<float, Transform> workObject_Dic = new Dictionary<float, Transform>();

        for (int i = 0; i < workRoot.childCount; i++)
        {
            ConstructSlot constructSlot = workRoot.GetChild(i).GetComponent<ConstructSlot>();
            Transform transformConstruct = workRoot.GetChild(i);

            if (constructSlot.construct.constructType == ConstructType.Work)
            {
                float distance = Vector3.Distance(transformConstruct.position, transform.position);

                if (!workObject_Dic.ContainsKey(distance))
                {
                    workObject_Dic.Add(distance, transformConstruct);
                }
            }
        }

        if (workObject_Dic.Count > 0)
        {
            float keyR = workObject_Dic.Min(e => e.Key);
            return workObject_Dic[keyR];
        }
        else
        {
            return transform;
        }
    }

    public Transform RandomRelaxObject(Transform transform)
    {
        List<Transform> relaxObject_List = new List<Transform>();

        for (int i = 0; i < relaxRoot.childCount; i++)
        {
            ConstructSlot constructSlot = relaxRoot.GetChild(i).GetComponent<ConstructSlot>();
            Transform transformConstruct = relaxRoot.GetChild(i);

            if (constructSlot.construct.constructType == ConstructType.Relax)
            {
                relaxObject_List.Add(transformConstruct);
            }
        }

        if (relaxObject_List.Count > 0)
        {
            int rnd = UnityEngine.Random.Range(0, relaxObject_List.Count - 1);
            return relaxObject_List[rnd];
        }
        else
        {
            return transform;
        }
    }

    public Transform RandomWorkObject(Transform transform)
    {
        List<Transform> workObject_List = new List<Transform>();

        for (int i = 0; i < workRoot.childCount; i++)
        {
            ConstructSlot constructSlot = workRoot.GetChild(i).GetComponent<ConstructSlot>();
            Transform transformConstruct = workRoot.GetChild(i);

            if (constructSlot.construct.constructType == ConstructType.Work)
            {
                workObject_List.Add(transformConstruct);
            }
        }

        if (workObject_List.Count > 0)
        {
            int rnd = UnityEngine.Random.Range(0, workObject_List.Count - 1);
            return workObject_List[rnd];
        }
        else
        {
            return transform;
        }
    }

    public GameObject GetConstruct(string constructID)
    {
        if (constructID.Substring(0) == "2")
        {
            return wallRoot.Find(constructID).gameObject;
        }
        else if (constructID.Substring(0) == "3")
        {
            return furnitureRoot.Find(constructID).gameObject;
        }
        else if (constructID.Substring(0) == "4")
        {
            return relaxRoot.Find(constructID).gameObject;
        }
        else if (constructID.Substring(0) == "5")
        {
            return workRoot.Find(constructID).gameObject;
        }
        else
        {
            return null;
        }
    }
}
