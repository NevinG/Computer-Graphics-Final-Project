using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlantShooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpot;
    public float timerBeforeShoot;
    protected float timer;
    public float range = 20;
    Animator animator;

    int layerMask;
    protected bool zombie = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Zombie");
    }
    private void Update()
    {
        zombie = CheckIfZombie();
        //check for zombie
        if(zombie)
        {
            timer += Time.deltaTime;
            if (timer >= timerBeforeShoot && zombie)
            {
                timer = 0;
                ShootProjectile();
            }
        }

    }

    public void ShootProjectile()
    {
        animator.Play("shoot");
        GameObject proj = Instantiate(projectile, projectileSpot.position, Quaternion.identity);
        proj.GetComponent<Projecticle>().rowPos = transform.position.y;
    }

    public bool CheckIfZombie()
    {
        bool zombie = false;
        foreach(GameObject g in GameHandler.instance.zombiePos)
        {
            Vector2 pos = (Vector2)g.transform.position;
            if(pos.y == transform.position.y && Vector2.Distance(transform.position,pos) > 0 && Vector2.Distance(transform.position, pos) < range)
            {
                zombie = true;
            }
        }
        return zombie;
    }

}
