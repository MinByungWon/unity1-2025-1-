using UnityEngine;

public class TitleUIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void f_GameStart()
    {
        SoundManager.Instance.f_StopAllBGM(); //모든 배경음악 재생 중지
        SoundManager.Instance.f_PlayBGM(SoundName.BGM_StageBGM1, 0.1f);     //스테이지2 배경음악 재생
        SoundManager.Instance.f_PlaySFX(SoundName.SFX_ButtonClick, 1.0f);   //버튼 클릭 효과음 재생
        GameManager.Instance.f_GameStart(); //게임 시작
    }
}
