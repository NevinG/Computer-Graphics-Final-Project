using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedConveyor : MonoBehaviour
{
    public Plant[] possibleSeeds;
    float waitTime;
    float timer;

    float xpos = -6.259f;
    float offset = 1.218f;

    public GameObject cardPrefab;

    List<ConveyorCard> cardsOnTrack = new List<ConveyorCard>();

    public int lowerTime = 7;
    public int upperTime = 10;

    private void Start()
    {
        waitTime = Random.Range(lowerTime, upperTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            timer = 0;
            AddSeed();
            waitTime = Random.Range(7, 10);
        }

        for (int i = 0; i < cardsOnTrack.Count; i++)
        {
            if(cardsOnTrack[i] == null)
            {
                cardsOnTrack.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < cardsOnTrack.Count; i++)
        {
            if (cardsOnTrack[i].transform.position.x > (xpos + offset * i))
            {
                cardsOnTrack[i].canMoveLeft = true;
            }
            else
            {
                cardsOnTrack[i].canMoveLeft = false;
            }
        }
    }

    public void AddSeed()
    {
        //randomly get a seed
        Plant plant = possibleSeeds[Random.Range(0, possibleSeeds.Length)];
        GameObject go = Instantiate(cardPrefab, transform.TransformPoint(new Vector3(6.367f,0,0)),Quaternion.identity,transform);
        go.GetComponent<ConveyorCard>().plant = plant;
        cardsOnTrack.Add(go.GetComponent<ConveyorCard>());
    }
}
