using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public SpriteRenderer[] tiles;
    public Sprite groundImg;
    public float speed;

    SpriteRenderer temp;

    void Start()
    {
        tiles = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
        temp = tiles[0];
    }

    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (-7 >= tiles[i].transform.position.x)
                {
                    for (int j = 0; j < tiles.Length; j++)
                    {
                        if (temp.transform.position.x < tiles[j].transform.position.x)
                            temp = tiles[j];
                    }
                    tiles[i].transform.position = new Vector2(temp.transform.position.x + 0.88f, -0.89f);
                    tiles[i].sprite = groundImg;
                }
            }
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * GameManager.instance.gameSpeed);
            }
        }      
    }
}
