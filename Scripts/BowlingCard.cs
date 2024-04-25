using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCard : ConveyorCard
{
    private void OnMouseUp()
    {
        if (GameHandler.instance.placeSpot != null && GameHandler.instance.OpenSpot() && placedPlant.transform.position.x < -4.84f)
        {
            GameHandler.instance.placePlant(placedPlant.transform.position = GameHandler.instance.placeSpot.position, plant);
            Destroy(gameObject);
        }
        Destroy(placedPlant);
        GameHandler.instance.placing = false;
    }
}
