using TMPro;
using UnityEngine;

public class FinishScreen: UIWindow
{
    [SerializeField] private TMP_Text scoreText;
    private void OnEnable()
    {
        scoreText.text = $"Score: {GameData.Score.ToString()}";
    }
}