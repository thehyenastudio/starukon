using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newScoreScript : MonoBehaviour
{
    public GameObject field;
    public Text ScoreText;
    public Text ScoreCount;
    public InputField NameText;
    private bool total = true;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(field);

        total = PlayerPrefs.GetInt("totalScore") > PlayerPrefs.GetInt("lastScore") ? true : false;

        if (total)
        {
            ScoreText.text = "YOUR NEW RECORD";
            ScoreCount.text = PlayerPrefs.GetInt("totalScore").ToString();
        }
        else
        {
            ScoreText.text = "YOUR RECORD";
            ScoreCount.text = PlayerPrefs.GetInt("lastScore").ToString();
        }
    }

    public void OnOK()
    {
        if (total)
        {
            PlayerPrefs.SetString("totalName", NameText.text);
        }
        else
        {
            PlayerPrefs.SetString("lastName", NameText.text);
        }
        SceneManager.LoadScene(0);
    }
}
