using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugConsole : MonoBehaviour
{
    string myLog;

    Queue myLogQueue = new Queue(20);

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;

        string newString = " [" + type + "] : " + myLog + "\n";

        if (!myLogQueue.Contains(newString))
        {
            myLogQueue.Enqueue(newString);
        }

        if (myLogQueue.Count == 15)
        {
            myLogQueue.Dequeue();
        }

        myLog = string.Empty;

        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }
    GUIStyle gUIStyle = new GUIStyle();
    public string command = "";

    public Stack<string> previousCommand = new Stack<string>();
    public Stack<string> nextCommand = new Stack<string>();

    void OnGUI()
    {
        Event e = Event.current;

        if (e.keyCode == KeyCode.DownArrow && previousCommand.Count != 0)
        {
            command = nextCommand.Pop();
            previousCommand.Push(command);
        }

        else if (e.keyCode == KeyCode.UpArrow && nextCommand.Count != 0)
        {
            command = previousCommand.Pop();
            nextCommand.Push(command);
        }

        if (e.keyCode == KeyCode.Return && command != "")
        {
            nextCommand.Push(command);
            string[] command_list;
            command_list = command.Split(' ');

            switch (command_list[0])
            {
                case "cls":
                    myLogQueue.Clear();
                    previousCommand.Push(command);
                    break;

                case "save":

                    MapManager.instance.UpdateMapLayer();
                    GameManager.instance.SyncToFirebase();
                    Debug.Log("Update data to Firebase");
                    previousCommand.Push(command);
                    break;

                case "money"://money 99999

                    int money;
                    int.TryParse(command_list[1], out money);
                    CompanyStructure.instance.AddCompanyMoney(money);
                    Debug.Log("Add money: "+money);
                    previousCommand.Push(command);
                    break;

                case "preset-map":// preset-map MapName

                    FirebaseData.instance.AddPresetMap(command_list[1]);
                    Debug.Log("Save preset map: " + command_list[1]);
                    previousCommand.Push(command);
                    break;

                case "load-map":// load-map MapName

                    MapManager.instance.RemoveMap();
                    FirebaseData.instance.LoadPresetMap(command_list[1]);
                    Debug.Log("Load preset map: " + command_list[1]);
                    previousCommand.Push(command);
                    break;

                case "gen-ground":// gen-ground 20 20

                    if (command_list.Length < 3 || command_list.Length > 3)
                    {
                        return;
                    }

                    int row;
                    int column;

                    int.TryParse(command_list[1], out row);
                    int.TryParse(command_list[2], out column);

                    if (row <= 0 || column <= 0)
                    {
                        return;
                    }

                    if (row > 20 || column > 20)
                    {
                        return;
                    }


                    MapManager.instance.RemoveGround();
                    MapManager.instance.GenerateGround(row, column);
                    Debug.Log("Generate ground");
                    previousCommand.Push(command);
                    break;

                case "remove-map":

                    MapManager.instance.RemoveMap();
                    Debug.Log("Remove map");
                    previousCommand.Push(command);
                    break;

                case "add-component":// add-component 101 1

                    int componentID;
                    int level;

                    int.TryParse(command_list[1], out componentID);
                    int.TryParse(command_list[2], out level);

                    if (level <= 0)
                    {
                        Debug.LogError("Invalid Component level. (" + System.DateTime.Now + ")");
                        return;
                    }

                    Component component = ComponentAsset.instance.GetComponentAsset(command_list[1]);

                    if (component == null)
                    {
                        Debug.LogError("Invalid ComponentID. (" + System.DateTime.Now + ")");
                        return;
                    }

                    StartupStructure.instance.AddComponent(new ComponentData(component, level,0));

                    Debug.Log("Add Component: " + component.name+" [Level: "+level+"]");
                    previousCommand.Push(command);
                    break;

                case "preset-Startup":

                    FirebaseData.instance.PresetStartupStarter();

                    Debug.Log("Preset Component Starter.");
                    previousCommand.Push(command);
                    break;

                case "preset-company":

                    FirebaseData.instance.PresetCompanyStarter();

                    Debug.Log("Preset Company Starter.");
                    previousCommand.Push(command);
                    break;

                //case "clone-component":

                //    Debug.Log("Clone All Component.");
                //    previousCommand.Push(command);
                //    break;

                case "skip-month":

                    GameTimeStructure.instance.NextMonth();
                    Debug.Log("Skip-Mouth");
                    previousCommand.Push(command);
                    break;

                default:
                    Debug.LogError("Invalid command. Please read my guide. (" + System.DateTime.Now + ")");
                    break;
            }
            command = "";
        }

        GUI.skin.textArea.fontSize = 30;
        GUI.skin.textField.fontSize = 30;

        GUI.TextArea(new Rect(10, 50, (Screen.width / 2), 50), "\t\t Debug Console : ");
        GUI.TextArea(new Rect(10, 100, (Screen.width / 2), (Screen.height / 2)), myLog);
        command = GUI.TextField(new Rect(10, (Screen.height / 2) + 100, (Screen.width / 2), 40), command, 200);
    }
}
