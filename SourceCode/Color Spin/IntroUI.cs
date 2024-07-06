using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    // ���� �� �̸�
    public string mainSceneName = "Main_Scene";

    public GameObject optionPanel;
    public Slider backgroundSlider;
    public Slider effectsSlider;

    public Text topScoreText1;
    public Text topTimeText1;
    public Text topScoreText2;
    public Text topTimeText2;
    public Text topScoreText3;
    public Text topTimeText3;



    void Start()
    {
        optionPanel.SetActive(false);

        // �����̴� �ʱ�ȭ
        backgroundSlider.value = PlayerPrefs.GetFloat("BackgroundVolume", 1.0f);
        effectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1.0f);

        // �����̴� ���� �̺�Ʈ ����
        backgroundSlider.onValueChanged.AddListener(SetBackgroundVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        // �ְ� ��� �ҷ�����
        int topScore1 = PlayerPrefs.GetInt("TopScore0", 0);
        float topTime1 = PlayerPrefs.GetFloat("TopTime0", 0);
        int topScore2 = PlayerPrefs.GetInt("TopScore1", 0);
        float topTime2 = PlayerPrefs.GetFloat("TopTime1", 0);
        int topScore3 = PlayerPrefs.GetInt("TopScore2", 0);
        float topTime3 = PlayerPrefs.GetFloat("TopTime2", 0);

        // �ؽ�Ʈ ������Ʈ
        topScoreText1.text = "1st Score: " + topScore1;
        topTimeText1.text = "Time: " + Mathf.FloorToInt(topTime1 / 60F) + ":" + Mathf.FloorToInt(topTime1 % 60F).ToString("00");
        topScoreText2.text = "2nd Score: " + topScore2;
        topTimeText2.text = "Time: " + Mathf.FloorToInt(topTime2 / 60F) + ":" + Mathf.FloorToInt(topTime2 % 60F).ToString("00");
        topScoreText3.text = "3rd Score: " + topScore3;
        topTimeText3.text = "Time: " + Mathf.FloorToInt(topTime3 / 60F) + ":" + Mathf.FloorToInt(topTime3 % 60F).ToString("00");

    }

    // Start ��ư�� Ŭ������ �� ȣ��� �޼���
    public void StartClick()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    // Quit ��ư�� Ŭ������ �� ȣ��� �޼���
    public void QuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ���� ���, �÷��� ��� ����
#else
        Application.Quit();  // ����� ���ø����̼ǿ����� ����
#endif
    }

    public void OptionButton()
    {
        optionPanel.SetActive(true);
    }

    public void BackClick()
    {
        optionPanel.SetActive(false);
    }

    public void SetBackgroundVolume(float volume)
    {
        AudioManager.instance.SetBackgroundVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        AudioManager.instance.SetSoundEffectsVolume(volume);
    }

}
