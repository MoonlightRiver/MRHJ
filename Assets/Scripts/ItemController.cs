using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Rspeed, Sspeed, Rpower, Mspeed, JumpM, JumpCD, MaxHpUp };

public class ItemController : MonoBehaviour
{

    public GameObject ItemPrefab;
    public Sprite CurrentSprite;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;
    public Sprite Sprite7;
    public Sprite Sprite8;
    private SpriteRenderer spriteRenderer;
    
    public float HealRate;
public float RspeedRate;
public float SspeedRate;
public float RpowerRate;
public float MspeedRate;
public float JumpMRate;
public float JumpCDRate;
public float MaxHpUpRate;

    public ItemType Type { get; private set; }

    void Start()
    {
        ItemPrefab = GameObject.FindWithTag("Item");
        spriteRenderer = ItemPrefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CurrentSprite;
        
        Type = SlotMachine();
        MatchImage(Type, spriteRenderer);
        Debug.Log("Got Item : " + Type.ToString());        
    }


    ItemType SlotMachine()
    {
        float ItemSlot = Random.Range(0f, 100f);
        if (ItemSlot <= HealRate)
        {
            return ItemType.Heal;
        }
        else if (ItemSlot <= HealRate + RspeedRate)
        {
            return ItemType.Rspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate)
        {
            return ItemType.Sspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate)
        {
            return ItemType.Rpower;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate)
        {
            return ItemType.Mspeed;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate + JumpMRate)
        {
            return ItemType.JumpM;
        }
        else if (ItemSlot <= HealRate + RspeedRate + SspeedRate + RpowerRate + MspeedRate + JumpMRate + JumpCDRate)
        {
            return ItemType.JumpCD;
        }
        else
        {
            return ItemType.MaxHpUp;
        }
    }

    void MatchImage(ItemType Type, SpriteRenderer spriteRenderer)
    {
        switch (Type)
        {
            case ItemType.Heal:
                spriteRenderer.sprite = Sprite1;
                break;
            case ItemType.Rspeed:
                spriteRenderer.sprite = Sprite2;
                break;
            case ItemType.Sspeed:
                spriteRenderer.sprite = Sprite3;
                break;
            case ItemType.Rpower:
                spriteRenderer.sprite = Sprite4;
                break;
            case ItemType.Mspeed:
                spriteRenderer.sprite = Sprite5;
                break;
            case ItemType.JumpM:
                spriteRenderer.sprite = Sprite6;
                break;
            case ItemType.JumpCD:
                spriteRenderer.sprite = Sprite7;
                break;
            case ItemType.MaxHpUp:
                spriteRenderer.sprite = Sprite8;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
