using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoeMine : MonoBehaviour
{
    bool grown = false;
    float timer = 0;
    float growthTime = 15;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > growthTime && !grown)
        {
            grown = true;
            Vector3 pos = transform.position;
            pos.y += .25f;
            transform.position = pos;
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
            Vector2 pos = new Vector2(g.transform.position.x, g.transform.position.z);
            if (transform.position.z == pos.y && pos.x - transform.position.x < .25f && pos.x - transform.position.x >= -.1)
            {
                Destroy(g);
                Destroy(gameObject);
            }
        }
    }
}
