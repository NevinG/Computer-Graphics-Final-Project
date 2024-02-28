using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlants : MonoBehaviour
{
    #region
    public static AllPlants instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion
    public List<Plant> allPlants;
}
