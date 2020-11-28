using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject field;
    public GameObject playerObj;
    public List<GameObject> ballObj;
    public GameObject enemyObj;
    public enum Bonuses { upSpeed, downSpeed, upSize, downSize, controlBall, bigBall, upScore, bomb, twoBall, speedBall, lifer, bonusBall };

    public float HP = 1f;
    public float enemyHP = 1f;
    private int life = 3;
    public int speed = 4;
    public float playerSpeed = 1f;
    public float ballSpeed = 200f;

    public int ballCount = 20;

    public List<GameObject> enemys;
    public bool ready = false;
    public bool controlBall = false;

    public int level = 0;
    public int levelSmart = 0;

    public GameObject player;
    public GameObject[] enemy;
    public GameObject[] Texts;
    public GameObject[] clouds;
    public GameObject bubble;

    public Transform PlayerStartPoint;
    public Transform[] EnemyStartPoint;
    public Transform PlayerEndPoint;
    public Transform EnemyEndPoint;
    public Transform TextStartPoint;
    public Transform StartSkyPoint;
    public Transform EndSkyPoint;
    public Transform StartBubblePoint;
    public Transform EndBubblePoint;

    private float TimeCloud;
    public float controlTimeCloud = 5f;
    private float TimeBubble;
    public float controlTimeBubble = 7f;

    public bool isBall = true;

#if UNITY_ANDROID //extra life for ads
    [SerializeField] private GameObject getExtraLifeBtn;
    public float getExtraLifeTimer = 0f;
    private bool endLife = false;
    private bool secondChance = false;

    public bool isTap = false;
#endif

    private void Awake()
    {
        ADSManager.Init();
        Cursor.visible = false;
        Instance = this;
        Helper.Set2DCameraToObject(field);
#if UNITY_ANDROID
        if (PlayerPrefs.GetString("AndroidInput") == "TAP") isTap = true;
        else isTap = false;
#endif
    }

    private void Start()
    {
        PrepareGame();
    }

    private void Update()
    {
        if (Instance.enemys.Count > 0)
        {
            if (Instance.enemys[0] == null)
            {
                Instance.enemys.RemoveAt(0);
            }
        }
        if (enemyHP <= 0f) Win();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.F) && Instance.isBall == true)
        {
            OnFire();
        }
#endif
#if UNITY_ANDROID
        if (endLife && !secondChance)
        {
            Instance.getExtraLifeTimer += Time.deltaTime;
            if (Instance.getExtraLifeTimer >= 5f && !secondChance)
            {
                secondChance = true;
                Instance.GameOver();
            }
        }
#endif

        BackgroundAnim();
    }

    private void PrepareGame()
    {
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;

        enemyObj = Instantiate(enemy[levelSmart], EnemyStartPoint[levelSmart]);
        enemyObj.GetComponent<StartObj>().EndPoint = EnemyEndPoint;
    }

    private void BackgroundAnim()
    {
        TimeCloud += Time.deltaTime;
        if (TimeCloud >= controlTimeCloud)
        {
            Instantiate(clouds[Random.Range(0, 5)], new Vector3(StartSkyPoint.position.x, Random.Range(StartSkyPoint.position.y, EndSkyPoint.position.y), StartSkyPoint.position.z), Quaternion.identity);
            TimeCloud = 0;
        }

        TimeBubble += Time.deltaTime;
        if (TimeBubble >= controlTimeBubble)
        {
            Instantiate(bubble, new Vector3(Random.Range(StartBubblePoint.position.x, EndBubblePoint.position.x), StartBubblePoint.position.y, StartBubblePoint.position.z), Quaternion.identity);
            TimeBubble = 0;
        }
    }

    private void Win()
    {
        level++;
        levelSmart++;
        ScoreManager.Instance.SetScore(500 * level);
        if (levelSmart >= 4)
        {
            levelSmart = 0;
        }
        SetSpeed(1);
        UIManager.time = 0f;
        UIManager.Instance.level.text = "LEVEL " + (level + 1).ToString();
        Destroy(enemyObj);
        HP = 1f;
        UIManager.Instance.enemyLifeBar.maxValue = level * 2f;
        enemyHP = level * 2f;
        enemyObj = Instantiate(enemy[levelSmart], EnemyStartPoint[levelSmart]);
        enemyObj.GetComponent<StartObj>().EndPoint = EnemyEndPoint;
    }

    public static void PlayerDie()
    {
        Instance.playerObj.GetComponent<PlayerScript>().StartDie();
        Instance.life--;
        if (Instance.life >= 0)
        {
            Destroy(UIManager.Instance.lifes[Instance.life], 1f);
            Instance.Restart();
        }
        else
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            Instance.GameOver();
#endif
#if UNITY_ANDROID
            if (!Instance.secondChance)
            {
                Instance.endLife = true;
                Instance.getExtraLifeBtn.SetActive(true);
                Instance.getExtraLifeTimer = 0f;
            }
            else
            {
                Instance.GameOver();
            }
#endif
        }
    }

    private void Restart()
    {
        HP = 1f;
        ballCount = 20;
        StartCoroutine(UIManager.Instance.ChangeLifeBar());
        Instance.ready = true;
        Instantiate(Texts[0], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;
    }

    private void GameOver()
    {
        ScoreManager.Instance.SaveScore();
        Instance.getExtraLifeBtn.SetActive(false);
        var text = Instantiate(Texts[1], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
    }

    public void SetSpeed(int speed)
    {
        if (Instance.speed < 20)
        {
            Instance.speed += speed;
            Instance.ballSpeed += 31;
            UIManager.Instance.speed.text = (Instance.speed - 3).ToString();
        }
    }

    public void GetDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            for (int i = 0; i < ballObj.Count; i++)
            {
                Destroy(ballObj[i]);
            }
            ballObj.Clear();
            PlayerDie();
        }

    }

    public void GetBonus(int bonus)
    {
        switch (bonus)
        {
            case (int)Bonuses.upSpeed:
                if (Instance.playerSpeed < 3f) Instance.playerSpeed += 0.2f;
                else Instance.playerSpeed = 3f;
                break;
            case (int)Bonuses.downSpeed:
                if (Instance.playerSpeed > 0.8f) Instance.playerSpeed -= 0.4f;
                else Instance.playerSpeed = 0.8f;
                break;
            case (int)Bonuses.upSize:
                playerObj.GetComponent<PlayerScript>().SetSprite(1);
                break;
            case (int)Bonuses.downSize:
                playerObj.GetComponent<PlayerScript>().SetSprite(-1);
                break;
            case (int)Bonuses.controlBall:
                Instance.controlBall = !Instance.controlBall;
                break;
            case (int)Bonuses.bigBall:
                for (int i = 0; i < ballObj.Count; i++)
                {
                    if (ballObj[i].GetComponent<BallScript>())
                    {
                        ballObj[i].GetComponent<BallScript>().SetSprite();
                    }
                }
                break;
            case (int)Bonuses.upScore:
                ScoreManager.Instance.SetScore(500);
                break;
            case (int)Bonuses.bomb:
                for (int i = 0; i < enemys.Count; i++)
                {
                    if (enemys[i].GetComponent<EnemyBulletScript>())
                    {
                        enemys[i].GetComponent<EnemyBulletScript>().StartDie(false);
                    }
                }
                break;
            case (int)Bonuses.twoBall:
                if (playerObj.GetComponent<PlayerScript>().ball != null)
                {
                    Instance.ballObj.Add(Instantiate(playerObj.GetComponent<PlayerScript>().ball, new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 0.04f, 5), playerObj.transform.rotation));
                    Instance.isBall = false;
                    UIManager.Instance.ballImage.SetActive(false);
                }
                break;
            case (int)Bonuses.speedBall:
                ballSpeed += 100f;
                break;
            case (int)Bonuses.lifer:
                HP += 0.2f;
                if (HP > 1f) HP = 1f;
                break;
            case (int)Bonuses.bonusBall:
                Instance.ballCount += 10;
                break;
        }
    }

    public void OnGetExtraLifeBtn()
    {
        ADSManager.ShowADS("rewardedVideo");
        life++;
        secondChance = true;
        Restart();
        getExtraLifeBtn.SetActive(false);
    }

    public void OnFire()
    {
        if (Instance.isBall == true)
        {
            Instance.ballObj.Add(Instantiate(playerObj.GetComponent<PlayerScript>().ball, new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 0.04f, 5), playerObj.transform.rotation));
            Instance.isBall = false;
            UIManager.Instance.ballImage.SetActive(false);
        }
    }
}
