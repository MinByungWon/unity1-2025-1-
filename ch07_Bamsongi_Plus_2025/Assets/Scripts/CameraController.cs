using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// 시네머신을 활용한 카메라 컨트롤
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("시작지점 카메라")]
    CinemachineCamera _startCam;
    [SerializeField, Tooltip("종료지점 카메라")]
    CinemachineCamera _endCam;
    private CinemachineBrain _brain = null;

    Coroutine _coroutine = null; // 코루틴 제어변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_startCam != null)
        {
            _startCam.transform.position = transform.position; // 메인카메라와 같은 위치
            _startCam.Priority = 1; // 시작 카메라 우선순위를 위로
          }
        else
        {
            Debug.LogError("_startCam is null");
        }

        if (_endCam != null)
        {
            _endCam.Priority = 0; // 목포 카메라 우선순위 0
        }
        else
        {
            Debug.LogError("_endCam is null");
        }

        if (_brain == null)
        {
            _brain = GetComponent<CinemachineBrain>(); //mainCamera의 시네머신브레인
        }

        // GameManager에게 해당 인스턴스 전달
        GameManager.Instance.CameraControl = this;
    }

    public void CameraMove()
    {
        GameManager.Instance.CanClick = false; // 카메라 움직일때는 발사 못하도록 설정
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CorMoveCamera());
        }
    }

    IEnumerator CorMoveCamera()
    {
        yield return new WaitForSeconds(1f); // 잠시 기다림

        // ease 설정
        _brain.DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.EaseIn, 0.6f);
        // 우선 순위 바꿈 _endCame으로 카메라 시점 이동
        _startCam.Priority = 0;
        _endCam.Priority = 1;
        yield return null; //프레임 넘기기
        // 카메라 전환이 끝날 때까지 프레임 넘기며 대기
        while (_brain.IsBlending)
        {
            yield return new WaitForEndOfFrame(); //프레임 넘기기
        }

        // _endCam으로 전환 되었을 시 2초 정도 기다림
        yield return new WaitForSeconds(2f);

        // 다시 _startCam 으로 전환
        _brain.DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.EaseOut, 0.4f);
        _startCam.Priority = 1;
        _endCam.Priority = 0;
        yield return null;
        while (_brain.IsBlending)
        {
            yield return new WaitForEndOfFrame();
        }

        if (GameManager.Instance.RestCount > 0)
        {
            // 전부 완료 시 GameManager.Instance의 canClick = true로 설정
            // 다시 밤송이 발사 가능
            GameManager.Instance.CanClick = true;
            _coroutine = null; // 다시 코루틴 호출 가능하게 초기화
        }
        else
        {
            GameManager.Instance.GameEnd();
        }
    }
}
