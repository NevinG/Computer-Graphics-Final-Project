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
        if(animator && animator.HasState(0, Animator.StringToHash("shoot"))) {
            animator.Play("shoot");
        }
        GameObject proj = Instantiate(projectile, projectileSpot.position, Quaternion.identity);
        proj.GetComponent<Projecticle>().rowPos = transform.position.z;
    }

    public bool CheckIfZombie()
    {
        bool zombie = false;
        foreach(GameObject g in GameHandler.instance.zombiePos)
        {
            Vector3 pos = g.transform.position;
            if(Mathf.Abs(pos.z - transform.position.z) < .02 && Vector3.Distance(transform.position,pos) > 0 && Vector3.Distance(transform.position, pos) < range)
            {
                zombie = true;
            }
        }
        return zombie;
    }

}
