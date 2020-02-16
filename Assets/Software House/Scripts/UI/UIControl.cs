using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    #region Singleton
    public static UIControl instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public TMP_Text day;
    public Image expBar;

    public TMP_Text time;

    public TMP_Text companyName;
    public TMP_Text companyLevel;
    public TMP_Text companyEXP;
    //public TMP_Text companyTier;
    //public TMP_Text companyPopular;
    public TMP_Text companyMoney;

    void Start()
    {
        CompanyTag();
    }

    void Update()
    {
        CompanyTag();
        Time();
        Clock();
        Exp();
    }

    public void Time()
    {
        if (GameTimeStructure.instance.GetGameTimeStructure() != null)
        {
            Data.GameTimeStructure gameTime = GameTimeStructure.instance.GetGameTimeStructure();

            day.text = gameTime.week + "/"+ gameTime.month + "/" + gameTime.year;
        }
    }

    public void Exp()
    {
        float temp = CompanyStructure.instance.GetCompanyExp();
        expBar.fillAmount = (temp / CompanyStructure.instance.GetCompanyMaxExp());
    }

    public void Clock()
    {
        if (GameTimeStructure.instance.GetGameTimeStructure() != null)
        {
            Data.GameTimeStructure gameTime = GameTimeStructure.instance.GetGameTimeStructure();

            time.text = gameTime.timeMinute + ":" + gameTime.timeSecond.ToString("0#");
        }
    }

    public void CompanyTag()
    {
        if (CompanyStructure.instance.GetCompanyStructure() != null)
        {
            Data.CompanyStructure company = CompanyStructure.instance.GetCompanyStructure();

            companyName.text = company.name;
            companyLevel.text = "Lv." + company.level;
            companyEXP.text = company.exp.ToString("C0").Replace("$", "") + "/"+ CompanyStructure.instance.GetCompanyMaxExp().ToString("C0").Replace("$", "") ;
            //companyTier.text = company.tier;
            //companyPopular.text = company.popular + "";
            companyMoney.text = company.money.ToString("C0").Replace("$","");
        }
    }
}
