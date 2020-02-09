using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaleReport
{
    public Data.GameTimeStructure gameTime;
    public int unitSelling;
    public int price;
    public int cost;
    public int income;
    public int profit;
    public int fan;

    public void Set(Data.GameTimeStructure gameTime, int unitSelling, int price, int cost, int income, int profit, int fan)
    {
        this.gameTime = gameTime;
        this.unitSelling = unitSelling;
        this.price = price;
        this.cost = cost;
        this.income = income;
        this.profit = profit;
        this.fan = fan;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        result["gameTime"] = gameTime.ToDictionary();
        result["unitSelling"] = unitSelling;
        result["price"] = price;
        result["cost"] = cost;
        result["income"] = income;
        result["profit"] = profit;
        result["fan"] = fan;

        return result;
    }
}
