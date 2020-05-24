using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

  public void LoadOtherWorld()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        SceneLoader.instance.Loadlevel(name);
    }
}
