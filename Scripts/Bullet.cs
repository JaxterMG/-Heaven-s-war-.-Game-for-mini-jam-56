using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float _speed;
    Vector3 _direction;
    public float _damage;
    public int _piercing=1;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * _speed;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<dougnutDrop>())
            return;
        if (collision.gameObject.GetComponent<CoinPickup>())
            return;
        if (collision.gameObject.GetComponent<EnemyScript>())
        {
            collision.gameObject.GetComponent<EnemyScript>().GetDamage(_damage);
            _piercing--;
            if (_piercing <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
