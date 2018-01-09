using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToMousePosition : MonoBehaviour
{
    private bool follow = false;
    private Transform cardPosition;
    
	void Start()
    {
        cardPosition = transform;
	}
	
    public void ChangePosition(RaycastHit hit)
    {
        if(follow)
        {
            Vector3 holder = hit.point;
            holder.y += 3;
            cardPosition.position = holder;
        }
    }

    public void FollowChange()
    {
        follow = !follow;
    }
}
