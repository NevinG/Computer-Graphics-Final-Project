using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoeMine : MonoBehaviour
{
    public Sprite grownMine;

    bool grown = false;
    float timer = 0;
    float growthTime = 15;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > growthTime)
        {
            grown = true;
            GetComponent<SpriteRenderer>().sprite = grownMine;
        }

        if(grown)
        {
            CheckHit();
        }
    }

    public void CheckHit()
    {
        foreach (GameObject g in GameHandler.instance.zombiePos)
        {
            Vector2 pos = (Vector2)g.transform.position;
            if (transform.position.y == pos.y && pos.x - transform.position.x <= .55f && pos.x - transform.position.x >= 0)
            {
                Destroy(g);
                Destroy(gameObject);
            }
        }
    }
}
