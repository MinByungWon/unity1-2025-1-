using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트를 움직일 수 있는 컨트롤러 스크립트 : 키를 조작해서 플레이어 움직이기
//   추가 --> 단, 플레이어가 게임창을 벗어나지 않도록 Vector 최솟값, 최댓값 설정하기
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float fMaxPositionX;

    [SerializeField]
    float fMinPositionX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ArrowGenerator 오브젝에서 게임 재시작 return GetIsRun()함수 호출
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // GetKeyDown 메서드 : 키가 눌렀는지 검출하는 메서드
                // 왼쪽 화살표 키가 눌렀을 때
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    // Translate 메서드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메서드
                    transform.Translate(-3, 0, 0);  // 왼쪽으로 '3' 움직인다.
                }

                // 오른쪽 화살표 키가 눌렀을 때
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Translate(3, 0, 0);  // 오른쪽으로 '3' 움직인다.
                }

                // Mathf Clamp() : 최소/최대값을 설정하여 지정한 범위 이외에 값이 되지 않도록 할 때 사용
                // 플레이어가 움직일 수 있는 최소 / 최대 범위값을 설정하여 그 범위를 벗어나지 않도록한다.
                float fPositionX = Mathf.Clamp(transform.position.x, fMinPositionX, fMaxPositionX);
                transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);
            }
        }
    }

    // 왼쪽 버튼을 눌렀을 때 플레이어를 왼쪽으로 이동시키는 메서드
    public void LButtonDown()
    {
        // ArrowGenerator 오브젝에서 게임 재시작 return GetIsRun()함수 호출
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Translate 메서드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메서드
                transform.Translate(-3, 0, 0);  // 왼쪽으로 '3' 움직인다.
            }
        }
    }

    // 오른쪽 버튼을 눌렀을 때 플레이어를 오른쪽으로 이동시키는 메서드
    public void RButtonDown()
    {
        // ArrowGenerator 오브젝에서 게임 재시작 return GetIsRun()함수 호출
        if (FindObjectOfType<GameDirector>() != null)
        {
            if (FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                transform.Translate(3, 0, 0);  // 오른쪽으로 '3' 움직인다.
            }
        }
    }


}
