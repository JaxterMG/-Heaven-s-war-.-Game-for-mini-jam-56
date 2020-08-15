using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject _resume;
    public GameObject _reviveAd;
    public GameObject _menu;
    bool _isPressed= false;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPressed)
            {
                
                _menu.SetActive(true);
                _reviveAd.SetActive(false);
                Time.timeScale = 0f;

            }
            else
            {
                _menu.SetActive(false);
                Time.timeScale = 1f;
            }
            _isPressed = !_isPressed;
        }
    }
    public void PlayerDeath()
    {
        int score = FindObjectOfType<GameHandler>()._wavesPassed;
        string path = Application.dataPath + "/Score.txt";
      
        
            //int prevRes = int.Parse(File.ReadAllText(path));
            //if(score>prevRes)
            //{
            //    File.WriteAllText(path, score.ToString());
            //}
        
        _resume.SetActive(false); 
        StartCoroutine("Death");
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        _menu.SetActive(true);
        _reviveAd.SetActive(true);
        Time.timeScale = 0f;
        _isPressed = !_isPressed;

    }
    public void Revive()
    {
        _menu.SetActive(false);
        _reviveAd.SetActive(false);
        Time.timeScale = 1;
        _isPressed = !_isPressed;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Resume()
    {
      
        _menu.SetActive(false);
        Time.timeScale = 1f;
        _isPressed = !_isPressed;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
