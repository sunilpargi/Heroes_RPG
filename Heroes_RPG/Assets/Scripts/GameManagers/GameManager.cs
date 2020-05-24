using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;

    [HideInInspector]
    public int selectedCahraterIndex;

    public GameObject playerInventory;

    void Awake()
    {
        MakingSingleton();
    }

     void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    void MakingSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "MainMenu")
        {
            Instantiate(playerInventory, Vector3.zero, Quaternion.identity);

            Vector3 pos = GameObject.FindGameObjectWithTag("SpawnPosition").transform.position;
            Instantiate(characters[selectedCahraterIndex], pos, Quaternion.identity);
        }
    }
}
