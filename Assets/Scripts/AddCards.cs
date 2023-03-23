using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{
[SerializeField]
private Transform CardGrid;

[SerializeField]
private GameObject Card;


  //called upon starting program
  void Awake(){
    //creates 4x4 grid of buttons.
    for(int i =0; i<16;i++)
    {
        GameObject gameCard = Instantiate(Card);
        gameCard.name = "" + i;
        gameCard.transform.SetParent(CardGrid, false);
    }

  }
}
