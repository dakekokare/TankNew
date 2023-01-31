using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    //�L�����o�X
    [SerializeField]
    private GameObject canvas;
    //�J���[�s�b�J�[
    private CUIColorPicker colorPicker;

    private void Start()
    {
        //�J���[�s�b�J�[
        colorPicker = canvas.transform.GetChild(3).GetChild(0).GetComponent<CUIColorPicker>();
    }

    public void OnClick()
    {
        Debug.Log("option click");
        //�^�C�g�����S�A�X�^�[�g�A�I�v�V�����{�^�����A�N�e�B�u
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        //�p�l���A�J���[�s�b�J�[�A�߂�{�^���A�N�e�B�u
        canvas.transform.GetChild(3).gameObject.SetActive(true);

    }
    public void OnBackClick()
    {
        Debug.Log("option back click");
        //�^�C�g�����S�A�X�^�[�g�A�I�v�V�����{�^�����A�N�e�B�u
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        //�p�l���A�J���[�s�b�J�[�A�߂�{�^����A�N�e�B�u
        canvas.transform.GetChild(3).gameObject.SetActive(false);

        //�F�����V�[�����L�I�u�W�F�N�g�ɓn��
        SceneShare.SetColor(colorPicker.Color);
    }
}
