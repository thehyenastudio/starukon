using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;

    void Awake()
    {
        Helper.Set2DCameraToObject(field);
        if (!PlayerPrefs.HasKey("totalScore"))
        {
            PlayerPrefs.SetInt("totalScore", 1);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        totalScoreText.text = PlayerPrefs.GetInt("totalScore") + "-" + PlayerPrefs.GetString("totalName");
    }

    void Update()
    {
        if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete)) && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("delete saves");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("totalScore", 1);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
