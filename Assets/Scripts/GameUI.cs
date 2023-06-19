using System;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UIWindow finishWindow;
    [SerializeField] private UIWindow startWindow;
    [SerializeField] private UIWindow inGameWindow;

    private void OnEnable()
    {
        PlayerCore.OnPlayerDead += OnPLayerDeath;
    }

    private void OnDisable()
    {
        PlayerCore.OnPlayerDead -= OnPLayerDeath;        
    }

    private void OnPLayerDeath()
    {
        inGameWindow.gameObject.SetActive(false);
        finishWindow.gameObject.SetActive(true);
        Cursor.visible = true;
    }
}