using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public float _damage;
    BoxCollider2D _attackzone;
    // Start is called before the first frame update
    void Start()
    {
        _attackzone = gameObject.GetComponent<BoxCollider2D>();
    }

    public void ActivateCollider()
    {
        _attackzone.enabled = true;
    }
    public void DeActivateCollider()
    {
        _attackzone.enabled = false;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer.Equals("Player"))
    //    {
    //        Debug.Log("Damage");
    //        collision.gameObject.GetComponent<MH_Movement>().GetDamage(_damage);
    //    }

    //}


}
