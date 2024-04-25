using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mower : MonoBehaviour
{
    float speed = 5;
    float damage = 10000;

    public bool on = false;
    private void Update()
    {
        if(CheckHit())
        {
            on = true;
        }
        if(on)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            CheckHit();
        }
        if(transform.position.x >= 20)
        {
            Destroy(gameObject);
        }
    }

    public bool CheckHit()
    {
        foreach (GameObject g in GameHandler.instance.zombiePos)
        {
            if(g != null)
            {
                Vector3 pos = g.transform.position;
                if (Mathf.Abs(transform.position.z - pos.z) <= .02f && pos.x - transform.position.x <= .1f)
                {
                    g.GetComponent<ZombieStats>().DamageZombie(damage);
                    return true;
                }
            }
        }
        return false;
    }
}
