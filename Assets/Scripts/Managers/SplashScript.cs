using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;
    public GameObject lastScore;
    public Text lastScoreText;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(field);
        if (!PlayerPrefs.HasKey("totalScore"))
        {
            PlayerPrefs.SetInt("totalScore", 10000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        totalScoreText.text = PlayerPrefs.GetString("totalName") + "-" + PlayerPrefs.GetInt("totalScore");
        if (!PlayerPrefs.HasKey("lastScore"))
        {
            lastScore.SetActive(false);
        }
        else
        {
            lastScore.SetActive(true);
            lastScoreText.text = PlayerPrefs.GetString("lastName") + "-" + PlayerPrefs.GetInt("lastScore");
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete)) && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("delete saves");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("totalScore", 10000);
            PlayerPrefs.SetString("totalName", "chipenstain");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
