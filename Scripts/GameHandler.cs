using System.Collections;

using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject[] _enemyPrefs;
    public int _enemyWave = 6;
    public int _wavesPassed;
    public int _shopingWave;
    public float _timeForShopping;
    public Animator _angelAnim;
    public StoreHolder _store;
    bool shopping =false;
    private int _maxEnemies=3;
    private int _minEnemies = 1;
    bool _alreadyUpdated = false;
    public TextMeshProUGUI _waveText;
    

    // Start is called before the first frame update
    void Start()
    {

        UpdateWave();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_enemyWave <= 0 && !_alreadyUpdated)
        {
            _alreadyUpdated = true;
            UpdateWave();
        }

    }
    private void UpdateWave()
    {
        _wavesPassed += 1;
        if (_wavesPassed % _shopingWave == 0)
        {

            shopping = true;

        }

        if (shopping)
        {
            StartCoroutine("Shopping");
        }
        else
        {
            _waveText.text = "Wave: "+_wavesPassed;
            _enemyWave = Random.Range(_minEnemies, _maxEnemies);


            for (int i = 0; i < _enemyWave; i++)
            {

                Instantiate(_enemyPrefs[Random.Range(0, _enemyPrefs.Length)], new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f)), Quaternion.identity);

            }
            _alreadyUpdated = false;


        }
    }
    IEnumerator Shopping()
    {
        _waveText.text = "ShoppingTime";
        _store.UpdateStore();
        _angelAnim.SetBool("Down",true);
        yield return new WaitForSeconds(_timeForShopping);
        _angelAnim.SetBool("Down", false);
        yield return new WaitForSeconds(1.5f);
        shopping = false;
        _maxEnemies += 2;
        _minEnemies += 1;
        _alreadyUpdated = false;


    }
    
}
