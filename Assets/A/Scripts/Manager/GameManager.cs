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
    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
        if (enemySpawner == null)
        {
            enemySpawner = FindAnyObjectByType<EnemySpawner>();
        }
        if (playerSpawner == null)
        {
            playerSpawner = FindAnyObjectByType<PlayerSpawner>();
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GameOver()
    {
        units.Clear();
        enemise.Clear();
        p_unit.Clear();
        SceneManager.LoadScene("GameStartScene");
    }
}