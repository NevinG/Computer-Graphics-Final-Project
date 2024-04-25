using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : PlantShooter
{
    bool shot1 = false;
    void Update()
    {
        zombie = CheckIfZombie();
        //check for zombie
        if (zombie)
        {
            timer += Time.deltaTime;
            if (timer >= timerBeforeShoot && zombie)
            {
                if(!shot1)
                {
                    ShootProjectile();
                }
                shot1 = true;
                if(timer >= (timerBeforeShoot + .1f))
                {
                    ShootProjectile();
                    timer = 0;
                    shot1 = false;
                }
                
            }
        }
    }
}
