using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject field;
    public AudioSource audioSourceScore;
    public GameObject playerObj;
    public GameObject ballObj;
    public enum Bonuses { upSpeed, downSpeed, upSize, downSize, controlBall, bigBall, upScore, bomb };

    private int life = 3;
    private int totalScore = 1000000;
    private int score = 0;
    public int speed = 4;
    public float playerSpeed = 1f;
    public float ballSpeed = 200f;
    public List<GameObject> enemys;
    public bool ready = false;
    public bool newScore = false;
    public bool controlBall = false;

    public GameObject player;
    public GameObject enemy;
    public GameObject[] Texts;
    public GameObject[] clouds;
    public GameObject bubble;
    public Transform PlayerStartPoint;
    public Transform EnemyStartPoint;
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
    private float TimeSpeed;
    public float controlTimeSpeed = 100f;

    private void Awake()
    {
        Cursor.visible = false;
        Instance = this;
        Helper.Set2DCameraToObject(field);
        Instance.totalScore = PlayerPrefs.GetInt("totalScore");
    }

    private void Start()
    {
        PrepareGame();
    }

    private void Update()
    {
        if (Instance.speed < 13)
        {
            TimeSpeed += Time.deltaTime;
            if (TimeSpeed >= controlTimeSpeed)
            {
                SetSpeed(1);
                TimeSpeed = 0f;
                if (Instance.speed == 8 || Instance.speed == 13)
                {
                    ballSpeed += 100f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        BackhroundAnim();
    }

    private void PrepareGame()
    {
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;

        var enemyObj = Instantiate(enemy, EnemyStartPoint);
        enemyObj.GetComponent<StartObj>().EndPoint = EnemyEndPoint;
        UIManager.Instance.totalScore.text = Instance.totalScore.ToString();
    }

    private void BackhroundAnim()
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

    private void Restart()
    {
        Instance.ready = true;
        Instantiate(Texts[0], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;
    }

    private void GameOver()
    {
        var text = Instantiate(Texts[1], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
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

    public void GetBonus(int bonus)
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
                foreach (GameObject enem in enemys)
                {
                    Destroy(enem);
                }
                enemys.Clear();
                break;
        }
    }
}
