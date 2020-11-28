using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;
    public GameObject lastScore;
    public Text lastScoreText;
#if UNITY_ANDROID
    public GameObject AndroidInput;
    public Text AndroidInputText;
#endif

    private void Awake()
    {
        Cursor.visible = false;
#if UNITY_ANDROID
        field.GetComponent<Transform>().localScale = new Vector3(0.5f, 1f, 1f);
#endif
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
#if UNITY_ANDROID
        AndroidInput.SetActive(true);
        if (!PlayerPrefs.HasKey("AndroidInput"))
        {
            PlayerPrefs.SetString("AndroidInput", "ACCELEROMETER");
        }
        AndroidInputText.text = PlayerPrefs.GetString("AndroidInput");
#endif
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
#endif
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

#if UNITY_ANDROID

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeAndroidInput()
    {
        if (PlayerPrefs.GetString("AndroidInput") == "TAP")
        {
            PlayerPrefs.SetString("AndroidInput", "ACCELEROMETER");
        }
        else PlayerPrefs.SetString("AndroidInput", "TAP");
        AndroidInputText.text = PlayerPrefs.GetString("AndroidInput");
    }
#endif
}
