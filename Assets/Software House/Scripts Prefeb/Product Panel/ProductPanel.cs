using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ProductPanel : MonoBehaviour
{
    #region Singleton
    public static ProductPanel instance;

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
    }
    #endregion

    public TMP_Text productName;
    public TMP_Text processPercent;
    public Image processBar;

    [Header("Menu")]
    public Button informationButton;
    public Button memberButton;
    public Button marketingButton;
    public Button bussinessModelButton;
    public Button deployButton;
    public Button removeButton;
    //public Button saleReportButton;
    //public Button feedBackButton;

    [Header("Panel")]
    public Transform MenuPanel;
    public Transform informationBuildPanel;
    public Transform informationReleasePanel;
    public Transform workerPanel;
    public Transform marketingPanel;
    public Transform businessPanel;

    [Header("Infomation Build")]
    public TMP_Text bug;
    public TMP_Text hype;
    public TMP_Text fanBuild;
    public TMP_Text costBuild;
    public TMP_Text priceBuild;

    [Header("Infomation Release")]
    public TMP_Text rank;
    public TMP_Text fanRelease;
    public TMP_Text income; 
    public TMP_Text unitSale;
    public TMP_Text price;
    public TMP_Text costRelease;
    public TMP_Text profit;
    public TMP_Text release;

    [Header("Product Component")]
    public Transform componentGenreContent;
    public Transform componentThemeContent;
    public Transform componentGraphicContent;
    public Transform componentCameraContent;
    public Transform componentFeatureContent;
    public Transform componentPlatformContent;

    [Header("Worker")]
    public Transform contentEmployee;
    public GameObject slotEmployee;
    public GameObject selectEmployeeButton;

    [Header("Business Model")]
    public TMP_Text priceDemo;
    public Button digitalDownloadButton;
    public Button freemiumButton;
    public Button subscriptionButton;
    public Button advertisingButton;

    [SerializeField] private Product product = new Product();

    public GameObject slotComponent;

    private void Start()
    {
        CloseAllPanel();

        if (product.isDone)
        {
            informationReleasePanel.gameObject.SetActive(true);
            MenuPanel.gameObject.SetActive(false);
        }
        else
        {
            informationBuildPanel.gameObject.SetActive(true);
            MenuPanel.gameObject.SetActive(true);
        }
        
        workerPanel.gameObject.SetActive(false);
        marketingPanel.gameObject.SetActive(false);
        businessPanel.gameObject.SetActive(false);

        RefreshComponentCardUI();

        informationButton.onClick.AddListener(() =>
        {
            CloseAllPanel();

            if (product.isDone)
            {
                informationReleasePanel.gameObject.SetActive(true);
                MenuPanel.gameObject.SetActive(false);
            }
            else
            {
                informationBuildPanel.gameObject.SetActive(true);
                MenuPanel.gameObject.SetActive(true);
            }
        });

        memberButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            workerPanel.gameObject.SetActive(true);
            RefreshSlotEmployee();
        });

        marketingButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            marketingPanel.gameObject.SetActive(true);
        });

        deployButton.onClick.AddListener(() =>
        {
            Deploy();
        });

        removeButton.onClick.AddListener(() =>
        {
            StartupStructure.instance.RemoveProduct(product.name);
        });


        // Bussiness Model
        bussinessModelButton.onClick.AddListener(() =>
        {
            businessPanel.gameObject.SetActive(true);

            if (product.bussinessModel == BussinessModelType.Digital_Download)
            {
                digitalDownloadButton.GetComponent<Button>().image.color = Color.gray;
            }
            else if (product.bussinessModel == BussinessModelType.Freemium)
            {
                freemiumButton.GetComponent<Button>().image.color = Color.gray;
            }
            else if (product.bussinessModel == BussinessModelType.Subscription)
            {
                subscriptionButton.GetComponent<Button>().image.color = Color.gray;
            }
            else if (product.bussinessModel == BussinessModelType.Advertising)
            {
                advertisingButton.GetComponent<Button>().image.color = Color.gray;
            }
        });

        digitalDownloadButton.onClick.AddListener(() =>
        {
            ClearButtonBusiness();
            digitalDownloadButton.GetComponent<Button>().image.color = Color.gray;
            CalculatePrice(BussinessModelType.Digital_Download);
        });

        freemiumButton.onClick.AddListener(() =>
        {
            ClearButtonBusiness();
            freemiumButton.GetComponent<Button>().image.color = Color.gray;
            CalculatePrice(BussinessModelType.Freemium);
        });

        subscriptionButton.onClick.AddListener(() =>
        {
            ClearButtonBusiness();
            subscriptionButton.GetComponent<Button>().image.color = Color.gray;
            CalculatePrice(BussinessModelType.Subscription);
        });

        advertisingButton.onClick.AddListener(() =>
        {
            ClearButtonBusiness();
            advertisingButton.GetComponent<Button>().image.color = Color.gray;
            CalculatePrice(BussinessModelType.Advertising);
        });
    }

    private void CloseAllPanel()
    {
        informationBuildPanel.gameObject.SetActive(false);
        informationReleasePanel.gameObject.SetActive(false);
        workerPanel.gameObject.SetActive(false);
        marketingPanel.gameObject.SetActive(false);
        businessPanel.gameObject.SetActive(false);
    }

    public void SetProduct(Product product)
    {
        this.product = product;
        productName.text = product.name;

        //infomation Build
        bug.text = "Bug: "+product.bug.ToString();
        hype.text = "Hype: " + product.hype.ToString();
        fanBuild.text = "Fan: " + product.fan.ToString();
        costBuild.text = "Cost: " + product.cost.ToString();
        priceBuild.text = "Price: " + product.price.ToString();

        //Infomation Release
        rank.text = "Rank: " + product.rank.ToString();
        fanRelease.text = "Fan: " + product.fan.ToString();
        income.text = "Income: " + product.income.ToString();
        unitSale.text = "Unit Salling: " + product.GetTotalSaleReport().unitSelling.ToString();
        price.text = "Price: " + product.price.ToString();
        costRelease.text = "Cost: " + product.cost.ToString();
        profit.text = "Profit: " + product.GetTotalSaleReport().profit.ToString();
        release.text = "Release: " + product.release.week+"/"+ product.release.month+"/"+ product.release.year;


        processBar.fillAmount = product.processCurrent / product.processMax;

        RefreshComponentCardUI();
    }

    public void RefreshComponentCardUI()
    {
        AddComponentCard(ComponentType.Genre, componentGenreContent, product.GetGenreDataList());
        AddComponentCard(ComponentType.Theme, componentThemeContent, product.GetThemeDataList());
        AddComponentCard(ComponentType.Graphic, componentGraphicContent, product.GetGraphicDataList());
        AddComponentCard(ComponentType.Camera, componentCameraContent, product.GetCameraDataList());
        AddComponentCard(ComponentType.Feature, componentFeatureContent, product.GetFeatureDataList());
        AddComponentCard(ComponentType.Platform, componentPlatformContent, product.GetPlatformDataList());
    }

    public void AddComponentCard(ComponentType componentType, Transform content, List<ComponentData> componentDatas)
    {
        content.GetComponent<ComponentCard>().ProductMode(product.categorie, componentType, componentDatas);
    }

    private void Update()
    {
        if (product != null)
        {
            UpdateProcessUI();
        }
    }

    public void UpdateProcessUI()
    {
        if (product.processCurrent > 0)
        {
            processBar.fillAmount = (float)product.processCurrent / product.processMax;
            float workProcess = ((float)product.processCurrent / product.processMax) * 100;
            processPercent.text = "Process " + (int)workProcess + "%";
        }
        else
        {
            processBar.fillAmount = 0;
            processPercent.text = "Process 0%";
        }
    }

    // Member
    public void RefreshSlotEmployee()
    {
        RemoveAllSlotEmployee();
        AddSlotEmployee();
    }

    private void AddSlotEmployee()
    {
        List<string> employeeIDs = product.employee_Worker;

        for (int i = 0; i < CompanyStructure.instance.GetMaxSlot().maxProductMember; i++)
        {
            if (i < employeeIDs.Count)
            {
                SlotEmployee slotEmployee_temp = Instantiate(slotEmployee, contentEmployee).GetComponent<SlotEmployee>();
                EmployeeData employeeData_temp = EmployeeStructure.instance.GetMyEmployeeData(employeeIDs[i]);
                slotEmployee_temp.Set(employeeData_temp);

                slotEmployee_temp.RemoveMemberMode(product, RefreshSlotEmployee);
            }
            else
            {
                Instantiate(selectEmployeeButton, contentEmployee).GetComponent<SelectEmployeeButton>().Set(product, () =>
                {
                    RefreshSlotEmployee();
                }, CompanyStructure.instance.GetMaxSlot().maxProductMember);
            }
        }
    }

    private void RemoveAllSlotEmployee()
    {
        for (int i = 0; i < contentEmployee.childCount; i++)
        {
            Destroy(contentEmployee.GetChild(i).gameObject);
        }
    }

    private void ClearButtonBusiness()
    {
        digitalDownloadButton.GetComponent<Button>().image.color = Color.white;
        freemiumButton.GetComponent<Button>().image.color = Color.white;
        subscriptionButton.GetComponent<Button>().image.color = Color.white;
        advertisingButton.GetComponent<Button>().image.color = Color.white;

    }

    private void CalculatePrice(BussinessModelType bussinessModelType)
    {
        product.SetBussinessModelType(bussinessModelType);

        if (bussinessModelType == BussinessModelType.Digital_Download)
        {
            int temp = (product.GetAllComponentDataList().Count * 2) / 3 * (product.GetAllComponentDataLevel()/2);
            priceDemo.text = temp +"$ / Unit";
        }
        else if (bussinessModelType == BussinessModelType.Freemium)
        {
            int temp = (product.GetAllComponentDataList().Count * 2) / 3 * product.GetAllComponentDataLevel();
            priceDemo.text = temp + "$ / Premium Unit";
        }
        else if (bussinessModelType == BussinessModelType.Subscription)
        {
            priceDemo.text = "10$ / Subscription";
        }
        else if (bussinessModelType == BussinessModelType.Advertising)
        {
            priceDemo.text = "0.01$ to 0.5$ / Unit";
        }
    }

    private void Deploy()
    {
        product.timeStamp = GameTimeStructure.instance.GetFutureTime(1);
    }

    //public GameObject componentCardTypeA;
    //public GameObject componentCardTypeB;

    //public GameObject commentCard;

    //public Transform componentPanel;
    //public Transform developPanel;
    //public Transform bussinessModelPanel;
    //public Transform saleReportPanel;
    //public Transform feedBackPanel;
    //public Transform marketingPanel;

    //// Component
    //public Transform componentGenreThemeContent;
    //public Transform componentGraphicCameraContent;
    //public Transform componentFeaturePlatformThemeContent;

    //// Member

    ////component
    //public Transform memberPanel;
    //public Transform componentContent;
    //public GameObject slotComponent;

    //public Transform memberContent;
    //public GameObject slotEmployee;
    //public GameObject selectEmployeeButton;
    //public int slotMemberMax;

    //// Bussiness Model
    //public Button digitalDownloadButton;
    //public Button freemiumButton;
    //public Button subscriptionButton;
    //public Button advertisingButton;

    //public Transform pricePanel;
    //public TMP_Text priceDisplay;
    //public Slider priceSlider;

    //// Sale Report
    //public TMP_Text unitSelling;
    //public TMP_Text costReport;
    //public TMP_Text profit;
    //public TMP_Text income;
    //public TMP_Text fan;

    //// Feed Back
    //public Transform commentContent;
    //public TMP_Text positiveCount;
    //public TMP_Text neutralCount;
    //public TMP_Text negativeCount;
    //public TMP_Text totalCommentCount;

    //// Marketing

    //public Button refreshButton;

    //[SerializeField] private Product product = new Product();

    //private void Start()
    //{
    //    CloseAllPanel();
    //    componentPanel.gameObject.SetActive(true);
    //    Component();

    //    deployButton.onClick.AddListener(() =>
    //    {
    //        Data.GameTimeStructure new_TimeStamp = new Data.GameTimeStructure();
    //        new_TimeStamp.week = GameTimeStructure.instance.GetGameTimeStructure().week;
    //        new_TimeStamp.month = GameTimeStructure.instance.GetGameTimeStructure().month;
    //        new_TimeStamp.year = GameTimeStructure.instance.GetGameTimeStructure().year;

    //        if (new_TimeStamp.month == 12)
    //        {
    //            new_TimeStamp.month = 1;
    //        }
    //        else
    //        {
    //            new_TimeStamp.month += 1;
    //        }

    //        product.timeStamp = new_TimeStamp;
    //    });

    //    componentButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        componentPanel.gameObject.SetActive(true);
    //        Component();
    //    });

    //    developButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        developPanel.gameObject.SetActive(true);
    //        Develop();
    //    });

    //    bussinessModelButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        bussinessModelPanel.gameObject.SetActive(true);
    //    });

    //    saleReportButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        saleReportPanel.gameObject.SetActive(true);
    //        SaleReport();
    //    });

    //    feedBackButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        feedBackPanel.gameObject.SetActive(true);
    //        Feedback();
    //    });

    //    marketingButton.onClick.AddListener(() =>
    //    {
    //        CloseAllPanel();
    //        marketingPanel.gameObject.SetActive(true);
    //    });

    //    // Bussiness Model
    //    digitalDownloadButton.onClick.AddListener(() =>
    //    {
    //        product.SetBussinessModelType(BussinessModelType.Digital_Download);
    //        pricePanel.gameObject.SetActive(true);
    //    });

    //    freemiumButton.onClick.AddListener(() =>
    //    {
    //        product.SetBussinessModelType(BussinessModelType.Freemium);
    //        pricePanel.gameObject.SetActive(true);
    //    });

    //    subscriptionButton.onClick.AddListener(() =>
    //    {
    //        product.SetBussinessModelType(BussinessModelType.Subscription);
    //        pricePanel.gameObject.SetActive(true);
    //    });

    //    advertisingButton.onClick.AddListener(() =>
    //    {
    //        product.SetBussinessModelType(BussinessModelType.Advertising);
    //        pricePanel.gameObject.SetActive(false);
    //    });

    //}

    //private void Update()
    //{
    //   if(pricePanel.gameObject.activeSelf)
    //    {
    //        product.SetPrice((int)priceSlider.value);
    //        priceDisplay.text = product.price.ToString();
    //    }
    //}

    //private void CloseAllPanel()
    //{
    //    componentPanel.gameObject.SetActive(false);
    //    bussinessModelPanel.gameObject.SetActive(false);
    //    saleReportPanel.gameObject.SetActive(false);
    //    feedBackPanel.gameObject.SetActive(false);
    //    marketingPanel.gameObject.SetActive(false);
    //    developPanel.gameObject.SetActive(false);
    //    memberPanel.gameObject.SetActive(false);
    //}

    //public void SaleReport()
    //{
    //    SaleReport saleReport = product.GetTotalSaleReport();

    //    unitSelling.text = saleReport.unitSelling.ToString();
    //    costReport.text = saleReport.cost.ToString();
    //    income.text = saleReport.income.ToString();
    //    profit.text = saleReport.profit.ToString();
    //    fan.text = saleReport.unitSelling.ToString();
    //}

    //private int positive;
    //private int neutral;
    //private int negative;

    //public void Feedback()
    //{
    //    RemoveAllCommentCard();
    //    AddCommentCard();

    //    foreach (var item in product.commentDatas)
    //    {
    //        if (item.sentiment == SentimentType.Positive)
    //        {
    //            positive++;
    //        }
    //        else if (item.sentiment == SentimentType.Neutral)
    //        {
    //            negative++;
    //        }
    //        else if (item.sentiment == SentimentType.Negative)
    //        {
    //            negative++;
    //        }
    //    }

    //    positiveCount.text = positive.ToString();
    //    neutralCount.text = neutral.ToString();
    //    negativeCount.text = negative.ToString();

    //    int total = positive + neutral + negative;
    //    totalCommentCount.text = total.ToString();
    //}

    //public void Develop()
    //{
    //    RefreshSlotComponent();
    //}

    //public void RefreshSlotComponent()
    //{
    //    RemoveAllSlotComponent(componentContent);
    //    AddSlotComponent(product.GetGenreDataList());
    //    AddSlotComponent(product.GetThemeDataList());
    //    AddSlotComponent(product.GetCameraDataList());
    //    AddSlotComponent(product.GetGraphicDataList());
    //    AddSlotComponent(product.GetFeatureDataList());
    //    AddSlotComponent(product.GetPlatformDataList());
    //}

    //private void AddSlotComponent(List<ComponentData> componentData_list)
    //{
    //    foreach (ComponentData componentData in componentData_list)
    //    {
    //        SlotComponent slotComponent_temp = Instantiate(slotComponent, componentContent).GetComponent<SlotComponent>();
    //        slotComponent_temp.Display(componentData);

    //        slotComponent_temp.ShowMemberMode(() =>
    //        {
    //            memberPanel.gameObject.SetActive(true);
    //            RefreshSlotEmployee(componentData);
    //        });
    //    }
    //}

    //private void RemoveAllSlotComponent(Transform content)
    //{
    //    for (int i = 0; i < content.childCount; i++)
    //    {
    //        Destroy(content.GetChild(i).gameObject);
    //    }
    //}

    //public void RefreshSlotEmployee(ComponentData componentData)
    //{
    //    RemoveAllSlotEmployee();
    //    AddSlotEmployee(componentData);
    //    memberPanel.gameObject.SetActive(true);
    //}

    //private void AddSlotEmployee(ComponentData componentData)
    //{
    //    List<int> employeeIDs = componentData.employee_Worker;

    //    for (int i = 0; i < slotMemberMax; i++)
    //    {
    //        if (i < employeeIDs.Count)
    //        {
    //            SlotEmployee slotEmployee_temp = Instantiate(slotEmployee, memberContent).GetComponent<SlotEmployee>();
    //            EmployeeData employeeData_temp = EmployeeStructure.instance.GetMyEmployeeData(employeeIDs[i]);
    //            slotEmployee_temp.Set(employeeData_temp);

    //            slotEmployee_temp.RemoveMemberMode(componentData, () => RefreshSlotEmployee(componentData));
    //        }
    //        else
    //        {
    //            Instantiate(selectEmployeeButton, memberContent).GetComponent<SelectEmployeeButton>().Set(product,componentData, () => RefreshSlotEmployee(componentData), slotMemberMax);
    //        }
    //    }
    //}

    //private void RemoveAllSlotEmployee()
    //{
    //    for (int i = 0; i < memberContent.childCount; i++)
    //    {
    //        Destroy(memberContent.GetChild(i).gameObject);
    //    }
    //}

    //public void RemoveAllCommentCard()
    //{
    //    for (int i = 0; i < commentContent.childCount; i++)
    //    {
    //        Destroy(commentContent.GetChild(i).gameObject);
    //    }
    //}

    //public void AddCommentCard()
    //{
    //    for (int i = 0; i < product.commentDatas.Count; i++)
    //    {
    //        GameObject commentCard_temp = Instantiate(commentCard, commentContent);
    //        commentCard_temp.GetComponent<CommentCard>().Set(product.commentDatas[i]);
    //    }
    //}

    //public Product GetProduct()
    //{
    //    return product;
    //}

    //public void SetName(string name)
    //{
    //    product.SetName(name);
    //}

    //public void SetProduct(Product product)
    //{
    //    this.product = product;
    //    productName.text = product.name;
    //    productRank.text = "#"+product.rank.ToString();
    //}

    //public void Component()
    //{
    //    RemoveAllComponent();

    //    MaxComponent maxComponent = CompanyStructure.instance.GetMaxComponent();

    //    AddComponentCard(ComponentType.Genre, componentCardTypeB, componentGenreThemeContent, product.GetGenreDataList(), maxComponent.maxGenre);
    //    AddComponentCard(ComponentType.Theme, componentCardTypeB, componentGenreThemeContent, product.GetThemeDataList(), maxComponent.maxTheme);
    //    AddComponentCard(ComponentType.Graphic, componentCardTypeB, componentGraphicCameraContent, product.GetGraphicDataList(), maxComponent.maxGraphic);
    //    AddComponentCard(ComponentType.Camera, componentCardTypeB, componentGraphicCameraContent, product.GetCameraDataList(), maxComponent.maxCamera);
    //    AddComponentCard(ComponentType.Feature, componentCardTypeA, componentFeaturePlatformThemeContent, product.GetFeatureDataList(), maxComponent.maxFeature);
    //    AddComponentCard(ComponentType.Platform, componentCardTypeA, componentFeaturePlatformThemeContent, product.GetPlatformDataList(), maxComponent.maxPlatform);
    //}

    //public void AddComponentCard(ComponentType componentType, GameObject componentCard, Transform content, List<ComponentData> componentDatas, int slotMax)
    //{
    //    GameObject componentCard_temp = Instantiate(componentCard, content);
    //    componentCard_temp.GetComponent<ComponentCard>().Set(product.categorie, componentType, componentDatas, slotMax);
    //}

    //public void RemoveAllComponent()
    //{
    //    for (int i = 0; i < componentGenreThemeContent.childCount; i++)
    //    {
    //        Destroy(componentGenreThemeContent.GetChild(i).gameObject);
    //    }

    //    for (int i = 0; i < componentGraphicCameraContent.childCount; i++)
    //    {
    //        Destroy(componentGraphicCameraContent.GetChild(i).gameObject);
    //    }

    //    for (int i = 0; i < componentFeaturePlatformThemeContent.childCount; i++)
    //    {
    //        Destroy(componentFeaturePlatformThemeContent.GetChild(i).gameObject);
    //    }
    //}
}
