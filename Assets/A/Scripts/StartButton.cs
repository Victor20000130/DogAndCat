using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button startButton;
    private void Awake()
    {
        startButton = GetComponent<Button>();
    }
    private void Start()
    {
        startButton.onClick.AddListener(GameManager.Instance.GameStart);
    }
}
