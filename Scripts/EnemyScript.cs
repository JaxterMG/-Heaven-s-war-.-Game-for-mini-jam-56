using System.Collections;


using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    public Animator _anim;
    public float health;
    public ParticleSystem _blood;
    protected GameObject Player;
   protected float _distance;
   public float _speed;
   protected float _step;
    public float _attackLenght;
   protected bool move = false;
    public GameObject _slimeAttackGO;
    Animator _slimeAttack;
   protected float _timeBetweenAttack = 1f;
    public GameObject _deathStain;
   public Rigidbody2D rb;
    public SpriteRenderer _spr;
    protected bool _facingRight = true;
   protected bool _death = false;
    public int _enemyValue;
    public GameObject _coinPref;
    public GameObject _doughPref;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _slimeAttack = _slimeAttackGO.GetComponent<Animator>();
        _blood = gameObject.GetComponentInChildren<ParticleSystem>();
        Player = GameObject.Find("Player");
        _anim = gameObject.GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();

    }
   protected void FixedUpdate()
    {
        if (move&&!_death)
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
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        _slimeAttack.SetTrigger("Attack");
        move = false;
        yield return new WaitForSeconds(1.5f);
        rb.isKinematic = false;
        move = true;
        _timeBetweenAttack = 1f;


    }
   public void GetDamage(float damage)
    {
        _blood.Play();
        rb.velocity = Vector2.zero;
        health -= damage;
        StartCoroutine("Death");
    }
    IEnumerator Death()
    {
        if (health <= 0)
        {
            int _rand = Random.Range(0, 4);
            if (_rand>2)
            {
                
                GameObject _coin = Instantiate(_coinPref, new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
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
                
            _death = true;
            move = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

           
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            
            _anim.SetTrigger("Death");

            Destroy(_slimeAttackGO);
            FindObjectOfType<GameHandler>()._enemyWave -= 1;
            Instantiate(_deathStain, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity,GameObject.Find("TrashHolder").transform);
            yield return new WaitForSeconds(2);
            Destroy(gameObject);


        }
    }
    public void SetMove()
    {
        move = true;
    }
}
