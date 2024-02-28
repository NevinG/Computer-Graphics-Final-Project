using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : MonoBehaviour
{
    float chewTime = 42;
    float chewTimer = 0;
    bool chewing = false;

    private void Update()
    {
        if(!chewing)
        {
            GetComponent<Animator>().Play("idle");
            CheckHit();
        }
        else
        {
            GetComponent<Animator>().Play("chewing");
            chewTimer += Time.deltaTime;
            if(chewTimer > chewTime)
            {
                chewTimer = 0;
                chewing = false;
            }
        }
    }

    public void CheckHit()
    {
        foreach (GameObject g in GameHandler.instance.zombiePos)
        {
            Vector2 pos = (Vector2)g.transform.position;
            if (transform.position.y == pos.y && pos.x - transform.position.x <= 2.5f && pos.x - transform.position.x >= 0)
            {
                Destroy(g);
                chewing = true;
                return;
                
            }
        }
    }
}
