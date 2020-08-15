using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int _value;
    private AudioSource _source;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>())
        {
            collision.gameObject.GetComponent<MH_Movement>().UpdateCoins(_value);
            Destroy(gameObject);
        }
    }

    

       
}
