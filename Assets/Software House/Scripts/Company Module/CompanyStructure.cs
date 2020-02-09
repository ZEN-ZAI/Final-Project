using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MaxComponent
{
    public int maxGenre = 1;
    public int maxTheme = 1;
    public int maxGraphic = 1;
    public int maxCamera = 1;
    public int maxFeature = 3;
    public int maxPlatform = 1;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["maxGenre"] = maxGenre;
        result["maxTheme"] = maxTheme;
        result["maxGraphic"] = maxGraphic;
        result["maxCamera"] = maxCamera;
        result["maxFeature"] = maxFeature;
        result["maxPlatform"] = maxPlatform;

        return result;
    }
}

[Serializable]
public class MaxSlot
{
    public int maxOutsourceMember = 2;
    public int maxProductMember = 2;
    public int maxResearchMember = 2;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["maxOutsourceMember"] = maxOutsourceMember;
        result["maxProductMember"] = maxProductMember;
        result["maxResearchMember"] = maxResearchMember;

        return result;
    }
}

[Serializable]
public class MaxWork
{
    public int maxProduct = 3;
    public int maxContract = 3;

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["maxProduct"] = maxProduct;
        result["maxContract"] = maxContract;

        return result;
    }
}
namespace Data
{
    [Serializable]
    public class CompanyStructure
    {
        public string name;
        public int level;
        public int exp;
        public string tier;
        public int popular;
        public long money;
        public long asset;
        public int point;
        public int maxEmployee = 2;

        public MaxComponent maxComponent;
        public MaxSlot maxSlot;
        public MaxWork maxWork;

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result["name"] = name;
            result["level"] = level;
            result["exp"] = exp;
            result["tier"] = tier;
            result["popular"] = popular;
            result["money"] = money;
            result["asset"] = asset;
            result["point"] = point;
            result["maxEmployee"] = maxEmployee;

            result["maxComponent"] = maxComponent.ToDictionary();
            result["maxSlot"] = maxSlot.ToDictionary();
            result["maxWork"] = maxWork.ToDictionary();

            return result;
        }

        public int GetCompanyMaxExp()
        {
            return level * 100;
        }
    }
}
public class CompanyStructure : MonoBehaviour
{
    #region Singleton
    public static CompanyStructure instance;

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

    [SerializeField] private Data.CompanyStructure companyStructure;

    public bool set;

    public string GetJson()
    {
        return JsonUtility.ToJson(companyStructure);
    }

    public void Set(string json)
    {
        companyStructure = JsonUtility.FromJson<Data.CompanyStructure>(json);
        set = true;
    }

    public int GetMaxEmployee()
    {
        return companyStructure.maxEmployee;
    }

    public MaxWork GetMaxWork()
    {
        return companyStructure.maxWork;
    }

    public MaxComponent GetMaxComponent()
    {
        return companyStructure.maxComponent;
    }

    public MaxSlot GetMaxSlot()
    {
        return companyStructure.maxSlot;
    }

    public void RemoveCompanyMoney(int money)
    {
        companyStructure.money -= money;
    }

    public void RemoveCompanyLevel(int level)
    {
        companyStructure.level -= level;
    }

    public void RemoveCompanyPopular(int popular)
    {
        companyStructure.popular -= popular;
    }

    public void AddCompanyMoney(int money)
    {
        companyStructure.money += money;
    }

    public void AddCompanyExp(int exp)
    {
        companyStructure.exp += exp;

        if (companyStructure.exp < 0)
        {
            companyStructure.exp = 0;
        }
    }

    public void AddCompanyPoint(int point)
    {
        companyStructure.point += point;
    }

    public void AddCompanyPopular(int Popular)
    {
        companyStructure.popular += Popular;
    }

    public string GetCompanyName()
    {
        return companyStructure.name;
    }

    public int GetCompanyExp()
    {
        return companyStructure.exp;
    }

    public int GetCompanyMaxExp()
    {
        return companyStructure.GetCompanyMaxExp();
    }

    public void SetCompanyName(string companyName)
    {
        companyStructure.name = companyName;
    }

    public Data.CompanyStructure GetCompanyStructure()
    {
        return companyStructure;
    }

}