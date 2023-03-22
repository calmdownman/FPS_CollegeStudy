using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject gameLabel; //���� ���� UI ������Ʈ ����
    Text gameText; //���� ���� UI�ؽ�Ʈ ������Ʈ ����

    public enum GameState
    {
        Ready,
        Run,
        GameOver
    }

    public GameState gState; //���� ���� ���

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gState = GameState.Ready; //�ʱ� ���ӻ��´� �غ�
        gameText = gameLabel.GetComponent<Text>();
        gameText.text = gState.ToString();
        gameText.color = new Color32(255,185,0,255);
        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart() 
    {
        yield return new WaitForSeconds(2f);
        gameText.text = "Go!"; //���� �ؽ�Ʈ ����
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);
        gState = GameState.Run;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
