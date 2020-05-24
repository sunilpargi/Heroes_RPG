using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public GameObject gameStartedPosition;
    public GameObject characterSelectPosition;

    private bool reached_gameStartedPosition;

    private bool reached_CharacterSelectPosition = true;
    private bool canClick;
    private bool backToMainMenu;

    //Easy way
    private List<GameObject> position = new List<GameObject>();
    // Easy Way

    void Awake()
    {
        position.Add(gameStartedPosition);
    }

    
    void Update()
    {
       MoveToGameStartedPosition();
        MoveToCharacterSelectMenu();
       MoveBackToMainmenu();
    }

    void MoveToPosition()
    {
        if(position.Count > 0)
        {
            transform.position = Vector3.Lerp(transform.position, position[0].transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, position[0].transform.rotation, 1f * Time.deltaTime);
        }
    }

    public void ChangePosition(int index)
    {
        position.RemoveAt(0);

        if(index == 0)
        {
            position.Add(gameStartedPosition);
        }
        else
        {
            position.Add(characterSelectPosition);
        }
    }

    void MoveToGameStartedPosition()
    {
        if (!reached_gameStartedPosition)
        {
            if(Vector3.Distance(transform.position,gameStartedPosition.transform.position) < 0.2f)
            {
                reached_gameStartedPosition = true;
                canClick = true;
            }
        }

        if (!reached_gameStartedPosition)
        {
            transform.position = Vector3.Lerp(transform.position, gameStartedPosition.transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, gameStartedPosition.transform.rotation, 1f * Time.deltaTime);
        }
    }

    void MoveToCharacterSelectMenu()
    {
         if (!reached_CharacterSelectPosition)
        {
            if(Vector3.Distance(transform.position, characterSelectPosition.transform.position) < 0.2f)
            {
                reached_CharacterSelectPosition = true;
                canClick = true;
            }
        }

        if (!reached_CharacterSelectPosition)
        {
            transform.position = Vector3.Lerp(transform.position, characterSelectPosition.transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, characterSelectPosition.transform.rotation, 1f * Time.deltaTime);
        }
    }

    void MoveBackToMainmenu()
    {
        if (backToMainMenu)
        {
            if (Vector3.Distance(transform.position, gameStartedPosition.transform.position) < 0.2f)
            {
                backToMainMenu = true;
                canClick = true;
            }

        }
        if (backToMainMenu)
        {
            transform.position = Vector3.Lerp(transform.position, characterSelectPosition.transform.position, 1f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, characterSelectPosition.transform.rotation, 1f * Time.deltaTime);
        }
    }

    public bool ReachedCharacterPosition
    {
        get
        {
            return reached_CharacterSelectPosition;
        }
        set
        {
            reached_CharacterSelectPosition = value;
        }
    }
    public bool CanClick
    {
        get
        {
            return canClick;
        }
        set
        {
            canClick = value;
        }
    }

    public bool BackToMainMenu
    {
        get
        {
            return backToMainMenu;
        }
        set
        {
            backToMainMenu = value;
        }
    }
}
