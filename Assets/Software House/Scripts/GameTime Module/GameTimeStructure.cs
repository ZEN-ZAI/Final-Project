using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Data
{
    [Serializable]
    public class GameTimeStructure
    {
        public int week;
        public int month;
        public int year;

        public int timeMinute;
        public float timeSecond;

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["week"] = week;
            result["month"] = month;
            result["year"] = year;

            result["timeMinute"] = timeMinute;
            result["timeSecond"] = timeSecond;

            return result;
        }

        public GameTimeStructure Clone()
        {
            return new GameTimeStructure
            {
                week = this.week,
                month = this.month,
                year = this.year,

                timeMinute = this.timeMinute,
                timeSecond = this.timeSecond
            };
        }
    }
}

public class GameTimeStructure : MonoBehaviour
{
    #region Singleton
    public static GameTimeStructure instance;

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

    [SerializeField] private Data.GameTimeStructure gameTimeStructure;

    public bool set;

    private int weekSet = 4;
    private int monthSet = 12;

    private int timeMinuteSet = 2;
    private readonly float timeSecondSet = 59;

    // Start is called before the first frame update
    void Start()
    {
        gameTimeStructure.timeMinute = timeMinuteSet;
        gameTimeStructure.timeSecond = timeSecondSet;
    }

    public string GetJson()
    {
        return JsonUtility.ToJson(gameTimeStructure);
    }

    public void Set(string json)
    {
        gameTimeStructure = JsonUtility.FromJson<Data.GameTimeStructure>(json);
        set = true;
    }

    public void Timer()
    {
        if (Time.timeScale == 1)
        {
            RunTime();
        }
    }

    private void RunTime()
    {
        gameTimeStructure.timeSecond -= Time.deltaTime;

        if (gameTimeStructure.timeSecond <= 0 && gameTimeStructure.timeMinute > 0)
        {
            gameTimeStructure.timeMinute--;
            gameTimeStructure.timeSecond = timeSecondSet;
        }
        else if (gameTimeStructure.timeSecond <= 0 && gameTimeStructure.timeMinute == 0)
        {
            gameTimeStructure.week++;
            gameTimeStructure.timeMinute = timeMinuteSet;
            gameTimeStructure.timeSecond = timeSecondSet;

            if (gameTimeStructure.week > weekSet)
            {
                gameTimeStructure.week = 1;
                gameTimeStructure.month++;

                if (gameTimeStructure.month > monthSet)
                {
                    gameTimeStructure.month = 1;
                    gameTimeStructure.year++;
                }
            }
        }
    }

    public Data.GameTimeStructure GetFutureTime(int week)
    {
        Data.GameTimeStructure futureTime = gameTimeStructure.Clone();

        for (int i = 0; i < week; i++)
        {
            futureTime.week++;

            if (futureTime.week > weekSet)
            {
                futureTime.week = 1;
                futureTime.month++;

                if (futureTime.month > monthSet)
                {
                    futureTime.month = 1;
                    futureTime.year++;
                }
            }
        }
        futureTime.timeMinute = timeMinuteSet;
        futureTime.timeSecond = timeSecondSet;

        return futureTime;
    }

    public void NextMonth()
    {
        if (gameTimeStructure.month == 12)
        {
            gameTimeStructure.month = 1;
        }
        else
        {
            gameTimeStructure.month += 1;
        }
    }

    public Data.GameTimeStructure GetGameTimeStructure()
    {
        return gameTimeStructure;
    }
}