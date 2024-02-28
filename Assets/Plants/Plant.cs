using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plant", order = 3)]
public class Plant : ScriptableObject
{
    public GameObject plantGameObject;
    public Sprite sprite;
    public int cost;
    public float cooldown;
}

