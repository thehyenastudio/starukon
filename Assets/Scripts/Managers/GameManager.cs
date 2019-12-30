using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject field;
    public GameObject playerObj;
    public GameObject ballObj;
    public GameObject enemyObj;
    public enum Bonuses { upSpeed, downSpeed, upSize, downSize, controlBall, bigBall, upScore, bomb };

    private float HP = 1f;
    public float enemyHP = 1f;
    private int life = 3;
    public int speed = 4;
    public float playerSpeed = 1f;
    public float ballSpeed = 200f;

    public List<GameObject> enemys;
    public bool ready = false;
    public bool controlBall = false;

    public int level = 0;

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
    private float TimeDay;
    private float controlTimeDay = 150f;

    private void Awake()
    {
        Cursor.visible = false;
        Instance = this;
        Helper.Set2DCameraToObject(field);
    }

    private void Start()
    {
        PrepareGame();
    }

    private void Update()
    {
        if (enemyHP <= 0f) Win();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
        {
            GetBonus(5);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            GetBonus(8);
        }

        BackgroundAnim();
    }

    private void PrepareGame()
    {
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;

        enemyObj = Instantiate(enemy[level], EnemyStartPoint[level]);
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

        TimeDay += Time.deltaTime;
        if (TimeDay >= controlTimeDay)
        {
            //todo
            TimeDay = 0;
        }
    }

    private void Win()
    {
        level++;
        SetSpeed(1);
        UIManager.time = 0f;
        UIManager.Instance.level.text = "LEVEL " + (level + 1).ToString();
        Destroy(enemyObj);
        HP = 1f;
        UIManager.Instance.enemyLifeBar.maxValue = level * 2f;
        enemyHP = level * 2f;
        StartCoroutine(UIManager.Instance.ChangeLifeBar());
        StartCoroutine(UIManager.Instance.ChangeEnemyLifeBar());
        enemyObj = Instantiate(enemy[level], EnemyStartPoint[level]);
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
        else Instance.GameOver();
    }

    private void Restart()
    {
        HP = 1f;
        StartCoroutine(UIManager.Instance.ChangeLifeBar());
        Instance.ready = true;
        Instantiate(Texts[0], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
        playerObj = Instantiate(player, PlayerStartPoint);
        playerObj.GetComponent<StartObj>().EndPoint = PlayerEndPoint;
    }

    private void GameOver()
    {
        ScoreManager.Instance.SaveScore();
        var text = Instantiate(Texts[1], new Vector3(TextStartPoint.position.x, TextStartPoint.position.y, TextStartPoint.position.z), Quaternion.identity);
    }

    public void SetSpeed(int speed)
    {
        Instance.speed += speed;
        UIManager.Instance.speed.text = (Instance.speed - 3).ToString();
    }

    public void GetDamage(float dmg)
    {
        HP -= dmg;
        StartCoroutine(UIManager.Instance.ChangeLifeBar(dmg));
        if (HP <= 0)
        {
            Destroy(ballObj);
            PlayerDie();
        }

    }

    public void GetBonus(int bonus)
    {
        Debug.Log("Give bonus #"+bonus.ToString());
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
                ScoreManager.Instance.SetScore(10000);
                break;
            case (int)Bonuses.bomb:
                foreach (GameObject enem in enemys)
                {
                    enem.GetComponent<EnemyBulletScript>().StartDie(true);
                }
                break;
        }
    }
}
