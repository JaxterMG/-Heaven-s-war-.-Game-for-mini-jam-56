using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class StartMenuManager : MonoBehaviour
{
    //public TextMeshProUGUI _score;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    string path = Application.dataPath + "/Score.txt";
    //    if (!File.Exists(path))
    //    {
    //        File.WriteAllText(path, "0");
    //    }
    //    _score.text = "Your best score: " + File.ReadAllText(path);
    //}


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
