using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameras : MonoBehaviour
{
    // �؂�ւ��ΏۃJ����
    [SerializeField] private Camera[] _cameras;

    // �N���X�t�F�[�h���o��\�����邽�߂�RawImage
    [SerializeField] private RawImage _crossFadeImage;

    // �t�F�[�h����
    [SerializeField] private float _fadeDuration = 1;

    private RenderTexture _renderTexture;
    private int _currentIndex;
    private Coroutine _fadeCoroutine;

    //���݂̃J�����ԍ�
    private int NowCamera = 0;

    // �w�肳�ꂽ�p�����[�^�͗L�����ǂ���
    private bool IsValid => _cameras.Length >= 2 && _crossFadeImage != null;

    // �t�F�[�h�����ǂ���
    private bool IsChanging => _fadeCoroutine != null;

    // ������
    private void Awake()
    {
        if (!IsValid) return;

        // �N���X�t�F�[�h�p��RenderTexture�쐬
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 10);

        // RawImage������
        _crossFadeImage.texture = _renderTexture;
        _crossFadeImage.gameObject.SetActive(false);

        // �J����������
        for (var i = 0; i < _cameras.Length; ++i)
        {
            // �ŏ��̃J���������L���ɂ���
            _cameras[i].enabled = i == _currentIndex;
        }
    }

    // �L�[���̓`�F�b�N�E�J�����ύX
    private void Update()
    {
        //if (!IsValid || IsChanging || !Input.anyKeyDown) return;

        //// �����L�[���̓`�F�b�N
        //if (!int.TryParse(Input.inputString, out var cameraNo))
        //    return;

        //// �����ꂽ�J�����ԍ��ɑΉ������J�����ɐ؂�ւ�
        //ChangeCamera(cameraNo - 1);
    }

    // �w�肳�ꂽ�C���f�b�N�X�̃J�����ɃN���X�t�F�[�h���Ȃ���؂�ւ���
    public void ChangeCamera(int index)
    {
        if (!IsValid || IsChanging)
            return;

        if (index < 0 || index >= _cameras.Length)
            return;

        if (index == _currentIndex)
            return;

        // �N���X�t�F�[�h���o�J�n
        _fadeCoroutine = StartCoroutine(CrossFadeCoroutine(index));
    }

    // �N���X�t�F�[�h���o�R���[�`��
    private IEnumerator CrossFadeCoroutine(int index)
    {
        // �t�F�[�h�p��RawImage�\��
        _crossFadeImage.gameObject.SetActive(true);

        // �t�F�[�h���̂݁A�؂�ւ���J�����̕`����RenderTexture�ɐݒ�
        var nextCamera = _cameras[index];
        nextCamera.enabled = true;
        nextCamera.targetTexture = _renderTexture;

        // RawImage�̃��l�����X�ɕύX�i�t�F�[�h�j
        var startTime = Time.time;

        while (true)
        {
            var time = Time.time - startTime;
            if (time > _fadeDuration)
                break;

            var alpha = time / _fadeDuration;
            _crossFadeImage.color = new Color(1, 1, 1, alpha);

            yield return null;
        }

        // �؂�ւ���J������L����
        nextCamera.targetTexture = null;
        _cameras[_currentIndex].enabled = false;

        // �t�F�[�h�p��RawImage��\��
        _crossFadeImage.gameObject.SetActive(false);

        _currentIndex = index;
        _fadeCoroutine = null;
    }

    public void Change()
    {
        NowCamera++;

        if (NowCamera >= _cameras.Length)
            NowCamera = 0;

        ChangeCamera(NowCamera);
    }
}