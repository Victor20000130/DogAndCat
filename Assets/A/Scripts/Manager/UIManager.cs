using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    public Canvas mainCanvas;
    public GameObject pausePanel;

    public GameObject playerPanel;

    public TMP_Text coinText;
    public TMP_Text levelUpText;
    public Button levelUpButton;
    private TextMeshProUGUI levelupButtonText;
    public Button extraSkillButton;
    private TextMeshProUGUI extraSkillButtonText;

    public Button[] spawnButtons;
    public GameObject[] grayPanel;

    public GameObject[] battleResult;

    private bool isPaused = false;


    protected override void Awake()
    {
        base.Awake();
        levelupButtonText = levelUpButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        extraSkillButtonText = extraSkillButton.transform.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Start()
    {
        pausePanel.SetActive(false);
        for (int i = 0; i < battleResult.Length; i++)
        {
            battleResult[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            Debug.Log(isPaused);
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
        pausePanel = transform.Find("PausePanel")?.gameObject;
    }

}
