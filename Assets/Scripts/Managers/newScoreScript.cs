using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newScoreScript : MonoBehaviour
{
    public GameObject field;
    public Text totalScoreText;
    public InputField totalNameText;

    private void Awake()
    {
        Cursor.visible = false;
        Helper.Set2DCameraToObject(field);
        totalScoreText.text = PlayerPrefs.GetInt("totalScore").ToString();
    }

    public void OnOK()
    {
        PlayerPrefs.SetString("totalName", totalNameText.text);
        SceneManager.LoadScene(0);
    }
}
