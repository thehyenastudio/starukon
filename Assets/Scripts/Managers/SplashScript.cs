using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(field);
        if (!PlayerPrefs.HasKey("totalScore"))
        {
            PlayerPrefs.SetInt("totalScore", 1000000000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        totalScoreText.text = PlayerPrefs.GetInt("totalScore") + "-" + PlayerPrefs.GetString("totalName");
    }

    private void Update()
    {
        if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete)) && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("delete saves");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("totalScore", 1000000000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
