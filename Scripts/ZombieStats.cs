using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [HideInInspector] public Zombie zombie;
    bool atPlant = false;
    [HideInInspector] public float health;
    float timer = 0;
    float timer2 = 0;
    bool flashingRed = false;
    float redLength = .15f;


    float waitTimer = 0;
    float waitTime;
    PlantHealth atThisPlant;

    bool started = false;

    SpriteRenderer[] sprites;

    float startingHealth;

    public bool vault = false;
    public bool newspaper = false;

    float speedMult = 1;
    float frozenMult = 1;

    float timer3 = 0;
    float freezeTime;
    bool frozen = false;

    private void Start()
    {
        waitTime = Random.Range(0, 300) / 100.0f;
        health = zombie.health;
        sprites = GetComponentsInChildren<SpriteRenderer>();
        startingHealth = health;
    }
    private void Update()
    {
        waitTimer += Time.deltaTime;
        if(waitTimer > waitTime)
        {
            started = true;
        }
        if(started)
        {
            atPlant = CheckIfAtPlant();
            if (!atPlant)
            {
                transform.position = new Vector3(transform.position.x - (zombie.speed * Time.deltaTime * speedMult * frozenMult), transform.position.y, transform.position.z);
                if (GetComponent<Animator>() != null && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {
                    GetComponent<Animator>().Play("Walk");
                    if(newspaper && health < 270)
                    {
                        GetComponent<Animator>().Play("run");
                    }
                }
            }
            else
            {
                if(vault)
                {
                    Vector3 pos = transform.position;
                    pos.x -= 1.1f;
                    transform.position = pos;
                    vault = false;
                    speedMult = .5f;
                }
                timer2 += (Time.deltaTime * frozenMult * speedMult);
                if (timer2 >= 1f)
                {
                    timer2 = 0;
                    atThisPlant.RemoveHealth(1);
                    if (GetComponent<Animator>() != null && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                    {
                        GetComponent<Animator>().Play("Attack1");
                    }
                }
            }

            if (flashingRed)
            {
                timer += Time.deltaTime;
                if (timer >= redLength)
                {
                    foreach (SpriteRenderer sr in sprites)
                    {
                        sr.color = Color.white;
                    }
                    flashingRed = false;
                    timer = 0;
                }
            }
            else
            {
                if(frozen)
                {
                    foreach (SpriteRenderer sr in sprites)
                    {
                        sr.color = Color.blue;
                    }
                }
                else
                {
                    foreach (SpriteRenderer sr in sprites)
                    {
                        sr.color = Color.white;
                    }
                }
            }

            //check if zombie ate brains
            if(transform.position.x <= -3.2)
            {
                GameHandler.instance.dead = true;
                GameHandler.instance.deadUI.SetActive(true);
                Destroy(gameObject);
            }

            //freeze check
            if(frozen)
            {
                frozenMult = .5f;
                timer3 += Time.deltaTime;
                if(timer3 > freezeTime)
                {
                    frozen = false;
                    timer3 = 0;
                }
            }
            else
            {
                frozenMult = 1;
            }

            if(newspaper)
            {
                if(health <= 270)
                {
                    speedMult = 3f;
                    if (GetComponent<Animator>() != null && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("eat"))
                    {
                        GetComponent<Animator>().Play("run");
                    }
                }
            }
        }
        
    }

    private bool CheckIfAtPlant()
    {
        bool plant = false;
        foreach(GameObject g in GameHandler.instance.plantPos)
        {
            if(Mathf.Abs(g.transform.position.z - transform.position.z) <= .02f &&  transform.position.x - g.transform.position.x < .25f && transform.position.x - g.transform.position.x > 0)
            {
                plant = true;
                atThisPlant = g.GetComponent<PlantHealth>();
            }
        }
        return plant;
    }

    public void DamageZombie(float damage)
    {
        if(started)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            //make flash red
            flashingRed = true;
            timer = 0;

            foreach (SpriteRenderer sr in sprites)
            {
                sr.color = Color.red;
            }
            ZombieRemoveAtHealth zh = GetComponentInChildren<ZombieRemoveAtHealth>();
            if(zh != null)
            {
                zh.UpdateLimbs();
            }
        }
    }

    public void Freeze(float t)
    {
        frozen = true;
        freezeTime = t;
        timer3 = 0;
    }
}
