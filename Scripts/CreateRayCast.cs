using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRayCast : MonoBehaviour
{
    private CardToMousePosition moveActions;
    private CardsInHand cardsHand;
    private ManaAmount manaAmount;
    private CardData cardData;
    private BattlefieldCardList battlefieldList;
    private Attack attack;
    private int cardMask;
    private int playfieldMask;
    private int defaultMask;
    private float maxRayDistance = 200f;

    void Start()
    {
        manaAmount = FindObjectOfType<ManaAmount>();
        cardsHand = FindObjectOfType<CardsInHand>();
        battlefieldList = FindObjectOfType<BattlefieldCardList>();
        cardMask = LayerMask.GetMask("Cards");
        playfieldMask = LayerMask.GetMask("Playfield");
        defaultMask = ~cardMask;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(Physics.Raycast(ray, out hit, maxRayDistance, defaultMask)) //everything else but not the cards
        {
            if(Input.GetMouseButtonDown(0) && hit.collider.tag == "EndTurnButton")
            {
                hit.collider.GetComponent<TurnSwitch>().ChangeTurn();
            }
            if(Input.GetMouseButtonUp(0) && hit.collider.name != "FightField")
            {
                if(moveActions != null && cardsHand.ContainsCard(moveActions.gameObject))
                {
                    moveActions.gameObject.transform.position = cardsHand.getCardPositions(moveActions.gameObject.name);
                    moveActions.gameObject.transform.eulerAngles = cardsHand.getCardRotations(moveActions.gameObject.name);
                }
            }
        }

        if(Physics.Raycast(ray, out hit, maxRayDistance, cardMask)) //selecting the card
        {
            if(Input.GetMouseButtonDown(0) && hit.collider.tag == "Card")
            {
                moveActions = hit.collider.GetComponent<CardToMousePosition>();
                cardData = hit.collider.GetComponent<CardData>();
                if (hit.collider.GetComponent<Attack>() != null) {
                    attack = hit.collider.GetComponent<Attack>();
                } else
                {
                    attack = null;
                }
            }
        }

        if(Physics.Raycast(ray, out hit, maxRayDistance, playfieldMask)) //move the card
        {
            if(moveActions != null && cardData != null)
            {
                if(cardData.GetManaCost() <= manaAmount.GetCurrentMana())
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        moveActions.FollowChange();
                    }
                    else if(Input.GetMouseButtonUp(0))
                    {
                        moveActions.FollowChange();
                        if(cardsHand.removeCard(moveActions.gameObject))
                        {
                            battlefieldList.AddToArray(moveActions.gameObject);
                        }
                        else
                        {
                            attack.CheckHit();
                            battlefieldList.CardOrderner();
                        }
                        moveActions = null;
                        cardData = null;
                    }
                    else if(Input.GetMouseButton(0))
                    {
                        moveActions.ChangePosition(hit);
                    }
                }
            }
        }
    }
}
