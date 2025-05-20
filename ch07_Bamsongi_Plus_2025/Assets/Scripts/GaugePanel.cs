
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 클릭중일시 변화하는 power 값의 비율에 맞게 게이지 표시
/// </summary>
public class GaugePanel : MonoBehaviour
{
    [SerializeField]
    Image _gaugeBar = null; // 변화할 Image

    [SerializeField]
    Image _gaugeBarSpace = null; //최대 너비 계산용

    float _maxWidth = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.GaugeUI = this;// GameManager에게 자기가신 넘겨준다.
        if (_gaugeBarSpace != null)
        {
            // 최대 넓이
            _maxWidth = _gaugeBarSpace.rectTransform.rect.width;
        }
        gameObject.SetActive(false); // 시작시 Active false
    }

    // 넘겨받은 최대파워 대비 현재 파워 비율 만큼 최대 너비에 곱하여 사용
    public void SetGauge(float powerRate)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        float currentWidth = _maxWidth * powerRate; // 최대 너비에 현재 파워 비율 곱하기
        Vector2 sizeDelta = _gaugeBar.rectTransform.sizeDelta;
        sizeDelta.x = currentWidth; //x 너비를 변경
        _gaugeBar.rectTransform.sizeDelta = sizeDelta; //적용
    }

    //게이지 비활성화
    public void InActiveGauge()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}
