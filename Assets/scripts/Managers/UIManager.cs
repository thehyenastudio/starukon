using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text totalScore;
    public Text score;
    public Text speed;

    public Image[] lifes;

    private void Awake()
    {
        Instance = this;
    }
}
