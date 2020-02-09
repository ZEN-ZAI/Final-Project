using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

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
        //DontDestroyOnLoad(gameObject);
    }

    #endregion

    private GameState gameState;
    public GameState GameState
    {
        get { return gameState; }
    }

    public bool setup = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        gameState = GameState.Play;

        if (!GameTimeStructure.instance.set)
        {
            print("GameTimeStructure is NULL -> Generate GameTimeStructure");
            GameTimeStructure.instance.Set("{}");
        }

        if (!MapStructure.instance.set)
        {
            print("MapStructure NULL -> Generate Map Starter");
            MapStructure.instance.Set(null);
            MapManager.instance.LoadMapStarter();
        }
        else
        {
            BuildConstruct();
        }

        if (!CompanyStructure.instance.set)
        {
            print("CompanyStructure is NULL -> Generate CompanyStructure");
            CompanyStructure.instance.Set("{}");
        }

        if (!EmployeeStructure.instance.set)
        {
            print("EmployeeStructure NULL -> Generate EmployeeStructure");
            EmployeeStructure.instance.Set(null);
        }

        if (EmployeeStructure.instance.set && EmployeeStructure.instance.GetRecruitmentEmployeeCount() == 0)
        {
            EmployeeManager.instance.RandomRecruitmentEmployee(5);
            EmployeeManager.instance.GenerateStaterCharacter();
        }
        else if(EmployeeStructure.instance.GetRecruitmentEmployeeCount() > 0 && EmployeeStructure.instance.set)
        {
            BuildEmployee();
        }

        if (!WorkStructure.instance.set)
        {
            print("WorkStructure NULL -> Generate WorkStructure");
            WorkStructure.instance.Set(null);
        }
        else if (WorkStructure.instance.GetContractScenarioTypeCount() == 0)
        {
            WorkManager.instance.RandomOutsourceScenarioType(5);
        }
        else if (WorkStructure.instance.GetContractGraphicTypeCount() == 0)
        {
            WorkManager.instance.RandomOutsourceGraphicType(5);
        }
        else if (WorkStructure.instance.GetContractModuleTypeCount() == 0)
        {
            WorkManager.instance.RandomOutsourceModuleType(5);
        }
        else if (WorkStructure.instance.GetContractSupportTypeCount() == 0)
        {
            WorkManager.instance.RandomOutsourceSupportType(5);
        }
        else if (WorkStructure.instance.GetContractFullGameProjectTypeCount() == 0)
        {
            WorkManager.instance.RandomOutsourceFullGameProjectType(5);
        }

        if (!StartupStructure.instance.set)
        {
            print("StartupStructure NULL -> Generate StartupStructure");
            StartupStructure.instance.Set(null);
            StartupStructure.instance.GetStartupStructure().genreResearch = ComponentAsset.instance.GetStarterComponentGenreDict();
            StartupStructure.instance.GetStartupStructure().graphicResearch = ComponentAsset.instance.GetStarterComponentGraphicDict();
            StartupStructure.instance.GetStartupStructure().themeResearch = ComponentAsset.instance.GetStarterComponentThemeDict();
            StartupStructure.instance.GetStartupStructure().cameraResearch = ComponentAsset.instance.GetStarterComponentCameraDict();
            StartupStructure.instance.GetStartupStructure().featureResearch = ComponentAsset.instance.GetStarterComponentFeatureDict();
            StartupStructure.instance.GetStartupStructure().platformResearch = ComponentAsset.instance.GetStarterComponentPlatformDict();
        }
        else
        {
            print("Product Count: " + StartupStructure.instance.GetProductCount());
        }

        Invoke("LoadingScreenOff", 0f);

        setup = true;
    }

    private void LoadingScreenOff()
    {
        LoadingScreen.instance.SetActive(false);
    }

    private int nextUpdate;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Play)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        if (gameState == GameState.Play && setup)
        {
            GameTimeStructure.instance.Timer();

            if (Time.time >= nextUpdate && FirebaseData.instance.reference != null)
            {
                //Debug.Log(Time.time + ">=" + nextUpdate);
                nextUpdate = Mathf.FloorToInt(Time.time) + 5;
                FirebaseData.instance.SyncToFirebase();
            }

            CompanyManager.instance.CheckLeveling();
            WorkManager.instance.CheckOutsourceDueDate();
            WorkManager.instance.CheckProductTimeStamp();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.Pause;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameState = GameState.Play;
    }

    public void SyncToFirebase()
    {
        FirebaseData.instance.SyncToFirebase();
    }

    public void UpdateToFirebase()
    {
        UpdateCompanyToFirebase();
        UpdateGameTimeToFirebase();
        UpdateMapToFirebase();
        UpdateEmployeeToFirebase();
        UpdateWorkToFirebase();
        UpdateStartupToFirebase();
    }

    private void UpdateCompanyToFirebase()
    {
        FirebaseData.instance.UpdateCompanyToFirebase();
    }

    private void UpdateMapToFirebase()
    {
        MapManager.instance.UpdateMapLayer();
        FirebaseData.instance.UpdateMapToFirebase();
    }

    private void UpdateEmployeeToFirebase()
    {
        FirebaseData.instance.UpdateEmployeeToFirebase();
    }

    private void UpdateGameTimeToFirebase()
    {
        FirebaseData.instance.UpdateGameTimeToFirebase();
    }

    private void UpdateWorkToFirebase()
    {
        FirebaseData.instance.UpdateWorkToFirebase();
    }

    private void UpdateStartupToFirebase()
    {
        FirebaseData.instance.UpdateStartupToFirebase();
    }

    #region Setup

    private void BuildConstruct()
    {
        MapManager.instance.Build();
    }

    private void BuildEmployee()
    {
        EmployeeManager.instance.BuildEmployee(EmployeeStructure.instance.GetMyEmployeeDataList());
    }

    #endregion
}