using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text totalScore;
    public Text score;
    public Text speed;
    public Text level;
    public Image[] lifes;
    public Slider lifeBar;
    public Slider enemyLifeBar;
    public Text ballCountText;

#if UNITY_ANDROID //ads timer button
    [SerializeField]  private Slider getExtraLifeTimer;
#endif

    public GameObject ballImage;

    public static float time;
    private float controlTime = 2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time <= controlTime)
        {
            level.gameObject.SetActive(true);
        }
        else level.gameObject.SetActive(false);
        StartCoroutine(ChangeLifeBar());
        StartCoroutine(ChangeEnemyLifeBar());
        StartCoroutine(ChangeAdsTimerBar());
    }

    public IEnumerator ChangeLifeBar()
    {
        yield return new WaitForSeconds(0.5f);
        float newValue = 1 - GameManager.Instance.HP;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            lifeBar.value = Mathf.Lerp(lifeBar.value, newValue, time);
            yield return null;
        }
        lifeBar.value = newValue;
    }
    public IEnumerator ChangeEnemyLifeBar()
    {
        yield return new WaitForSeconds(0.5f);
        float newValue = enemyLifeBar.maxValue - GameManager.Instance.enemyHP;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            enemyLifeBar.value = Mathf.Lerp(enemyLifeBar.value, newValue, time);
            yield return null;
        }
        enemyLifeBar.value = newValue;
    }

    public void ChangeBallCount(string count)
    {
        ballCountText.text = count;
    }

#if UNITY_ANDROID
    public IEnumerator ChangeAdsTimerBar()
    {
        yield return new WaitForSeconds(0.5f);
        float newValue = GameManager.Instance.getExtraLifeTimer;
        for (float time = 0.0f; time <= 1; time += 0.5f * Time.deltaTime)
        {
            getExtraLifeTimer.value = Mathf.Lerp(getExtraLifeTimer.value, newValue, time);
            yield return null;
        }
        getExtraLifeTimer.value = newValue;
    }
#endif
}