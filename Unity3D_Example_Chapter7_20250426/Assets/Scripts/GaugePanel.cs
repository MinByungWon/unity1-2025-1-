
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ŭ�����Ͻ� ��ȭ�ϴ� power ���� ������ �°� ������ ǥ��
/// </summary>
public class GaugePanel : MonoBehaviour
{
    [SerializeField]
    Image _gaugeBar = null; // ��ȭ�� Image
    [SerializeField]
    Image _gaugeBarSpace = null; //�ִ� �ʺ� ����

    float _maxWidth = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.GaugeUI = this;// GameManager���� �ڱⰡ�� �Ѱ��ش�.
        if (_gaugeBarSpace != null)
        {
            // �ִ� ����
            _maxWidth = _gaugeBarSpace.rectTransform.rect.width;
        }
        gameObject.SetActive(false); // ���۽� Active false
    }

    // �Ѱܹ��� �ִ��Ŀ� ��� ���� �Ŀ� ���� ��ŭ �ִ� �ʺ� ���Ͽ� ���
    public void SetGauge(float powerRate)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        float currentWidth = _maxWidth * powerRate; // �ִ� �ʺ� ���� �Ŀ� ���� ���ϱ�
        Vector2 sizeDelta = _gaugeBar.rectTransform.sizeDelta; 
        sizeDelta.x = currentWidth; //x �ʺ� ����
        _gaugeBar.rectTransform.sizeDelta = sizeDelta; //����
    }

    //������ ��Ȱ��ȭ
    public void InActiveGauge()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}
