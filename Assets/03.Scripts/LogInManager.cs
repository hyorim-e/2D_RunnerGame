using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviour
{
    DBController dBController;
    public bool isLoggIn = false;
    string memberID;
    public TMP_InputField idText;
    public TMP_InputField passText;
    ArrayList characterList = new ArrayList();

    public GameObject logInUI;
    public GameObject selectUI;

    public GameObject[] Characters;

    private void Awake()
    {
        dBController = gameObject.GetComponent<DBController>();
    }

    public void LogIn()
    {
        if(dBController.LogIn(idText.text, passText.text))
        {
            Debug.Log("�α����� �Ϸ�Ǿ����ϴ�");
            isLoggIn = true;
            memberID = idText.text;
            PlayerPrefs.SetString("memberID", memberID);

            characterList = dBController.SelectCharacters(idText.text);
            for (int i = 0; i < Characters.Length; i++)
            {
                if (characterList.Contains(Characters[i].name))
                {
                    Characters[i].SetActive(true);
                }
            }

            logInUI.SetActive(false);
            selectUI.SetActive(true);
        }
        else
        {
            Debug.Log("�α��� ����");
        }
    }

    public void SelectCharacter(string id)
    {
        SceneManager.LoadScene("GameScene");

        PlayerPrefs.SetString(id, id);
    }
}
