using Firebase.Auth;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class Information
{
    public string uid;
    public string username;
    public string email;

    public Information(string uid, string username, string email)
    {
        this.uid = uid;
        this.username = username;
        this.email = email;
    }
}

public class FirebaseSignin : MonoBehaviour
{
    public TMP_InputField email_input, password_input;
    public Button SignupButton, SigninButton, AnonymousButton, SignoutButton;
    public GameObject SigninPanel;

    public Transform notAuthenticationPanel;
    public Transform authenticationPanel;
    public TMP_Text username;

    private void Awake()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://softwarehouse-ae0ed.firebaseio.com/");
    }

    // Use this for initialization
    private void Start()
    {
        // Get the root reference location of the database.
        //FirebaseData.instance.reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseData.instance.auth = FirebaseAuth.DefaultInstance;

        SignupButton.onClick.AddListener(() => SignUp(email_input.text, password_input.text));
        SigninButton.onClick.AddListener(() => SignIn(email_input.text, password_input.text));
        AnonymousButton.onClick.AddListener(() => Play());
        SignoutButton.onClick.AddListener(() =>
        {
            NotAuthentication();
            FirebaseData.instance.auth.SignOut();
        });

        AutoSignIn();
    }

    #region Create new user

    public void AutoSignIn()
    {
        if (FirebaseData.instance.auth.CurrentUser != null)
        {
            print("Users: " + FirebaseData.instance.auth.CurrentUser.UserId);
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users/" + FirebaseData.instance.auth.CurrentUser.UserId + "/Company Structure/name").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    print(task.Exception);
                    return;
                }

                DataSnapshot snapshot = task.Result;
                var result = snapshot.Value;

                UnityMainThreadDispatcher.instance.Enqueue(() => Authentication(result.ToString()));
            });
        }
        else
        {
            NotAuthentication();
        }
    }

    private void Authentication(string companyName)
    {
        username.text = "Company: " + companyName + "\nUser: " + FirebaseData.instance.auth.CurrentUser.UserId;
        MessageSystem.instance.UpdateMessage("Auto Signin: " + companyName);
        authenticationPanel.gameObject.SetActive(true);
        notAuthenticationPanel.gameObject.SetActive(false);
    }

    private void NotAuthentication()
    {
        notAuthenticationPanel.gameObject.SetActive(true);
        authenticationPanel.gameObject.SetActive(false);
    }

    public bool cloneCompanyStruture = false;
    public bool cloneGameTimeStruture = false;
    public bool cloneMapStruture = false;
    public bool cloneStartupStruture = false;

    private void CreateDataStarter(FirebaseUser newUser)
    {
        print("Clone data starter.");

        Information user = new Information(newUser.UserId, newUser.DisplayName, newUser.Email);
        string informationJson = JsonUtility.ToJson(user);

        FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(newUser.UserId).Child("Information").SetRawJsonValueAsync(informationJson);

        // Copy Map Starter
        UnityMainThreadDispatcher.instance.Enqueue(() => CloneCompanyStruture(newUser));

        // Copy GameTime Starter
        UnityMainThreadDispatcher.instance.Enqueue(() => CloneGameTimeStruture(newUser));

        // Copy Company Starter
        UnityMainThreadDispatcher.instance.Enqueue(() => CloneMapStruture(newUser));

        // Copy Startup Starter
        UnityMainThreadDispatcher.instance.Enqueue(() => CloneStartupStruture(newUser));

    }

    private void CloneStartupStruture(FirebaseUser newUser)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System").Child("Preset").Child("Startup Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            string startupJson = snapshot.GetRawJsonValue();
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(newUser.UserId).Child("Startup Structure").SetRawJsonValueAsync(startupJson);
            cloneStartupStruture = true;
        });
    }

    private void CloneCompanyStruture(FirebaseUser newUser)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System").Child("Preset").Child("Company Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            string companyJson = snapshot.GetRawJsonValue();
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(newUser.UserId).Child("Company Structure").SetRawJsonValueAsync(companyJson);
            cloneCompanyStruture = true;
        });
    }

    private void CloneGameTimeStruture(FirebaseUser newUser)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System").Child("Preset").Child("GameTime Structure").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            string gameTimeJson = snapshot.GetRawJsonValue();
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(newUser.UserId).Child("GameTime Structure").SetRawJsonValueAsync(gameTimeJson);
            cloneGameTimeStruture = true;
        });
    }

    private void CloneMapStruture(FirebaseUser newUser)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("Game System").Child("Preset").Child("Map Structure").Child("Starter").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                print(task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            string mapJson = snapshot.GetRawJsonValue();
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(newUser.UserId).Child("Map Structure").SetRawJsonValueAsync(mapJson);
            cloneMapStruture = true;
        });
    }

    #endregion

    public void SignUp(string email, string password)
    {
        if (notAuthenticationPanel.gameObject.activeSelf)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Debug.LogError("Email or Password is null.");
                MessageSystem.instance.UpdateMessage("Email or Password is empty.");
                UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));
                return;
            }

            FirebaseData.instance.auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));

                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Sign up failed"));
                    return;
                }
                if (task.IsFaulted)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));

                    Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Sign up failed"));
                    if (task.Exception.InnerExceptions.Count > 0)
                        MessageSystem.instance.UpdateMessage(task.Exception.InnerExceptions[0].Message);
                    return;
                }

                FirebaseUser user = task.Result; // Firebase user has been created.
                UnityMainThreadDispatcher.instance.Enqueue(() => CreateDataStarter(user));
                Debug.LogFormat("Firebase user created successfully: {0} ({1})", user.DisplayName, user.UserId);
                UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Signup Success"));
            });
        }
        else
        {
            MessageSystem.instance.UpdateMessage("Error, Now your are signin.");
        }
    }

    public void SignIn(string email, string password)
    {
        if (notAuthenticationPanel.gameObject.activeSelf)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Debug.LogError("Email or Password is null.");
                MessageSystem.instance.UpdateMessage("Email or Password is empty.");
                UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));
                return;
            }

            FirebaseData.instance.auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));

                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Sign in failed"));
                    return;
                }

                if (task.IsFaulted)
                {
                    UnityMainThreadDispatcher.instance.Enqueue(() => SetSigninPanel(true));

                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    UnityMainThreadDispatcher.instance.Enqueue(() => MessageSystem.instance.UpdateMessage("Sign in failed"));
                    if (task.Exception.InnerExceptions.Count > 0)
                        MessageSystem.instance.UpdateMessage(task.Exception.InnerExceptions[0].Message);
                    return;
                }

                FirebaseUser user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
                UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.reference =
                FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(user.UserId));

                FirebaseDatabase.DefaultInstance.RootReference.Child("Users/" + FirebaseData.instance.auth.CurrentUser.UserId + "/Company Structure/name").GetValueAsync().ContinueWith(task2 =>
                {
                    if (task2.IsFaulted)
                    {
                        print(task.Exception);
                        return;
                    }

                    DataSnapshot snapshot = task2.Result;
                    var result = snapshot.Value;

                    UnityMainThreadDispatcher.instance.Enqueue(() => Authentication(result.ToString()));
                });
            });
        }
        else
        {
            MessageSystem.instance.UpdateMessage("Error, Now your are signin.");
        }
    }

    public void Play()
    {
        SigninPanel.SetActive(false);

        if (FirebaseData.instance.auth.CurrentUser == null)
        {
            StartCoroutine(WaitForCloneData());

            UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                FirebaseUser user = task.Result;

                Debug.LogFormat("User anonymous signed in successfully: {0} ({1})", user.DisplayName, user.UserId);

                UnityMainThreadDispatcher.instance.Enqueue(() => CreateDataStarter(user));
                MessageSystem.instance.UpdateMessage("Signup Success");
            }));
        }
        else
        {
            UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.reference =
            FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(FirebaseData.instance.auth.CurrentUser.UserId));

            UnityMainThreadDispatcher.instance.Enqueue(() => LoadGameData());
            UnityMainThreadDispatcher.instance.Enqueue(SelectLoadScene());
        }
    }

    private IEnumerator WaitForCloneData()
    {
        while (!cloneCompanyStruture || !cloneGameTimeStruture || !cloneMapStruture)
        {
            yield return null;
        }

        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.reference =
        FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(FirebaseData.instance.auth.CurrentUser.UserId));

        UnityMainThreadDispatcher.instance.Enqueue(() => LoadGameData());
        UnityMainThreadDispatcher.instance.Enqueue(SelectLoadScene());
    }

    private void LoadGameData()
    {
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadCompanyFromFirebase());
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadGameTimeFromFirebase());
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadMapFromFirebase());
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadEmployeeFromFirebase());
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadWorkFromFirebase());
        UnityMainThreadDispatcher.instance.Enqueue(() => FirebaseData.instance.LoadStartupFromFirebase());
    }

    public void SetSigninPanel(bool state)
    {
        SigninPanel.SetActive(state);
    }

    private IEnumerator SelectLoadScene()
    {
        print("Select Load Scene");
        LoadingScreen.instance.SetActive(true);

        bool stop = false;

        while (!stop)
        {
            if (FirebaseData.instance.loadAllData &&
                    GameTimeStructure.instance.GetGameTimeStructure() != null &&
                    MapStructure.instance.GetMapStructure().ground_layer.Count != 0 &&
                    CompanyStructure.instance.GetCompanyStructure() != null)
            {

                if (CompanyStructure.instance.GetCompanyName() == "")
                {
                    print("Select Load Scene: Story");
                    AsyncOperation async = SceneManager.LoadSceneAsync("Story");
                    ProgressBar(async);
                }
                else
                {
                    print("Select Load Scene: Home");
                    AsyncOperation async = SceneManager.LoadSceneAsync("Home");
                    ProgressBar(async);
                }

                stop = true;

            }
            yield return null;
        }
    }

    private void ProgressBar(AsyncOperation async)
    {
        float progress = Mathf.Clamp01(async.progress / .9f);
        print("Progress: " + progress * 100 + "%");
        LoadingScreen.instance.SetLoadingBar(progress);
    }
}