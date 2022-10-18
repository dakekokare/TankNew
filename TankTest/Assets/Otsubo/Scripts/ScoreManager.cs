using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UIを使う場合には忘れずに追加すること！
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private Text scoreLabel;

    void Start()
    {
        scoreLabel = GetComponent<Text>();
        scoreLabel.text = "SCORE：" + score;
    }

    // スコアを増加させるメソッド
    // （ポイント）外部からアクセスするためpublicで定義すること
    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "SCORE：" + score;
    }
}