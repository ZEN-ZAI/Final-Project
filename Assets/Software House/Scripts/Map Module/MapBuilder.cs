using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapBuilder : MonoBehaviour
{
    private Transform groundRoot;
    private Transform wallRoot;
    private Transform furnitureRoot;
    public Transform relaxRoot;
    public Transform workRoot;
    public Transform barrierRoot;

    [SerializeField] private Vector3 position;
    public List<ConstructControl> construct_list = new List<ConstructControl>();

    public void SetRoot()
    {
        this.groundRoot = MapManager.instance.groundRoot;
        this.wallRoot = MapManager.instance.wallRoot;
        this.furnitureRoot = MapManager.instance.furnitureRoot;
        this.relaxRoot = MapManager.instance.relaxRoot;
        this.workRoot = MapManager.instance.workRoot;
        this.barrierRoot = MapManager.instance.barrierRoot;
    }

    public void BuildMap(List<ConstructData> ConstructData_Layer)
    {
        List<Construct> constructAsset = ConstructAsset.instance.GetConstruct();

        print("Building Countstrcut: " + ConstructData_Layer.Count);

        foreach (ConstructData constructData in ConstructData_Layer)
        {
            int index = constructAsset.FindIndex(e => e.constructID == constructData.constructID);

            if (constructAsset[index].constructType == ConstructType.Ground)
            {
                GenerateConstruct(constructAsset[index],
                    new Vector3(constructData.x, constructData.y, constructData.z), constructData, groundRoot);

            }
            if (constructAsset[index].constructType == ConstructType.Wall)
            {
                GameObject tempFurniture = GenerateConstruct(constructAsset[index],
                   new Vector3(constructData.x, constructData.y + 0.1f, constructData.z), constructData, wallRoot);

                construct_list.Add(tempFurniture.GetComponent<ConstructControl>());
            }
            else if (constructAsset[index].constructType == ConstructType.Furniture)
            {
                GameObject tempFurniture = GenerateConstruct(constructAsset[index],
                    new Vector3(constructData.x, constructData.y + 0.1f, constructData.z), constructData, furnitureRoot);

                construct_list.Add(tempFurniture.GetComponent<ConstructControl>());
            }
            else if (constructAsset[index].constructType == ConstructType.Relax)
            {
                GameObject tempFurniture = GenerateConstruct(constructAsset[index],
                    new Vector3(constructData.x, constructData.y + 0.1f, constructData.z), constructData, relaxRoot);

                construct_list.Add(tempFurniture.GetComponent<ConstructControl>());
            }
            else if (constructAsset[index].constructType == ConstructType.Work)
            {
                GameObject tempFurniture = GenerateConstruct(constructAsset[index],
                    new Vector3(constructData.x, constructData.y + 0.1f, constructData.z), constructData, workRoot);

                construct_list.Add(tempFurniture.GetComponent<ConstructControl>());
            }
            else if (constructAsset[index].constructType == ConstructType.Barrier)
            {
                GenerateConstruct(ConstructAsset.instance.GetBarrier(),
                    new Vector3(constructData.x, constructData.y + 0.1f, constructData.z), constructData, barrierRoot);
            }
        }
    }

    public GameObject GenerateConstruct(Construct construct, Vector3 position, ConstructData constructData, Transform parent)
    {
        GameObject construct_temp = Instantiate(construct.constructPrefab, parent);
        construct_temp.AddComponent<ConstructSlot>().constructData = constructData;
        construct_temp.GetComponent<ConstructSlot>().construct = construct;

        construct_temp.transform.localEulerAngles = new Vector3(0, constructData.rotation, 0);
        construct_temp.transform.localPosition = position;
        construct_temp.name = construct_temp.name = constructData.constructID +""+ constructData.x +""+ constructData.z;

        return construct_temp;
    }

    public void GenerateGround(int row, int column)
    {
        position = Vector3.zero;
        position.x = -((row * 10) / 2) + 5;
        position.z = -((column * 10) / 2) + 5;

        Construct constructGround = ConstructAsset.instance.groundAsset[0];
        Construct barrier = ConstructAsset.instance.GetBarrier();
        GameObject tempGround = null;

        for (int y = 0; y < column; y++)
        {
            for (int x = 0; x < row; x++)
            {
                if (y == 0|| (x == 0 && y > 0)|| y == column-1 || x == row-1 )
                {
                    tempGround = GenerateConstruct(barrier, position, new ConstructData(), barrierRoot);
                    tempGround.GetComponent<ConstructSlot>().constructData.Set(barrier.constructID, (int)position.x, (int)position.z, 0, 0);

                    position.z += tempGround.transform.localScale.x;
                }
                else
                {
                    tempGround = GenerateConstruct(constructGround, position, new ConstructData(), groundRoot);
                    tempGround.GetComponent<ConstructSlot>().constructData.Set(constructGround.constructID, (int)position.x, (int)position.z, 0, 0);

                    position.z += tempGround.transform.localScale.x;

                }

            }
            position.x += tempGround.transform.localScale.x;
            position.z = -((column * 10) / 2) + 5;
        }
    }
}