using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;

    private Vector2 moveDirection;
    private Agent agent;

    private Command keyMove, keySpace, keyNotthing, mouseMove;
    //private List<Command> oldCommands = new List<Command>();

    /*Coroutine replayCoroutine;
    public bool shouldStartReplay;
    public bool isReplaying;*/

    // Start is called before the first frame update
    void Start()
    {
        keyMove = new PerformMoveWithKey();
        keySpace = new PerformDash();
        keyNotthing = new PerformIdle();
        mouseMove = new PerformMoveWithMouse();

        agent = actor.GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        /*if (!isReplaying)
            HandleInput();
        StartReplay();*/
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(agent, true);
            //oldCommands.Add(keySpace);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            keyMove.Execute(agent, true);
            //oldCommands.Add(keyMove);
        }
        /*else
        {
            keyNotthing.Execute(agent, true);
            //oldCommands.Add(keyNotthing);
        }*/

        /*if (Input.GetKeyDown(KeyCode.R))
            shouldStartReplay = true;

        if (Input.GetKey(KeyCode.Z))
            UndoLastCommand();*/

        if (Input.GetMouseButtonDown(0))
        {
            mouseMove.Execute(agent, true);
        }

    }

    /*void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command c = oldCommands[oldCommands.Count - 1];
            c.Execute(anim,agent, false);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }

    void StartReplay()
    {
        if(shouldStartReplay && oldCommands.Count > 0)
        {
            shouldStartReplay = false;
            if(replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    IEnumerator ReplayCommands()
    {
        isReplaying = true;

        for(int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(agent, true);
            yield return new WaitForSeconds(0f);
        }

        isReplaying = false;
    }*/
}
