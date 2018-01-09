using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitch : MonoBehaviour {

    private bool myTurn = true;

	public void ChangeTurn()
    {
        myTurn = !myTurn;
        if(myTurn)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
