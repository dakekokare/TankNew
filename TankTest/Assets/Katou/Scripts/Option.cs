using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    //キャンバス
    [SerializeField]
    private GameObject canvas;
    //カラーピッカー
    private CUIColorPicker colorPicker;

    private void Start()
    {
        //カラーピッカー
        colorPicker = canvas.transform.GetChild(3).GetChild(0).GetComponent<CUIColorPicker>();
    }

    public void OnClick()
    {
        Debug.Log("option click");
        //タイトルロゴ、スタート、オプションボタンを非アクティブ
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        //パネル、カラーピッカー、戻るボタンアクティブ
        canvas.transform.GetChild(3).gameObject.SetActive(true);

    }
    public void OnBackClick()
    {
        Debug.Log("option back click");
        //タイトルロゴ、スタート、オプションボタンをアクティブ
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        //パネル、カラーピッカー、戻るボタン非アクティブ
        canvas.transform.GetChild(3).gameObject.SetActive(false);

        //色情報をシーン共有オブジェクトに渡す
        SceneShare.SetColor(colorPicker.Color);
    }
}
