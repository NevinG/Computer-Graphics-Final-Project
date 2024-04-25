using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    [HideInInspector] public int index;
    public bool pickedCard;

    private void OnMouseUpAsButton()
    {
        if(pickedCard)
        {
            GameHandler.instance.DeselectedCard(index);
        }
        else
        {
            GameHandler.instance.SelectedCard(index);
        }
        
    }
}
