using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class newScoreScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;
    public InputField totalNameText;

    void Awake()
    {
        Helper.Set2DCameraToObject(field);
        totalScoreText.text = PlayerPrefs.GetInt("totalScore").ToString();
    }

    public void OnOK()
    {
        PlayerPrefs.SetString("totalName", totalNameText.text);
        SceneManager.LoadScene(0);
    }
}
