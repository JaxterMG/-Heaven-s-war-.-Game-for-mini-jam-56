using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MH_Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float _speed;
    private Animator _anim;
    private SpriteRenderer _sRend;
    public float health;
    private Animator _camera;
    public int _gold=10;
    public static MH_Movement PlayerInstance;
    public Image _hpImg;
    public float _maxHealth;
    Vector2 move;
    public TextMeshProUGUI _coinText;
    private AudioSource _source;
    public AudioClip[] _clips;
    public AudioSource _coinSource;
    private MobileJoystick _mJContr;

    // Start is called before the first frame update
    void Start()
    {
        _mJContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileJoystick>();
        _source = GetComponent<AudioSource>();
        _coinText.text = "x" + _gold;
        _sRend = GetComponent<SpriteRenderer>();
         PlayerInstance = this;
        _camera = Camera.main.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _hpImg.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float _moveX = _mJContr.Horizontal();
        float _moveY = _mJContr.Vertical();
        move = new Vector2(_moveX, _moveY);

        _rb.velocity = move*_speed;
        if(_rb.velocity == Vector2.zero)
        {
            _anim.SetInteger("States", 0);
        }
        else
        {
            _anim.SetInteger("States", 1);
        }
    }
    public void UpdateDoughnutHealth(float Health)
    {
        if (health < _maxHealth)
        {
            health += Health;
            _hpImg.fillAmount = health / _maxHealth;
        }
    }
    public void UpdateHealth(float Health)
    {
        _hpImg.fillAmount = health / _maxHealth;
    }
    public void GetDamage(float damage)
    {
        _source.clip = _clips[0];
        _source.Play();
        health -= damage;
        _hpImg.fillAmount -= health / _maxHealth;
        _camera.SetTrigger("Shake");
        if (health <= 0)
        {
            _hpImg.fillAmount = 0;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            _rb.isKinematic = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            _rb.velocity = Vector2.zero;
            _anim.SetTrigger("Death");
             FindObjectOfType<MenuManager>().PlayerDeath();
            //  var part = Instantiate(_blood, gameObject.transform);
            //  Destroy(part, 2);
            

        }
    }
    public void Resurrect()
    {
        _hpImg.fillAmount = 1;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        _rb.isKinematic = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        _anim.SetTrigger("Resurrect");
        FindObjectOfType<MenuManager>().Revive();

    }

    public void PlayStep()
    {
        _source.clip = _clips[2];
        _source.Play();
    }
    public void UpdateCoins(int _coin)
    {
        _coinSource.Play();
        _source.Play();
        _gold += _coin;
        _coinText.text = "x" + _gold;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Attack"))
        { 
            Debug.Log("Damaged");
            GetDamage(collision.GetComponent<SlimeAttack>()._damage);
        }
    }
    public void Flip(bool _bool)
    {
        _sRend.flipX = _bool;
    }
}
