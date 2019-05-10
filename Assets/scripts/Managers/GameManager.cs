using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //gameplay's variable
    public int life = 3;
    public int totalScore = 1000000;
    public int score = 0;
    public int speed = 4;
    public float playerSpeed = 1f;
    public bool ready = false;
    public List<GameObject> enemys;

    public AudioSource audioSourceScore;

    public GameObject playerObj;        // a player
    public GameObject ballObj;

    public GameObject field;

    //objects variables
    public GameObject player;           // player prefab
    public GameObject enemy;            // enemy prefab
    public GameObject[] Texts;            // reddy\gameover prefab
    public Transform PlayerStartPoint;
    public Transform EnemyStartPoint;
    public Transform PlayerEndPoint;
    public Transform EnemyEndPoint;
    public Transform TextStartPoint;

    public bool newScore = false;

    //sky's variable
    public GameObject[] clouds;
    public Transform StartSkyPoint;
    public Transform EndSkyPoint;

    private float TimeCloud;
    public float controlTimeCloud = 5f;

    //bubble's variable
    public GameObject bubble;
    public Transform StartBubblePoint;
    public Transform EndBubblePoint;

    private float TimeBubble;
    public float controlTimeBubble = 7f;

    private float TimeSpeed;
    public float controlTimeSpeed = 100f;

    public enum Bonuses {upSpeed, downSpeed, upSize, downSize, controlBall, bigBall, upScore, bomb};
    public bool controlBall = false;

    private void Awake()
    {
        Instance = this;
        Helper.Set2DCameraToObject(field);
        Instance.totalScore = PlayerPrefs.GetInt("totalScore");
    }

    void Start()
    {
        PrepareGame();
    }

    void Update()
    {
        if (Instance.speed < 13)
        {
            TimeSpeed += Time.deltaTime;
            if (TimeSpeed >= controlTimeSpeed)
            {
                SetSpeed(1);
                TimeSpeed = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        BackhroundAnim();
    }

    void PrepareGame()
    {
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;

        var enemyObj = Instantiate(enemy, EnemyStartPoint);
        enemyObj.GetComponent<StartObj>().EndPoint = EnemyEndPoint;
        UIManager.Instance.totalScore.text = Instance.totalScore.ToString();
    }

    void BackhroundAnim()
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

    public static void PlayerDie()
    {
        Instance.playerObj.GetComponent<PlayerScript>().StartDie();
        Instance.life--;
        if (Instance.life >= 0)
        {
            Destroy(UIManager.Instance.lifes[Instance.life], 1f);
            Instance.Restart();
        }
        else Instance.GameOver();
    }

    public void Restart()
    {
        Instance.ready = true;
        Instantiate(Texts[0], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;
    }

    public void SetSpeed(int speed)
    {
        Instance.speed += speed;
        UIManager.Instance.speed.text = (Instance.speed - 3).ToString();
    }

    public void SetScore(int score)
    {
        Instance.score += score;
        UIManager.Instance.score.text = Instance.score.ToString();

        if (Instance.score > Instance.totalScore)
        {
            newRecord();
        }
    }

    public void newRecord()
    {
        if (!newScore)
        {
            audioSourceScore.Play();
            newScore = true;
        }
        Instance.totalScore = Instance.score;
        UIManager.Instance.totalScore.text = Instance.totalScore.ToString();
        PlayerPrefs.SetInt("totalScore", Instance.totalScore);
    }

    public void GameOver()
    {
        var text = Instantiate(Texts[1], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
    }

    public void GetBonus (int bonus)
    {
        Debug.Log(bonus);
        switch (bonus)
        {
            case (int)Bonuses.upSpeed:
                if (Instance.playerSpeed < 2f) Instance.playerSpeed += 0.2f;
                else Instance.playerSpeed = 2f;
                break;
            case (int)Bonuses.downSpeed:
                if (Instance.playerSpeed > 0.4f) Instance.playerSpeed -= 0.6f;
                else Instance.playerSpeed = 0.4f;
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
                ballObj.GetComponent<BallScript>().SetSprite();
                break;
            case (int)Bonuses.upScore:
                Instance.SetScore(5000);
                break;
            case (int)Bonuses.bomb:
                foreach(GameObject enem in enemys)
                {
                    Destroy(enem);
                }
                enemys.Clear();
                break;
        }
    }
}
