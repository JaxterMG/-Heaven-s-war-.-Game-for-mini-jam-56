
using System.Runtime.InteropServices;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Store _item;
    private SpriteRenderer _spriteImage;
    // Start is called before the first frame update
    public Canvas _storeCanv;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _description;
    public TextMeshProUGUI _cost;
    bool bought = false;
    private mobilePurchaseButton _puchaseButton;


    void Start()
    {
        _puchaseButton = GameObject.FindGameObjectWithTag("PurchaseButton").GetComponent<mobilePurchaseButton>();
        _spriteImage = GetComponent<SpriteRenderer>();
    }

    public void UpdateCell(Store _givenItem)
    {
        _item = _givenItem;
        _spriteImage.sprite = _item.Icon;
        bought = false;

    }
    private void BuyItem()
    {
        bought = true;

        if (_item.Weapon)
        {

            for (int i = 0; i < MH_Movement.PlayerInstance.transform.GetChild(0).transform.childCount; i++)
            {
                Destroy(MH_Movement.PlayerInstance.transform.GetChild(0).transform.GetChild(i).gameObject);
            }
            Instantiate(_item.ItemGO, MH_Movement.PlayerInstance.gameObject.transform);

        }
        if (_item.SpeedBoost)
        {
            MH_Movement.PlayerInstance._speed += 0.3f;
        }
        if (_item.HpBoost)
        {
            MH_Movement.PlayerInstance.health = MH_Movement.PlayerInstance._maxHealth;
            MH_Movement.PlayerInstance.UpdateHealth(MH_Movement.PlayerInstance.health);
        }
        if (_item.PierceBoost)
        {
            FindObjectOfType<Weapon>().ChangePiercing(1);
        }
        if (_item.DamageBoost)
        {
            FindObjectOfType<Weapon>().ChangeDamage(0.5f);
        }

        _spriteImage.sprite = null;
        MH_Movement.PlayerInstance.UpdateCoins(-_item._goldCost);

        _name.gameObject.SetActive(false);
        _description.gameObject.SetActive(false);
        _cost.gameObject.SetActive(false);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>() && !bought)
        {
            _name.gameObject.SetActive(true);

            _name.text = "Item: " + _item._itemName;
            _description.text = "Description: " + _item.Description;
            _description.gameObject.SetActive(true);
            _cost.gameObject.SetActive(true);
            _cost.text = "Price: " + _item._goldCost.ToString();


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>())
        {
            _name.gameObject.SetActive(false);
            _description.gameObject.SetActive(false);
            _cost.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>() && (Input.GetKeyDown(KeyCode.E) || _puchaseButton._pressed))
        {
            if (!bought)
            {

                if (MH_Movement.PlayerInstance._gold >= _item._goldCost)
                {
                    BuyItem();
                }
            }
        }
    }
}
