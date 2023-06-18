using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    public GameObject[] characters;

    private void Awake()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (PlayerPrefs.HasKey(characters[i].name))
            {
                characters[i].SetActive(true);
                PlayerPrefs.DeleteKey(characters[i].name);
            }
        }       
    }
}
