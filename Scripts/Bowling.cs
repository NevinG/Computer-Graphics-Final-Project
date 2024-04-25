using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    float moveSpeed = 3;
    Vector2 dir;
    public bool explode = false;
    float radius = 3;

    void Start()
    {
        transform.parent = null;
        dir = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += dir * moveSpeed * Time.deltaTime;
        transform.position = pos;

        CheckHit();

        if (transform.position.x >= 20)
        {
            Destroy(gameObject);
        }
    }

    public void CheckHit()
    {
        if(!explode)
        {
            foreach (GameObject g in GameHandler.instance.zombiePos)
            {
                if (Mathf.Abs(Vector2.Distance(g.transform.position, transform.position)) <= .2f)
                {
                    g.GetComponent<ZombieStats>().DamageZombie(350);
                    float yRange = -.5f;
                    if (Random.Range(0, 2) == 1)
                    {
                        yRange = .5f;
                    }
                    dir = new Vector2(1, yRange);
                    dir.Normalize();
                }
            }
        }
        else
        {
            foreach (GameObject g in GameHandler.instance.zombiePos)
            {
                if (Mathf.Abs(Vector2.Distance(g.transform.position, transform.position)) <= .3f)
                {
                    //explode
                    foreach (GameObject go in GameHandler.instance.zombiePos)
                    {
                        if (Mathf.Abs(Vector2.Distance(go.transform.position, transform.position)) <= radius)
                        {
                            go.GetComponent<ZombieStats>().DamageZombie(1800);
                        }
                    }
                    Destroy(gameObject);
                }
            }
          
        }
       
    }
}
