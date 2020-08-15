using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float _speed;
    Vector3 _direction;
    public float _damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>())
        {
            collision.gameObject.GetComponent<MH_Movement>().GetDamage(_damage);
            Destroy(gameObject);
        }
        if(collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    
    }


}


