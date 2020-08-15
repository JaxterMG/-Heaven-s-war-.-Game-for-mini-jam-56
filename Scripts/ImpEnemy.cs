using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpEnemy : EnemyScript
{

    public Transform _shotPoint;
    public GameObject _impBullet;
    private Transform playerTransform;
    public float _bulletSpeed;
    new bool move = true;



    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _blood = gameObject.GetComponentInChildren<ParticleSystem>();
        Player = GameObject.Find("Player");
        _anim = gameObject.GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();
      
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
   new void FixedUpdate()
    {
        if (move && !_death)
        {

            _timeBetweenAttack -= Time.deltaTime;
            _step = _speed * Time.deltaTime;
            Vector2 flip = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).normalized;

            if (flip.x < 0 && _facingRight)
            {

                _spr.flipX = true;
                _facingRight = !_facingRight;
            }
            else if (flip.x > 0 && !_facingRight)
            {

                _spr.flipX = false;
                _facingRight = !_facingRight;
            }
            _distance = (Player.transform.position - gameObject.transform.position).magnitude;
            if (_distance > _attackLenght)
            {

                gameObject.transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, _step);
            }
            else if (_distance <= _attackLenght && _timeBetweenAttack <= 0)
            {
                StartCoroutine("Attack");
                
            }
        }
        else return;

    }

  IEnumerator Attack()
    {
        move = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        _anim.SetTrigger("Attack");
        float plrY = playerTransform.position.y;
        float plrX = playerTransform.position.x;
        Vector2 aimDir = new Vector2(plrX - transform.position.x, plrY - transform.position.y).normalized;
        float _angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Vector3 rot = new Vector3(0, 0, _angle - 90);
        GameObject bullet = Instantiate(_impBullet, transform.position, Quaternion.Euler(rot));
        bullet.GetComponent<Rigidbody2D>().velocity = aimDir * _bulletSpeed;
       

        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        _timeBetweenAttack = 0.5f;
        move = true;



    }

    IEnumerator Death()
    {
        if (health <= 0)
        {
            _death = true;
            move = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            int _rand = Random.Range(0, 4);
            if (_rand > 2)
            {

                GameObject _coin = Instantiate(_coinPref, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                _coin.GetComponent<CoinPickup>()._value = _enemyValue;
            }
            else
            {
                _rand = Random.Range(0, 4);
                if (_rand > 2)
                {

                    GameObject _doughnut = Instantiate(_doughPref, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    _doughnut.GetComponent<dougnutDrop>()._value = 1;
                }
            }
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            _anim.SetTrigger("Death");
            FindObjectOfType<GameHandler>()._enemyWave -= 1;
            yield return new WaitForSeconds(1);
            Instantiate(_deathStain, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, GameObject.Find("TrashHolder").transform); ;
            yield return new WaitForSeconds(2);
            Destroy(gameObject);


        }
    }
  
}
