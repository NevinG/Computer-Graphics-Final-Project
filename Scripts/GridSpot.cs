using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpot : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(GameHandler.instance.placing == true)
        {
            GameHandler.instance.placeSpot = transform;
        }
    }
    private void OnMouseExit()
    {
        GameHandler.instance.placeSpot = null;
    }
}
