using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartEndManager : MonoBehaviour
{


    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }

}
