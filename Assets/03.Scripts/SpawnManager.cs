using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> MobPool = new List<GameObject>();
    public GameObject[] Mobs;
    public int objCnt = 1;

    private void Awake()
    {
        for(int i = 0; i < Mobs.Length; i++)
        {
            for(int j = 0; j < objCnt; j++)
            {
                MobPool.Add(CreateObj(Mobs[i], transform));
            }
        }   
    }

    private void Start()
    {
        GameManager.instance.onPlay += PlayGame;
    }

    void PlayGame(bool isplay)
    {
        if (isplay)
        {
            for(int i = 0; i < MobPool.Count; i++)
            {
                if (MobPool[i].activeSelf)
                    MobPool[i].SetActive(false);
            }
            StartCoroutine(CreateMob());
        }
        else
            StopAllCoroutines();
    }

    IEnumerator CreateMob()
    {
        yield return new WaitForSeconds(0.5f);
        while (GameManager.instance.isPlay)
        {
            MobPool[DeactiveMob()].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }   
    }

    int DeactiveMob()
    {
        List<int> num = new List<int>();
        for(int i = 0; i < MobPool.Count; i++)
        {
            if (!MobPool[i].activeSelf)
                num.Add(i);
        }
        int x = 0;
        if (num.Count > 0)
            x = num[Random.Range(0, num.Count)];
        return x;
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}
