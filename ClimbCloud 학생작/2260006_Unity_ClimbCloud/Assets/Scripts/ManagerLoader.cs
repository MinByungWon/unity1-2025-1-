/*
 * 전역 관리용 Manager 스크립트들을 초기화 후 타이틀 씬으로 이동하기 위한 스크립트
 * 빈 씬(ManagersScene) → Manager 오브젝트 초기화 → TitleScene으로 이동
 * 
 * Manager 스크립트들은 싱글톤 패턴으로 작성되었기에 씬 전환에도 삭제되지 않음
 * 따라서 씬 로딩 우선순위의 가장 앞에서 한 번만 로딩하면 이후의 씬에서 지속 사용 가능
 */
using UnityEngine;
using UnityEngine.SceneManagement; //씬을 로딩하기 위해 SceneManagement 임포트

public class ManagerLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TitleScene으로 전환
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
