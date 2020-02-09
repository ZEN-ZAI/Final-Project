using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WorkManager : MonoBehaviour
{
    #region Singleton
    public static WorkManager instance;
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

    public void RandomOutsourceScenarioType(int count)
    {
        print("Random Outsource ScenarioType: " + count);

        WorkStructure.instance.ClearContractScenarioType();

        for (int i = 0; i < count; i++)
        {
            ContractData ContractData = GenerateContract(ContractType.Scenario);
            WorkStructure.instance.AddContractScenarioType(ContractData);
        }
    }

    public void RandomOutsourceGraphicType(int count)
    {
        print("Random Outsource GraphicType: " + count);

        WorkStructure.instance.ClearContractGraphicType();

        for (int i = 0; i < count; i++)
        {
            ContractData ContractData = GenerateContract(ContractType.Graphic);
            WorkStructure.instance.AddContractGraphicType(ContractData);
        }
    }

    public void RandomOutsourceModuleType(int count)
    {
        print("Random Outsource ModuleType: " + count);

        WorkStructure.instance.ClearContractModuleType();

        for (int i = 0; i < count; i++)
        {
            ContractData ContractData = GenerateContract(ContractType.Module);
            WorkStructure.instance.AddContractModuleType(ContractData);
        }
    }

    public void RandomOutsourceSupportType(int count)
    {
        print("Random Outsource SupportType: " + count);

        WorkStructure.instance.ClearContractSupportType();

        for (int i = 0; i < count; i++)
        {
            ContractData ContractData = GenerateContract(ContractType.Support);
            WorkStructure.instance.AddContractSupportType(ContractData);
        }
    }

    public void RandomOutsourceFullGameProjectType(int count)
    {
        print("Random Outsource FullGameProjectType: " + count);

        WorkStructure.instance.ClearContractFullGameProjectType();

        for (int i = 0; i < count; i++)
        {
            ContractData ContractData = GenerateContract(ContractType.FullGameProject);
            WorkStructure.instance.AddContractFullGameProjectType(ContractData);
        }
    }

    public void AddOutSourceWork(ContractData ContractData)
    {
        Data.GameTimeStructure temp = GameTimeStructure.instance.GetGameTimeStructure();
        string timeStamp = temp.timeSecond.ToString("#.#").Replace(".","") + "" + temp.timeMinute + "" + temp.week + "" + temp.month;

        ContractData.workID = ContractData.workID + "-" + timeStamp;
        WorkStructure.instance.AddOutSourceWork(ContractData);
    }

    public void RemoveOutSourceWork(ContractData ContractData)
    {
        WorkStructure.instance.RemoveOutSourceWork(ContractData);
    }

    public bool ContainsOutSourceWork(string workID)
    {
       return  WorkStructure.instance.ContainsOutsourceWork(workID);
    }

    public ContractData GetOutSourceWork(string workID)
    {
        return WorkStructure.instance.GetOutSourceWork(workID);
    }

    public void CheckOutsourceDueDate()
    {
        foreach (ContractData ContractData in WorkStructure.instance.GetOutSourceWorkList())
        {
            print("CheckOutsourceDueDate -> " + ContractData.workID);

            if (ContractData.dueDate.week == GameTimeStructure.instance.GetGameTimeStructure().week
                && ContractData.dueDate.month == GameTimeStructure.instance.GetGameTimeStructure().month
                && ContractData.dueDate.year == GameTimeStructure.instance.GetGameTimeStructure().year)
            {
                print("DueDate -> " + ContractData.workID);
                if (ContractData.isDone)
                {
                    ContractData.ClaimReward();
                }
                else
                {
                    ContractData.Fine();
                }
            }
        }
    }

    public void CheckProductTimeStamp()
    {
        foreach (Product product in StartupStructure.instance.GetProductList())
        {
            if (product.timeStamp.week == GameTimeStructure.instance.GetGameTimeStructure().week
                && product.timeStamp.month == GameTimeStructure.instance.GetGameTimeStructure().month
                && product.timeStamp.year == GameTimeStructure.instance.GetGameTimeStructure().year)
            {
                CalcurateSelling(product);
            }
        }
    }

    public void CalcurateSelling(Product product)
    {
        SaleReport saleReport = new SaleReport();
        int unitSelling = UnityEngine.Random.Range(10, 100000);
        int price = product.price;
        int cost = product.cost;
        int income = product.price * unitSelling;
        int profit = cost - income;
        int fan = (unitSelling / 100) * UnityEngine.Random.Range(1, 25);

        saleReport.Set(GameTimeStructure.instance.GetGameTimeStructure(), unitSelling, price, cost, income, profit, cost);
        product.timeStamp = GameTimeStructure.instance.GetFutureTime(1);
        product.AddSaleReport(saleReport);

        
    }

    public void Processing(string workID)//componentID
    {
        string[] words = workID.Split(':');

        print("Processing " + workID);

        if (words[0] == "OutSource" && GetOutSourceWork(words[1]) != null)
        {
            print("Outsource");
            ContractData ContractData = GetOutSourceWork(words[1]);

            ContractData.Process();
            ContractData.UpdateProcessCurrent();
        }
        else if (words[0] == "Research" && StartupStructure.instance.GetResearchComponentData(words[1]) != null)
        {
            print("Research");
            ComponentData componentData = StartupStructure.instance.GetResearchComponentData(words[1]);

            componentData.ProcessResearch();
        }
        else if (words[0] == "Product" && StartupStructure.instance.GetProduct(words[1]) != null)
        {
            print("Product");
            Product product =  StartupStructure.instance.GetProduct(words[1]);

            product.Process();
            product.UpdateProcessCurrent();
        }
        else
        {
            print("Processing Not found: " + workID);
        }
    }

    public bool WorkIsDone(string workID)
    {
        string[] words = workID.Split(':');

        //words[0] type
        //words[1] component or oursourceId
        //words[1] component


        if (words[0] == "OutSource" && GetOutSourceWork(words[1]) != null)
        {
            ContractData ContractData = GetOutSourceWork(words[1]);
            if (ContractData.processCurrent == ContractData.processMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (words[0] == "Research" && StartupStructure.instance.GetResearchComponentData(words[1]) != null)
        {
            ComponentData componentData = StartupStructure.instance.GetResearchComponentData(words[1]);

            if (componentData.researchLevel == 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (words[0] == "Product" && StartupStructure.instance.GetProduct(words[1]) != null)
        {
            Product product = StartupStructure.instance.GetProduct(words[1]);

            if (product.processCurrent == product.processMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            print("WorkIsDone Not found: " + workID);
            return false;
        }
    }

    public ContractData GenerateContract(ContractType contractType)
    {
        ContractData contractData = new ContractData();

        contractData.contractType = contractType;
        contractData.scaleType = (ScaleType)UnityEngine.Random.Range(1, Enum.GetValues(typeof(ScaleType)).Length);


        int maxComponentRequirements = 0;

        Data.GameTimeStructure dueDate = new Data.GameTimeStructure();

        if (contractData.scaleType == ScaleType.Small)
        {
            maxComponentRequirements = UnityEngine.Random.Range(2, 3 + 1);
            dueDate = GameTimeStructure.instance.GetFutureTime(UnityEngine.Random.Range(2, 3));
        }
        else if (contractData.scaleType == ScaleType.Medium)
        {
            maxComponentRequirements = UnityEngine.Random.Range(4, 9 + 1);
            dueDate = GameTimeStructure.instance.GetFutureTime(UnityEngine.Random.Range(2, 4));
        }
        else if (contractData.scaleType == ScaleType.Large)
        {
            maxComponentRequirements = UnityEngine.Random.Range(10, 15 + 1);
            dueDate = GameTimeStructure.instance.GetFutureTime( UnityEngine.Random.Range(4, 12));
        }

        MaxComponent maxComponent = CompanyStructure.instance.GetMaxComponent();

        if (contractType == ContractType.Scenario)
        {
            contractData.genreDatas = ComponentAsset.instance.RandomComponentGenre(maxComponentRequirements, maxComponent.maxGenre);
            contractData.cameraDatas = ComponentAsset.instance.RandomComponentCamera(maxComponentRequirements, maxComponent.maxCamera);
        }
        else if (contractType == ContractType.Graphic)
        {
            contractData.themeDatas = ComponentAsset.instance.RandomComponentTheme(maxComponentRequirements, maxComponent.maxTheme);
            contractData.graphicDatas = ComponentAsset.instance.RandomComponentGraphic(maxComponentRequirements, maxComponent.maxGraphic);
        }
        else if (contractType == ContractType.Module)
        {
            contractData.featureDatas = ComponentAsset.instance.RandomComponentFeature(maxComponentRequirements, maxComponent.maxFeature);
            contractData.platformDatas = ComponentAsset.instance.RandomComponentPlatform(maxComponentRequirements, maxComponent.maxPlatform);
        }
        else if (contractType == ContractType.Support)
        {
            //Wait
        }
        else if (contractType == ContractType.FullGameProject)
        {
            contractData.genreDatas = ComponentAsset.instance.RandomComponentGenre(maxComponentRequirements, maxComponent.maxGenre);
            contractData.cameraDatas = ComponentAsset.instance.RandomComponentCamera(maxComponentRequirements, maxComponent.maxCamera);
            contractData.themeDatas = ComponentAsset.instance.RandomComponentTheme(maxComponentRequirements, maxComponent.maxTheme);
            contractData.graphicDatas = ComponentAsset.instance.RandomComponentGraphic(maxComponentRequirements, maxComponent.maxGraphic);
            contractData.featureDatas = ComponentAsset.instance.RandomComponentFeature(maxComponentRequirements, maxComponent.maxFeature);
            contractData.platformDatas = ComponentAsset.instance.RandomComponentPlatform(maxComponentRequirements, maxComponent.maxPlatform);
        }

        int allLevelComponent_temp = 0;
        string workID = "";

        foreach (ComponentData componentRequirements in contractData.genreDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }
        foreach (ComponentData componentRequirements in contractData.cameraDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }
        foreach (ComponentData componentRequirements in contractData.themeDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }
        foreach (ComponentData componentRequirements in contractData.graphicDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }
        foreach (ComponentData componentRequirements in contractData.featureDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }
        foreach (ComponentData componentRequirements in contractData.platformDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
            workID += (componentRequirements.componentID[componentRequirements.componentID.Length - 1] * componentRequirements.productLevel).ToString();
        }

        contractData.processMax = allLevelComponent_temp;

        contractData.workID = (int)contractData.contractType + (int)contractData.scaleType + maxComponentRequirements + workID + "";

        Reward reward = new Reward();
        reward.exp = (10 * allLevelComponent_temp);
        reward.money = allLevelComponent_temp *5000;


        Fine fine = new Fine();
        fine.money = (allLevelComponent_temp * 5000) / 2;

        contractData.reward = reward;
        contractData.fine = fine;
        contractData.dueDate = dueDate;

        return contractData;
    }
}
