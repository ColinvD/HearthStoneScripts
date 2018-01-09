using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAmount : MonoBehaviour {

    [SerializeField]
    private int currentMana = 1;

	public int GetCurrentMana()
    {
        return currentMana;
    }

    public void ChangeCurrentMana(int amount)
    {
        currentMana += amount;
    }
}
