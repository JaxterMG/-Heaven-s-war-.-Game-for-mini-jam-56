using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dougnutDrop : MonoBehaviour
{
    public int _value;
    private void Start()
    { 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MH_Movement>())
        {
            collision.gameObject.GetComponent<MH_Movement>().UpdateDoughnutHealth(_value);
            Destroy(gameObject);
        }
    }


}
