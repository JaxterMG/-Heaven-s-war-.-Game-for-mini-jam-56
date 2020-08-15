using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMana : MonoBehaviour
{
    public GameObject[] _clouds;
    public float _defaultTime;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = _defaultTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Instantiate(_clouds[Random.Range(0, _clouds.Length)], new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-4.7f, 4.7f), 0), Quaternion.identity,gameObject.transform);
            _timer = _defaultTime;
        }
    }
}
