using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyManager : MonoBehaviour
{
    #region Singleton
    public static CompanyManager instance;
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

    public void CheckLeveling()
    {
        Data.CompanyStructure companyStructure = CompanyStructure.instance.GetCompanyStructure();

        if (companyStructure.exp == companyStructure.GetCompanyMaxExp())
        {
            companyStructure.exp = 0;
            companyStructure.level++;
            companyStructure.point++;
        }
        else if (companyStructure.exp > companyStructure.GetCompanyMaxExp())
        {
            companyStructure.exp = companyStructure.GetCompanyMaxExp() - companyStructure.exp;
            companyStructure.level++;
            companyStructure.point++;
        }
    }
}
