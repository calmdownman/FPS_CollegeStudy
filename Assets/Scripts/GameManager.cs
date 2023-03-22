using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject gameLabel; //게임 상태 UI 오브젝트 변수
    Text gameText; //게임 상태 UI텍스트 컴포넌트 변수

    public enum GameState
    {
        Ready,
        Run,
        GameOver
    }

    public GameState gState; //게임 상태 상수

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gState = GameState.Ready; //초기 게임상태는 준비
        gameText = gameLabel.GetComponent<Text>();
        gameText.text = gState.ToString();
        gameText.color = new Color32(255,185,0,255);
        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart() 
    {
        yield return new WaitForSeconds(2f);
        gameText.text = "Go!"; //상태 텍스트 변경
        yield return new WaitForSeconds(0.5f);
        gameLabel.SetActive(false);
        gState = GameState.Run;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
