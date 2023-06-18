using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class DBController : MonoBehaviour
{
    public string m_DatabaseFileName = "runnerDB.db";
    public string m_TableName = "memberTBL";
    private DatabaseAccess m_DatabaseAccess;
    //public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetData();
        }
    }

    public void GetData()
    {
        string filePath = Path.Combine(Application.dataPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);
        SqliteDataReader rdr = m_DatabaseAccess.ExecuteQuery("select * from memberTBL where memberID ='Thomas' and memberPW = 'thomas123';");

        string temp = string.Empty;
        if (rdr == null) temp = "No return";
        else
        {
            Debug.Log("select..!");
            while (rdr.Read())
            {
                for(int i = 0; i < rdr.FieldCount; i++)
                {
                    if (i != rdr.FieldCount - 1)
                        temp += rdr[i] + ";"; // parser 넣어주기
                    else if (i == rdr.FieldCount - 1)
                        temp += rdr[i] + "\n";
                }
            }
        }
        //scoreText.text = temp;
        m_DatabaseAccess.CloseSqlConnection();
    }

    public bool LogIn(string id, string pass)
    {
        string filePath = Path.Combine(Application.dataPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);

        SqliteDataReader rdr = m_DatabaseAccess.ExecuteQuery(
            "select * from memberTBL where memberID='" + id + "' and memberPW='" + pass + "';");
;       string temp = string.Empty;
        if (rdr == null)
        {
            temp = "No return";
            m_DatabaseAccess.CloseSqlConnection();
            return false;
        }
        else
        {
            m_DatabaseAccess.CloseSqlConnection();
            return true;
        }
    }

    public ArrayList SelectCharacters(string id)
    {
        ArrayList tempList = new ArrayList();

        string filePath = Path.Combine(Application.dataPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);

        SqliteDataReader rdr = m_DatabaseAccess.ExecuteQuery(
            "SELECT characterName FROM memberTBL INNER JOIN characterTBL ON memberTBL.memberID = characterTBL.memberID WHERE memberTBL.memberID = '" + id + "';");

        string temp = string.Empty;
        if (rdr == null) temp = "No return";
        else
        {
            Debug.Log("select..!");
            while (rdr.Read())
            {
                for(int i = 0; i < rdr.FieldCount; i++)
                {
                    Debug.Log(rdr[i]);
                    tempList.Add(rdr[i]);
                }
            }
        }

        m_DatabaseAccess.CloseSqlConnection();
        return tempList;
    }
}
