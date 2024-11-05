using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject pausePanel;
    public GameObject playerPanel;
    public Slider playerHpBar;
    public Slider enemyHpBar;
    public GameObject coinPanel;
    public TextMeshProUGUI coinText;
    public Button levelUpButton;
    public TextMeshProUGUI levelupButtonText;
    public Button extraSkillButton;
    public TextMeshProUGUI extraSkillButtonText;
    public Button[] spawnButtons;
    public GameObject[] grayPanel;
    public GameObject[] battleResult;
    private bool isPaused = false;
    private void Awake()
    {
        //levelupButtonText = levelUpButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        //extraSkillButtonText = extraSkillButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        //coinText = coinPanel.transform.GetComponentInChildren<TMP_Text>();

    }
    private void Start()
    {
        pausePanel.SetActive(false);
        battleResult[0].SetActive(false);
        battleResult[1].SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
    public void SpawnEnDisAble()
    {
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            if (GameManager.Instance.player.coins < GameManager.Instance.playerSpawner.unitStats[i].unitCost)
            {
                spawnButtons[i].interactable = false;
                grayPanel[i].SetActive(true);
            }
            else
            {
                spawnButtons[i].interactable = true;
                grayPanel[i].SetActive(false);
            }
        }
        if (GameManager.Instance.player.coins < GameManager.Instance.player.levelUpCost || GameManager.Instance.player.maxLv == GameManager.Instance.player.level)
        {
            levelUpButton.interactable = false;
            levelupButtonText.color = Color.gray;
        }
        else
        {
            levelUpButton.interactable = true;
            levelupButtonText.color = Color.white;
        }
        if (GameManager.Instance.player.extraSkillColldown <= 0)
        {
            extraSkillButton.interactable = true;
            extraSkillButtonText.color = Color.white;
        }
        else
        {
            extraSkillButton.interactable = false;
            extraSkillButtonText.color = Color.gray;
        }
    }
    public void OnRestart()
    {
        Start();
    }
    private void Reset()
    {
        mainCanvas = GetComponent<Canvas>();
        pausePanel = transform.Find("PausePanel")?.gameObject;
        playerPanel = transform.Find("PlayerPanel")?.gameObject;
        coinPanel = transform.Find("CoinPanel")?.gameObject;
    }

}
