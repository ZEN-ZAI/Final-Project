using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;

public class CreateProductPanel : MonoBehaviour
{
    public TMP_InputField platformNameField;

    public Button createProductButton;

    // Component
    public Transform componentGenreContent;
    public Transform componentThemeContent;
    public Transform componentGraphicContent;
    public Transform componentCameraContent;
    public Transform componentFeatureContent;
    public Transform componentPlatformContent;

    [SerializeField] private Product product = new Product();

    // Start is called before the first frame update
    void Start()
    {
        SetCategorieType();
        Component();

        createProductButton.onClick.AddListener(() => CreateProduct());
    }

    // Update is called once per frame
    void Update()
    {
        if (platformNameField.text == "")
        {
            createProductButton.gameObject.SetActive(false);
        }
        else
        {
            createProductButton.gameObject.SetActive(true);
        }
    }

    public void Component()
    {
        MaxComponent maxComponent = CompanyStructure.instance.GetMaxComponent();

        AddComponentCard(ComponentType.Genre, componentGenreContent, product.GetGenreDataList(), maxComponent.maxGenre);
        AddComponentCard(ComponentType.Theme, componentThemeContent, product.GetThemeDataList(), maxComponent.maxTheme);
        AddComponentCard(ComponentType.Graphic, componentGraphicContent, product.GetGraphicDataList(), maxComponent.maxGraphic);
        AddComponentCard(ComponentType.Camera, componentCameraContent, product.GetCameraDataList(), maxComponent.maxCamera);
        AddComponentCard(ComponentType.Feature, componentFeatureContent, product.GetFeatureDataList(), maxComponent.maxFeature);
        AddComponentCard(ComponentType.Platform, componentPlatformContent, product.GetPlatformDataList(), maxComponent.maxPlatform);
    }

    public void AddComponentCard(ComponentType componentType, Transform content, List<ComponentData> componentDatas, int slotMax)
    {
        content.GetComponent<ComponentCard>().CreateMode(product.categorie, componentType, componentDatas, slotMax);
    }

    public void SetCategorieType()
    {
        product.SetCategorie(CategorieType.Game);
    }

    public void CreateProduct()
    {
        product.SetName(platformNameField.text);
        product.SetProcessMax(CalculateProcessMax());

        if (product.GetGenreDataList().Count >= 1
            && product.GetThemeDataList().Count >= 1
            && product.GetGraphicDataList().Count >= 1
            && product.GetCameraDataList().Count >= 1
            && product.GetFeatureDataList().Count >= 1
            && product.GetPlatformDataList().Count >= 1
            && platformNameField.text != ""
            && platformNameField.text != "OutSource"
            && platformNameField.text != "Research"
            && platformNameField.text != "Product")
        {
            if (FirebaseData.instance.reference != null)
            {
                if (StartupStructure.instance.GetProduct(platformNameField.text) == null)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Suscess, Platform [" + platformNameField.text + "] has created."));
                    UnityMainThreadDispatcher.instance.Enqueue(() => StartupStructure.instance.AddProduct(product));
                    UnityMainThreadDispatcher.instance.Enqueue(() => Destroy(this.gameObject));
                    UnityMainThreadDispatcher.instance.Enqueue(() => PanelControl.instance.OpenProductDevelopmentPanel());
                    UnityMainThreadDispatcher.instance.Enqueue(() => ProductPanel.instance.SetProduct(product));

                    UnityMainThreadDispatcher.instance.Enqueue(() =>
                    {
                        Dictionary<string, object> child = new Dictionary<string, object>();

                        foreach (var item in StartupStructure.instance.GetProductList())
                        {
                            child.Add(product.name,"");
                        }

                        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Share Data/Product-UID/"+CompanyStructure.instance.GetCompanyName()).UpdateChildrenAsync(child);
                    });
                }
                else
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Error, Product name [" + platformNameField.text + "] has exist."));
                    //        UnityMainThreadDispatcher.instance.Enqueue(() => createProductButton.interactable = false);
                }
            }
            else if (FirebaseData.instance.reference == null)
            {
                if (StartupStructure.instance.GetProduct(platformNameField.text) == null)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Suscess, Platform [" + platformNameField.text + "] has created."));
                    UnityMainThreadDispatcher.instance.Enqueue(() => StartupStructure.instance.AddProduct(product));
                    UnityMainThreadDispatcher.instance.Enqueue(() => Destroy(this.gameObject));
                    UnityMainThreadDispatcher.instance.Enqueue(() => PanelControl.instance.OpenProductDevelopmentPanel());
                    UnityMainThreadDispatcher.instance.Enqueue(() => ProductPanel.instance.SetProduct(product));
                }
                else
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Error, Product name [" + platformNameField.text + "] has exist."));
                }
            }
        }
    }

    public int CalculateProcessMax()
    {
        int allLevelComponent_temp = 0;

        foreach (ComponentData componentRequirements in product.genreDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }
        foreach (ComponentData componentRequirements in product.cameraDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }
        foreach (ComponentData componentRequirements in product.themeDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }
        foreach (ComponentData componentRequirements in product.graphicDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }
        foreach (ComponentData componentRequirements in product.featureDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }
        foreach (ComponentData componentRequirements in product.platformDatas)
        {
            allLevelComponent_temp += componentRequirements.productLevel;
        }

        return allLevelComponent_temp;
    }
}
