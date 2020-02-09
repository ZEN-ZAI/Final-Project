using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductListPanel : MonoBehaviour
{
    #region Singleton
    public static ProductListPanel instance;

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

    public Transform content;
    public GameObject slotProduct;

    // Start is called before the first frame update
    void Start()
    {
        RefreshSlotProduct();
    }

    public void RefreshSlotProduct()
    {
        RemoveAllSlotProduct();
        AddSlotProduct();
    }

    private void AddSlotProduct()
    {
        List<Product> products = StartupStructure.instance.GetProductList();

        for (int i = 0; i < CompanyStructure.instance.GetMaxWork().maxProduct; i++)
        {
            if (i < products.Count)
            {
                Product product_temp = products[i];
                SlotProduct slotProduct_temp = Instantiate(slotProduct, content).GetComponent<SlotProduct>();

                slotProduct_temp.Set(product_temp);
                slotProduct_temp.ShowProductMode();
            }
            else
            {
                Instantiate(slotProduct, content).GetComponent<SlotProduct>().AddProductMode();
            }
        }
    }

    private void RemoveAllSlotProduct()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
