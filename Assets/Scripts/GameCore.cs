using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private PlayerCore playerCore;
    [SerializeField] private EnemyManager enemyManager;
    
    public void StartGame()
    {
        GameData.Score = 0;
        Cursor.visible = false;
        playerCore.OnGameStart();   
        enemyManager.OnStartGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}