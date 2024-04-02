using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMaultiplier;

    private bool shouldCount = true;
    private float score = 0;

    private void Update()
    {
        if(!shouldCount)
            return;
        score += Time.deltaTime * scoreMaultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString("000");
    }

    public int EndTimer()
    {
        shouldCount = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(score);
    }
}