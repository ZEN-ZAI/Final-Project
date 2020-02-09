using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Product
{
    public string name;
    public int rank;
    public int bug;
    public int hype;
    public int fan;
    public int cost;
    public int price;
    public int income;
    public int profit;

    public CategorieType categorie;

    public List<ComponentData> genreDatas = new List<ComponentData>(1);
    public List<ComponentData> themeDatas = new List<ComponentData>();
    public List<ComponentData> cameraDatas = new List<ComponentData>();
    public List<ComponentData> graphicDatas = new List<ComponentData>();
    public List<ComponentData> platformDatas = new List<ComponentData>();
    public List<ComponentData> featureDatas = new List<ComponentData>();

    public BussinessModelType bussinessModel;

    public int processCurrent;
    public int processMax;

    public Data.GameTimeStructure release;
    public Data.GameTimeStructure timeStamp;

    public List<string> employee_Worker = new List<string>();


    public SaleReport totalSaleReport = new SaleReport();
    public List<SaleReport> saleReports = new List<SaleReport>();

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["name"] = name;
        result["rank"] = rank;
        result["bug"] = bug;
        result["hype"] = hype;
        result["fan"] = fan;
        result["cost"] = cost;
        result["price"] = price;
        result["income"] = income;
        result["profit"] = profit;

        result["categorie"] = categorie.GetHashCode();

        Dictionary<string, object> result_employee_Worker = new Dictionary<string, object>();

        for (int i = 0; i < employee_Worker.Count; i++)
        {
            result_employee_Worker.Add(i + "", employee_Worker[i]);
        }
        result["employee_Worker"] = result_employee_Worker;

        Dictionary<string, object> result_genreDatas = new Dictionary<string, object>();
        for (int i = 0; i < genreDatas.Count; i++)
        {
            result_genreDatas.Add(i+"", genreDatas[i].ToDictionary());
        }

        Dictionary<string, object> result_themeDatas = new Dictionary<string, object>();
        for (int i = 0; i < themeDatas.Count; i++)
        {
            result_themeDatas.Add(i + "", themeDatas[i].ToDictionary());
        }

        Dictionary<string, object> result_cameraDatas = new Dictionary<string, object>();
        for (int i = 0; i < cameraDatas.Count; i++)
        {
            result_cameraDatas.Add(i + "", cameraDatas[i].ToDictionary());
        }

        Dictionary<string, object> result_graphicDatas = new Dictionary<string, object>();
        for (int i = 0; i < graphicDatas.Count; i++)
        {
            result_graphicDatas.Add(i + "", graphicDatas[i].ToDictionary());
        }

        Dictionary<string, object> result_platformDatas = new Dictionary<string, object>();
        for (int i = 0; i < platformDatas.Count; i++)
        {
            result_platformDatas.Add(i + "", platformDatas[i].ToDictionary());
        }

        Dictionary<string, object> result_featureDatas = new Dictionary<string, object>();
        for (int i = 0; i < featureDatas.Count; i++)
        {
            result_featureDatas.Add(i + "", featureDatas[i].ToDictionary());
        }

        result["genreDatas"] = result_genreDatas;
        result["themeDatas"] = result_themeDatas;
        result["cameraDatas"] = result_cameraDatas;
        result["graphicDatas"] = result_graphicDatas;
        result["platformDatas"] = result_platformDatas;
        result["featureDatas"] = result_featureDatas;

        result["bussinessModel"] = bussinessModel.GetHashCode();

        result["release"] = release.ToDictionary();
        result["timeStamp"] = timeStamp.ToDictionary();

        result["totalSaleReport"] = totalSaleReport.ToDictionary();

        Dictionary<string, object> result_saleReports = new Dictionary<string, object>();
        for (int i = 0; i < saleReports.Count; i++)
        {
            result_saleReports.Add(i+"", saleReports[i].ToDictionary());
        }

        result["saleReports"] = result_saleReports;

        result["processCurrent"] = processCurrent;
        result["processMax"] = processMax;

        return result;
    }

    public void AddMember(string employeeID)
    {
        employee_Worker.Add(employeeID);
    }

    public void RemoveMember(string employeeID)
    {
        employee_Worker.Remove(employeeID);
    }

    public void RemoveAllMember()
    {
        employee_Worker.Clear();
    }

    public bool isDone
    {
        get
        {
            if (release.week !=0 && release.month != 0 && release.year != 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void Process()
    {
        foreach (string employeeID in employee_Worker)
        {
            if (ProcessComponent(employeeID,genreDatas))
            {
                continue;
            }
            else if(ProcessComponent(employeeID, themeDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, cameraDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, graphicDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, platformDatas))
            {
                continue;
            }
            else if (ProcessComponent(employeeID, featureDatas))
            {
                continue;
            }
        }
    }

    private bool ProcessComponent(string employeeID, List<ComponentData> componentDatas)
    {
        bool update = false; ;
        foreach (ComponentData componentData in componentDatas)
        {
            int power = EmployeeManager.instance.GetPowerByEmployeeID(employeeID, componentData);

            if (power != 0 && componentData.developLevel < componentData.productLevel)
            {
                componentData.UpdateDevelopLevel(power);
                update = true;
                break;
            }
        }

        return update;
    }

    public void UpdateProcessCurrent()
    {
        int temp_processCurrent = 0;

        foreach (ComponentData componentData in genreDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in themeDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in cameraDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in graphicDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in platformDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        foreach (ComponentData componentData in featureDatas)
        {
            temp_processCurrent += componentData.developLevel;
        }

        processCurrent = temp_processCurrent;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetProcessMax(int process)
    {
        this.processMax = process;
    }

    public void SetCategorie(CategorieType categorie)
    {
        this.categorie = categorie;
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
    }

    public Data.GameTimeStructure GetTimeStamp()
    {
        return timeStamp;
    }

    public void SetTimeStamp(int week, int month, int year)
    {
        timeStamp.week = week;
        timeStamp.month = month;
        timeStamp.year = year;
    }

    public void SetRelease(int week,int month, int year)
    {
        release.week = week;
        release.month = month;
        release.year = year;
    }

    public void SetBussinessModelType(BussinessModelType bussinessModelType)
    {
        this.bussinessModel = bussinessModelType;
    }

    public bool CanAddComponent(Component component)
    {
        int index = -1;

        if (component.componentType == ComponentType.Feature)
        {
            index = platformDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (component.componentType == ComponentType.Platform)
        {
            index = featureDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (component.componentType == ComponentType.Genre)
        {
            index = genreDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (component.componentType == ComponentType.Camera)
        {
            index = genreDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (component.componentType == ComponentType.Graphic)
        {
            index = genreDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (component.componentType == ComponentType.Theme)
        {
            index = genreDatas.FindIndex(e => e.componentID == component.componentID);

            if (index >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false; 
    }

    public List<ComponentData> GetPlatformDataList()
    {
        return platformDatas;
    }

    public List<ComponentData> GetFeatureDataList()
    {
        return featureDatas;
    }

    public List<ComponentData> GetGenreDataList()
    {
        return genreDatas;
    }

    public List<ComponentData> GetThemeDataList()
    {
        return themeDatas;
    }

    public List<ComponentData> GetGraphicDataList()
    {
        return graphicDatas;
    }

    public List<ComponentData> GetCameraDataList()
    {
        return cameraDatas;
    }

    public List<ComponentData> GetAllComponentDataList()
    {
        List<ComponentData> temp = new List<ComponentData>();

        foreach (var item in GetPlatformDataList())
        {
            temp.Add(item);
        }

        foreach (var item in GetFeatureDataList())
        {
            temp.Add(item);
        }

        foreach (var item in GetGenreDataList())
        {
            temp.Add(item);
        }

        foreach (var item in GetThemeDataList())
        {
            temp.Add(item);
        }

        foreach (var item in GetGraphicDataList())
        {
            temp.Add(item);
        }

        foreach (var item in GetCameraDataList())
        {
            temp.Add(item);
        }

        return temp;
    }

    public int GetAllComponentDataLevel()
    {
        int temp = 0;

        foreach (var item in GetPlatformDataList())
        {
            temp += item.productLevel;
        }

        foreach (var item in GetFeatureDataList())
        {
            temp += item.productLevel;
        }

        foreach (var item in GetGenreDataList())
        {
            temp += item.productLevel;
        }

        foreach (var item in GetThemeDataList())
        {
            temp += item.productLevel;
        }

        foreach (var item in GetGraphicDataList())
        {
            temp += item.productLevel;
        }

        foreach (var item in GetCameraDataList())
        {
            temp += item.productLevel;
        }

        return temp;
    }

    public ComponentData FindComponent(string componentID)
    {
        int index1 = -1;
        int index2 = -1;
        int index3 = -1;
        int index4 = -1;
        int index5 = -1;
        int index6 = -1;

        index1 = platformDatas.FindIndex(e => e.componentID == componentID);
        index2 = genreDatas.FindIndex(e => e.componentID == componentID);
        index3 = featureDatas.FindIndex(e => e.componentID == componentID);
        index4 = themeDatas.FindIndex(e => e.componentID == componentID);
        index5 = graphicDatas.FindIndex(e => e.componentID == componentID);
        index6 = cameraDatas.FindIndex(e => e.componentID == componentID);

        if (index1 >= 0)
        {
            return platformDatas[index1];
        }
        else if (index2 >= 0)
        {
            return genreDatas[index2];
        }
        else if (index3 >= 0)
        {
            return featureDatas[index3];
        }
        else if (index4 >= 0)
        {
            return themeDatas[index4];
        }
        else if (index5 >= 0)
        {
            return graphicDatas[index5];
        }
        else if (index6 >= 0)
        {
            return cameraDatas[index6];
        }
        else
        {
            return null;
        }
    }

    public void AddGenreData(ComponentData ComponentData)
    {
        genreDatas.Add(ComponentData);
    }

    public void AddFeatureData(ComponentData ComponentData)
    {
        featureDatas.Add(ComponentData);
    }

    public void AddPlatformData(ComponentData ComponentData)
    {
        platformDatas.Add(ComponentData);
    }

    public void AddThemeData(ComponentData ComponentData)
    {
        themeDatas.Add(ComponentData);
    }

    public void AddGraphicData(ComponentData ComponentData)
    {
        graphicDatas.Add(ComponentData);
    }

    public void AddCameraData(ComponentData ComponentData)
    {
        cameraDatas.Add(ComponentData);
    }


    public void RemoveGenreData(ComponentData ComponentData)
    {
        genreDatas.Remove(ComponentData);
    }

    public void RemoveFeatureData(ComponentData ComponentData)
    {
        featureDatas.Remove(ComponentData);
    }

    public void RemovePlatformData(ComponentData ComponentData)
    {
        platformDatas.Remove(ComponentData);
    }

    public void RemoveThemeData(ComponentData ComponentData)
    {
        themeDatas.Remove(ComponentData);
    }

    public void RemoveGraphicData(ComponentData ComponentData)
    {
        graphicDatas.Remove(ComponentData);
    }

    public void RemoveCameraData(ComponentData ComponentData)
    {
        cameraDatas.Remove(ComponentData);
    }

    //public List<CommentData> GetCommentDataList()
    //{
    //    return commentDatas;
    //}

    public SaleReport GetTotalSaleReport()
    {
        return totalSaleReport;
    }

    public void CalculateTotalSaleReport()
    {
        SaleReport saleReport = new SaleReport();

        foreach (SaleReport item in saleReports)
        {
            saleReport.cost += item.cost;
            saleReport.profit += item.profit;
            saleReport.income += item.income;
            saleReport.unitSelling += item.unitSelling;
            saleReport.fan += item.fan;
        }

        totalSaleReport = saleReport;
    }

    public void AddSaleReport(SaleReport saleReport)
    {
        saleReports.Add(saleReport);
        CalculateTotalSaleReport();
    }

    public void RemoveSaleReport(SaleReport saleReport)
    {
        saleReports.Remove(saleReport);
    }
}