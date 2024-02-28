using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public Zombie zombie;

    public void SpawnZombie()
    {
        GameObject z = Instantiate(zombie.zombieGameObject, transform.position, Quaternion.identity);
        GameHandler.instance.zombiePos.Add(z);
        z.GetComponent<ZombieStats>().zombie = zombie;

        Destroy(gameObject);
    }
}
