using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieWave", menuName = "ScriptableObjects/ZombieWave", order = 2)]
public class ZombieWave : ScriptableObject
{
    public float waitTimeBeforeNextWavel;
    public Zombie[] zombies;
}
