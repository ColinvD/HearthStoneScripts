using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    [SerializeField]
    private GameObject damagePopup;
    [SerializeField]
    private SpriteRenderer healthSprite;
    [SerializeField]
    private Sprite health29Sprite;
    [SerializeField]
    private Health enemyHealth;
    private BattlefieldCardList battlefieldCardList;
    private CardData data;
    private int damage;
    private float maxRayDistance = 200f;

    // Use this for initialization
    void Start () {
        data = GetComponent<CardData>();
        battlefieldCardList = FindObjectOfType<BattlefieldCardList>();
        damage = data.GetDamage();
	}

    public void CheckHit()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            if (hit.collider.tag == "Enemy" && hit.collider.GetComponent<Health>() != null)
            {
                enemyHealth = hit.collider.GetComponent<Health>();
                if (data.GetAttacked() == false)
                {
                    enemyHealth.LoseHealth(data.GetDamage());
                    data.ChangeAttacked();
                    Instantiate(damagePopup, hit.transform);
                    healthSprite.sprite = health29Sprite;
                }
                enemyHealth = null;
                battlefieldCardList.CardOrderner();
            }
        }
    }
}
