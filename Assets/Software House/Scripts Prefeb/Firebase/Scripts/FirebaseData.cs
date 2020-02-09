using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class FirebaseData : MonoBehaviour
{
    public FirebaseAuth auth;
    public DatabaseReference reference;

    #region Singleton
    public static FirebaseData instance;
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

    public bool loadCompany;
    public bool loadGameTime;
    public bool loadMap;
    public bool loadEmployee;
    public bool loadWork;
    public bool loadStartup;

    public bool loadAllData
    {
        get
        {
            if (loadCompany
                && loadMap
                && loadEmployee
                && loadGameTime
                && loadWork
                && loadStartup)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void LoadCompanyFromFirebase()
    {
        reference.Child("Company Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            CompanyStructure.instance.Set(json);
            loadCompany = true;

            print("Load Company From Firebase [Finish]");
        });
    }

    public void LoadMapFromFirebase()
    {
        reference.Child("Map Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            MapStructure.instance.Set(json);
            loadMap = true;

            print("Load Map From Firebase [Finish]");
        });
    }

    public void LoadEmployeeFromFirebase()
    {
        reference.Child("Employee Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            EmployeeStructure.instance.Set(json);
            loadEmployee = true;

            print("Load Employee From Firebase [Finish]");
        });
    }

    public void LoadGameTimeFromFirebase()
    {
        reference.Child("GameTime Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            GameTimeStructure.instance.Set(json);
            loadGameTime = true;

            print("Load GameTime From Firebase [Finish]");
        });
    }

    public void LoadWorkFromFirebase()
    {
        reference.Child("Work Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            WorkStructure.instance.Set(json);
            loadWork = true;

            print("Load Work From Firebase [Finish]");
        });
    }

    public void LoadStartupFromFirebase()
    {
        reference.Child("Startup Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();
            StartupStructure.instance.Set(json);
            loadStartup = true;

            print("Load Startup From Firebase [Finish]");
        });
    }

    public void UpdateMapToFirebase()
    {
        print("Save Map To Firebase");
        reference.Child("Map Structure").SetRawJsonValueAsync(MapStructure.instance.GetJson());
    }

    public void UpdateEmployeeToFirebase()
    {
        print("Save Employee To Firebase");
        reference.Child("Employee Structure").SetRawJsonValueAsync(EmployeeStructure.instance.GetJson());
    }

    public void UpdateGameTimeToFirebase()
    {
        print("Save GameTime To Firebase");
        reference.Child("GameTime Structure").SetRawJsonValueAsync(GameTimeStructure.instance.GetJson());
    }

    public void UpdateWorkToFirebase()
    {
        print("Save Work To Firebase");
        reference.Child("Work Structure").SetRawJsonValueAsync(WorkStructure.instance.GetJson());
    }

    public void UpdateCompanyToFirebase()
    {
        print("Save Company To Firebase");
        reference.Child("Company Structure").SetRawJsonValueAsync(CompanyStructure.instance.GetJson());
    }

    public void UpdateStartupToFirebase()
    {
        print("Save Startup To Firebase");
        reference.Child("Startup Structure").SetRawJsonValueAsync(StartupStructure.instance.GetJson());
    }

    public void SyncToFirebase()
    {
        SyncMapToFirebase();
        SyncEmployeeToFirebase();
        SyncGameTimeToFirebase();
        SyncWorkToFirebase();
        SyncCompanyToFirebase();
        SyncStartupToFirebase();
    }

    public void SyncMapToFirebase()
    {
        reference.Child("Map Structure").UpdateChildrenAsync(MapStructure.instance.GetMapStructure().ToDictionary());
    }

    public void SyncEmployeeToFirebase()
    {
        reference.Child("Employee Structure").UpdateChildrenAsync(EmployeeStructure.instance.GetEmployeeStructure().ToDictionary());
    }

    public void SyncGameTimeToFirebase()
    {
        reference.Child("GameTime Structure").UpdateChildrenAsync(GameTimeStructure.instance.GetGameTimeStructure().ToDictionary());
    }

    public void SyncWorkToFirebase()
    {
        reference.Child("Work Structure").UpdateChildrenAsync(WorkStructure.instance.GetWorkStructure().ToDictionary());
    }

    public void SyncCompanyToFirebase()
    {
        reference.Child("Company Structure").UpdateChildrenAsync(CompanyStructure.instance.GetCompanyStructure().ToDictionary());
    }

    public void SyncStartupToFirebase()
    {
        reference.Child("Startup Structure").UpdateChildrenAsync(StartupStructure.instance.GetStartupStructure().ToDictionary());
    }

    //FirebaseData.instance.AddJsonToFirebase("Game System/Preset","Preset");
    //FirebaseData.instance.AddJsonToFirebase("Game System/Share Data/Company-UID", "Share Data Company-UID");
    public void AddJsonToFirebase(string key, string filePath)
    {
        JsonLoader jsonLoader = new JsonLoader();

        string json = jsonLoader.LoadJson(filePath);
        print(json);

        FirebaseDatabase.DefaultInstance.RootReference.Child(key).SetRawJsonValueAsync(json);
    }

    public void AddPresetMap(string name)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Preset/Map Structure/" + name).UpdateChildrenAsync(MapStructure.instance.GetMapStructure().ToDictionary());
    }

    public void PresetCompanyStarter()
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Preset/Company Structure").UpdateChildrenAsync(CompanyStructure.instance.GetCompanyStructure().ToDictionary());
    }

    public void PresetStartupStarter()
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Preset/Startup Structure").UpdateChildrenAsync(StartupStructure.instance.GetStartupStructure().ToDictionary());
    }

    public void LoadPresetMap(string name)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System/Preset/Map Structure/" + name).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string json = snapshot.GetRawJsonValue();

            UnityMainThreadDispatcher.instance.Enqueue(()=> MapStructure.instance.Set(json));
            UnityMainThreadDispatcher.instance.Enqueue(()=> print("Load Preset-Map From Firebase [Finish]"));
            UnityMainThreadDispatcher.instance.Enqueue(() => MapManager.instance.Build());
        });
    }

    public void RunEmpID()
    {

    }

    //public void ReadData()
    //{
    //    reference.Child("Information")
    //    // หากข้อมูลมีการเปลี่ยนแหลงให้ทำการอ่านและแสดง
    //    .ValueChanged += HandleValueChanged;
    //}

    //private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null)
    //    {
    //        Debug.LogError(args.DatabaseError.Message);
    //        return;
    //    }

    //    // อ่าน Key เพื่อใช้แสดงผล
    //    List<string> keys = args.Snapshot.Children.Select(s => s.Key).ToList();
    //    foreach (var key in keys)
    //    {
    //        DisplayData(args.Snapshot, key);
    //    }

    //}

    //void DisplayData(DataSnapshot snapshot, string key)
    //{
    //    string json = snapshot.GetRawJsonValue();
    //    Information u = JsonUtility.FromJson<Information>(json);
    //    Debug.Log(u.uid + " " + u.username);
    //}

    /*private void Update()
    {
        if (reference != null)
        {
            RaadAllData();
        }
    }*/
}
