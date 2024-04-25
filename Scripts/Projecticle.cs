using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecticle : MonoBehaviour
{
    public float speed;
    public float damage;
    public float rowPos;

    public bool frozen;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        CheckHit();

        if (transform.position.x >= 20)
        {
            Destroy(gameObject);
        }
    }

    public void CheckHit()
    {
        foreach (GameObject g in GameHandler.instance.zombiePos)
        {
            Vector3 pos = g.transform.position; 
            if (Mathf.Abs(rowPos - pos.z) <= .02 && pos.x - transform.position.x <= .1f && pos.x - transform.position.x >= 0)
            {
                g.GetComponentInChildren<ZombieStats>().DamageZombie(damage);
                if(frozen)
                {
                    g.GetComponentInChildren<ZombieStats>().Freeze(5);
                }
                Destroy(gameObject);
            }
        }
    }
}
