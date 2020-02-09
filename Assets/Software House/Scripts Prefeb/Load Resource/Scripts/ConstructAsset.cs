using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConstructAsset : MonoBehaviour
{
    #region Singleton
    public static ConstructAsset instance;
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
        groundAsset = Resources.LoadAll<Construct>("Construct/Ground").ToList();
        wallAsset = Resources.LoadAll<Construct>("Construct/Wall").ToList();
        furnitureAsset = Resources.LoadAll<Construct>("Construct/Furniture").ToList();
        relaxAsset = Resources.LoadAll<Construct>("Construct/Relax").ToList();
        workAsset = Resources.LoadAll<Construct>("Construct/Work").ToList();

        CombineAsset();
    }

    public List<Construct> groundAsset;
    public List<Construct> wallAsset;
    public List<Construct> furnitureAsset;
    public List<Construct> relaxAsset;
    public List<Construct> workAsset;

    public List<Construct> constructAsset;

    private void CombineAsset()
    {
        foreach (Construct construct in groundAsset)
        {
            constructAsset.Add(construct);
        }

        foreach (Construct construct in wallAsset)
        {
            constructAsset.Add(construct);
        }

        foreach (Construct construct in furnitureAsset)
        {
            constructAsset.Add(construct);
        }

        foreach (Construct construct in relaxAsset)
        {
            constructAsset.Add(construct);
        }

        foreach (Construct construct in workAsset)
        {
            constructAsset.Add(construct);
        }
    }

    public List<Construct> GetConstruct()
    {
        return constructAsset;
    }

    public List<Construct> GetGroundAsset()
    {
        return groundAsset;
    }

    public List<Construct> GetWallAsset()
    {
        return wallAsset;
    }

    public List<Construct> GetFurnitureAsset()
    {
        return furnitureAsset;
    }

    public List<Construct> GetRelaxAsset()
    {
        return relaxAsset;
    }

    public List<Construct> GetWorkAsset()
    {
        return workAsset;
    }

}
