using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int Score;
   [SerializeField] TMP_Text text;
    public void ChangeScore()
    {
        text.text = Score.ToString();
    }
    public void ResetScore()
    {
        Score = 0;
        ChangeScore();
    }
}
