using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform _gun;
    Transform _plTr;
    SpriteRenderer _sPlayer;
    public GameObject _bulletPrefab;
    public Transform _shootPoint;
    public Transform _hanldePoint;
    Vector3 mousePos;
    Vector3 _aimDirection;
    private Animator _camera;
    public float _timeBetweenShots;
    private float _defaultTime;
    SpriteRenderer _sRend;
    public float _gunDamage;
    public float _defaultgunDamage;
    public int _maxPierce = 4;
    public float _maxGunDamage=10;
    private AudioSource _source;
    private WeaponJoystick _mJContr;
    private LineRenderer _lineRenderer;
    public Vector2 _lazerHit;
    public LayerMask _layerMask;
   

    // Start is called before the first frame update
    void Start()
    {
        _mJContr = GameObject.FindGameObjectWithTag("WeaponJoystick").GetComponent<WeaponJoystick>();
        _source = GetComponent<AudioSource>();
        _gunDamage = _defaultgunDamage;
        _bulletPrefab.GetComponent<Bullet>()._damage = _gunDamage;
        _bulletPrefab.GetComponent<Bullet>()._piercing = 1;
        _sRend = GetComponent<SpriteRenderer>();
        _defaultTime = _timeBetweenShots;
        _camera = Camera.main.GetComponent<Animator>();
        _plTr = GameObject.Find("Player").transform;
        _gun = gameObject.transform;
        _sPlayer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        _timeBetweenShots -= Time.deltaTime;

    }
   
    // Update is called once per frame
    void Update()
    {
        mousePos = _mJContr.GetVec2();
        // _aimDirection = (mousePos - _gun.position).normalized;
        _aimDirection = mousePos;
        float _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        _gun.eulerAngles = new Vector3(0, 0, _angle);
        LaserUpdate();
        Vector3 _localScale = Vector3.one;
        Vector3 _playerScale = Vector3.one;
        if (_angle > 90 || _angle < -90)
        {
            _localScale.y = -1f;
            _sPlayer.flipX = true;


        }
        else
        {
            _localScale.y = +1f;
            _sPlayer.flipX = false;

        }
        _gun.localScale = _localScale;
        _plTr.localScale = _playerScale;
        if (_mJContr._shooting)
        {
            
            if (_timeBetweenShots <= 0)
            {
                Shoot();
                //_camera.SetTrigger("Shake");
                _timeBetweenShots = _defaultTime;
            }
        }
    }
    void LaserUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position,_shootPoint.position-_hanldePoint.position,Mathf.Infinity);
        
       // Debug.DrawLine(transform.position, _gun.eulerAngles,Color.red);
        _lazerHit = hit.point;
        _lineRenderer.SetPosition(0, _shootPoint.position);
        _lineRenderer.SetPosition(1, _lazerHit);

    }
    public void ChangePiercing(int _pierceNum)
    {
        if(_bulletPrefab.GetComponent<Bullet>()._piercing<=_maxPierce)
        {
            _bulletPrefab.GetComponent<Bullet>()._piercing += _pierceNum;
        }
    }
    public void ChangeDamage(float _damageNum)
    {
        if (_bulletPrefab.GetComponent<Bullet>()._damage <= _maxGunDamage)
        {
            _bulletPrefab.GetComponent<Bullet>()._damage += _damageNum;
        }
    }
    void Shoot()
    {
        _source.Play();
        var bullet = Instantiate(_bulletPrefab, new Vector3(_shootPoint.position.x, _shootPoint.position.y, _gun.position.z), Quaternion.Euler(_gun.eulerAngles));

    }

        

    
}
