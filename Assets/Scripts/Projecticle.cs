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
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
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
            Vector2 pos = (Vector2)g.transform.position;
            if (rowPos == pos.y && pos.x - transform.position.x <= .1f && pos.x - transform.position.x >= 0)
            {
                g.GetComponent<ZombieStats>().DamageZombie(damage);
                if(frozen)
                {
                    g.GetComponent<ZombieStats>().Freeze(5);
                }
                Destroy(gameObject);
            }
        }
    }
}
