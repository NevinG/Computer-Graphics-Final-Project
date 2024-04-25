using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravePlacer : MonoBehaviour
{
    public GameObject grave;
    List<Transform> places = new List<Transform>();
    public int numberOfGraves = 0;

    List<Grave> graves = new List<Grave>();

    private void Start()
    {
        Transform[] t = transform.Find("Row 1").GetComponentsInChildren<Transform>();
        Transform[] t1 = transform.Find("Row 2").GetComponentsInChildren<Transform>();
        Transform[] t2 = transform.Find("Row 3").GetComponentsInChildren<Transform>();
        Transform[] t3 = transform.Find("Row 4").GetComponentsInChildren<Transform>();
        Transform[] t4 = transform.Find("Row 5").GetComponentsInChildren<Transform>();

        for(int i = 6; i < t.Length - 1; i++)
        {
            places.Add(t[i]);
        }
        for (int i = 6; i < t.Length - 1; i++)
        {
            places.Add(t1[i]);
        }
        for (int i = 6; i < t.Length - 1; i++)
        {
            places.Add(t2[i]);
        }
        for (int i = 6; i < t.Length - 1; i++)
        {
            places.Add(t3[i]);
        }
        for (int i = 6; i < t.Length - 1; i++)
        {
            places.Add(t4[i]);
        }

        for (int i = 0; i < numberOfGraves; i++)
        {
            int randomNum = Random.Range(0, places.Count);
            while(places[randomNum].GetComponentsInChildren<Transform>().Length > 1)
            {
                randomNum = Random.Range(1, places.Count);
            }
            GameObject g = Instantiate(grave, places[randomNum].position, Quaternion.identity, places[randomNum]);
            graves.Add(g.GetComponent<Grave>());
        }
    }

    public void SpawnZombies()
    {
        foreach(Grave gr in graves)
        {
            gr.SpawnZombie();
        }
    }
}
