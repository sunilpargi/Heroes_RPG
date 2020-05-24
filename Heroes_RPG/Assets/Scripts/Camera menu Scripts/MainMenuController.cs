using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttonPanel, characterSelectPanel, createCharacterPanel, optionPanel;

    private MainMenuCamera mainMenuCamera;

    void Awake()
    {
        mainMenuCamera = Camera.main.GetComponent<MainMenuCamera>();
    }

   public void PlayGame()
    {
        if (mainMenuCamera.CanClick)
        {
            mainMenuCamera.CanClick = false;

            buttonPanel.SetActive(false);
            characterSelectPanel.SetActive(true);

            mainMenuCamera.ReachedCharacterPosition = false;
        }

        // mainMenuCamera.ChangePosition(1);
        // buttonPanel.SetActive(false);
       // characterSelectPanel.SetActive(true);

    }

    public void BackToMainMenu()
    {
        if (mainMenuCamera.CanClick)
        {
            mainMenuCamera.CanClick = false;
            mainMenuCamera.BackToMainMenu = true;

            buttonPanel.SetActive(true);
            characterSelectPanel.SetActive(false);
        }

       // mainMenuCamera.ChangePosition(1);
        //  buttonPanel.SetActive(true);
        //characterSelectPanel.SetActive(false);
    }

    public void StartGame()
    {
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().Loadlevel("Village");
      //  SceneLoader.instance.Loadlevel("Village");
    }

    public void CreateCharacter()
    {
        characterSelectPanel.SetActive(false);
        createCharacterPanel.SetActive(true);
    }

    public void Accept()
    {
        characterSelectPanel.SetActive(true);
        createCharacterPanel.SetActive(false);
    }

    public void Cancel()
    {
        characterSelectPanel.SetActive(true);
        createCharacterPanel.SetActive(false);
    }

    public void OptionPanel()
    {
        optionPanel.SetActive(true);
        buttonPanel.SetActive(false);
    }

    public void CloseOptionPanel()
    {
        optionPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void SetQuality()
    {
        ChangeQuality();
    }

    public void SetResolution()
    {
        ChangeResolution();
    }

    public void ChangeQuality()
    {
        string level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (level)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0); 
                break;

            case "Normal":
                QualitySettings.SetQualityLevel(1);
                break;

            case "High":
                QualitySettings.SetQualityLevel(2);
                break;

            case "Ultra":
                QualitySettings.SetQualityLevel(3);
                break;

            case "No Shadows":
                if(QualitySettings.shadows == ShadowQuality.All)
                {
                    QualitySettings.shadows = ShadowQuality.Disable;
                }
                else
                {
                    QualitySettings.shadows = ShadowQuality.All;
                }
                break;
        }
    }

    public void ChangeResolution()
    {
        string index= UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (index)
        {
            case "0":
                Screen.SetResolution(1152, 648, true);
                break;

            case "1":
                Screen.SetResolution(1280, 720, true);
                break;

            case "2":
                Screen.SetResolution(1360, 768, true);
                break;

            case "3":
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
}
