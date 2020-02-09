using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DebugConsole))]
public class DebugControl : MonoBehaviour
{
    public static DebugControl instance;
    private DebugConsole debugConsole;

    public Button debugConsoleButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            debugConsole = GetComponent<DebugConsole>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        debugConsoleButton.onClick.AddListener(()=>
        {
            if (debugConsole.enabled)
            {
                SetActive(false);
            }
            else if (!debugConsole.enabled)
            {
                SetActive(true);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8) && debugConsole.enabled)
        {
            SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F8) && !debugConsole.enabled)
        {
            SetActive(true);
        }
    }

    public void SetActive(bool state)
    {
        debugConsole.enabled = state;
    }
}
