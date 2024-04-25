using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float timeBeforeExplode;

    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeBeforeExplode)
        {
            //explode
            foreach(GameObject g in GameHandler.instance.zombiePos)
            {
                Vector2 ZombiePos = new Vector2(g.transform.position.x, g.transform.position.z);
                Vector2 myPos = new Vector2(transform.position.x, transform.position.z);
                if(Mathf.Abs(Vector2.Distance(g.transform.position,transform.position)) <= radius)
                {
                    g.GetComponent<ZombieStats>().DamageZombie(1800);
                }
            }
            Destroy(gameObject);
        }
    }
}
