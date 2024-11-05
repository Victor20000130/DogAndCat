using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{
    internal List<Unit> units = new List<Unit>();
    internal List<Unit> enemise = new List<Unit>();
    internal List<Unit> p_unit = new List<Unit>();
    internal Player player;
    internal EnemySpawner enemySpawner;
    internal PlayerSpawner playerSpawner;
    internal UIManager uiManager;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        SceneManager.sceneLoaded += (x, y) =>
        {
            uiManager = FindAnyObjectByType<UIManager>();
            player = FindAnyObjectByType<Player>();
            enemySpawner = FindAnyObjectByType<EnemySpawner>();
            playerSpawner = FindAnyObjectByType<PlayerSpawner>();
        };
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GameOver()
    {

        for (int i = 0; i < uiManager.battleResult.Length; i++)
        {
            uiManager.battleResult[i].SetActive(false);
        }
        units.Clear();
        enemise.Clear();
        p_unit.Clear();
        SceneManager.LoadScene("GameStartScene");

    }
}