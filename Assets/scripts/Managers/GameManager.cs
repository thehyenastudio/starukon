using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //gameplay's variable
    public int life = 3;
    public int totalScore = 1000000;
    public int score = 0;
    public int speed = 1;

    public AudioSource audioSourceScore;

    public GameObject playerObj;        // a player

    public GameObject field;

    //objects variables
    public GameObject player;           // player prefab
    public GameObject enemy;            // enemy prefab
    public Transform PlayerStartPoint;
    public Transform EnemyStartPoint;
    public Transform PlayerEndPoint;
    public Transform EnemyEndPoint;

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

    private void Awake()
    {
        Instance = this;
        Helper.Set2DCameraToObject(field);
    }

    void Start()
    {
        PrepareGame();
    }

    void Update()
    {
        BackhroundAnim();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Application.Quit();
        }
    }

    void PrepareGame()
    {
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;

        var enemyObj = Instantiate(enemy, EnemyStartPoint);
        enemyObj.GetComponent<StartObj>().EndPoint = EnemyEndPoint;
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
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;
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
        audioSourceScore.Play();
        Instance.totalScore = Instance.score;
    }

    public void GameOver()
    {

    }
}
