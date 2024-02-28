using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "ScriptableObjects/Zombie", order = 1)]
public class Zombie : ScriptableObject
{
    public GameObject zombieGameObject;
    public float health;
    public float speed;
}
