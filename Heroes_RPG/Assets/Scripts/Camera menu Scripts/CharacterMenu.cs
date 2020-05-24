using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject charPosition;

    private int knight_Worrior_Index = 0;
    private int king_Worrior_Index = 1;
    private int Catgirl_Worrior_Index = 2;

    void Start()
    {
        characters[knight_Worrior_Index].SetActive(true);
        characters[knight_Worrior_Index].transform.position = charPosition.transform.position;
    }

  public void SelectCharacter()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        TurnOffCharacter();

        characters[index].SetActive(true);
        characters[index].transform.position = charPosition.transform.position;

        GameManager.instance.selectedCahraterIndex = index;
    }

    void TurnOffCharacter()
    {
        for(int i =0; i< characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }
}
