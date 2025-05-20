using UnityEngine;

public class RouletteController : MonoBehaviour
{

    float fRouletteRotationSpeed = 0.0f; //�ѷ� ȸ���ӵ� ���� �������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���콺�� Ŭ���ϸ� �귿�� �� ������ 10���� ȸ��
        //    Ŭ���ϸ� ȸ�� �ӵ��� ������� fRouletteRotationSpeed�� �����Ѵ�.
        if (Input.GetMouseButtonDown(0))
        {
            fRouletteRotationSpeed = 10;
        }

        // �ѷ� ������Ʈ�� ���� �������� �μ� ����ŭ ȸ����
        transform.Rotate(0, 0, fRouletteRotationSpeed);

        // �ѷ� ȸ�� ���ӽ�Ű��
        fRouletteRotationSpeed *= 0.98f;

    }
}
