using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoving : MonoBehaviour
{
    float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _speed = Random.Range(1,3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right*_speed * Time.deltaTime);
        Destroy(gameObject, 20);
    }
}
