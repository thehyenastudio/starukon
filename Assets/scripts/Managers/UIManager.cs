using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text totalScore;
    public Text score;
    public Text speed;
    public Image[] lifes;
    public Slider lifeBar;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ChangeLifeBar(float value)
    {
        float newValue = lifeBar.value + value;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            lifeBar.value = Mathf.Lerp(lifeBar.value, newValue, time);
            yield return null;
        }
        lifeBar.value = newValue;
    }

    public IEnumerator ChangeLifeBar()
    {
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            lifeBar.value = Mathf.Lerp(lifeBar.value, 0, time);
            yield return null;
        }
        lifeBar.value = 0f;
    }
}
