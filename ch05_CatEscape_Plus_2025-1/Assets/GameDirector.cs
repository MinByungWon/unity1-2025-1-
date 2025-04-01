using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ������Ʈ�� ��ũ��Ʈ���� �����ϱ� ���ؼ� ����Ʈ

// �÷��̾ ȭ�쿡 ������ �̸� ������ HP �������� ǥ�ø� �����ϴ� ��ũ��Ʈ
public class GameDirector : MonoBehaviour
{
    // HP �������������Ϸ��� ���� ��ũ��Ʈ�� HP �������� ��ü�� �����ϱ� ���ؼ� GameObject ���� ����
    Image hpGauge;

    public GameObject txtGameOverMessage = null;  // UI(Text) : txtGameOverMessage GameObject ����

    public GameObject btnReStart = null; // UI(Button) : btnReStart GameObject ����

    bool isRun = false;  // ���� ����� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // Find �޼��带 ��� �� �� �߿��� HP �������� ������Ʈ�� ã�� hpGauge ������ ����
        hpGauge = GameObject.Find("hpGauge").GetComponent<Image>();

        // Game Over Text
        txtGameOverMessage = GameObject.Find("txtGameOverMessage");
        txtGameOverMessage.gameObject.SetActive(false);

        // ���� ����� Button
        btnReStart = GameObject.Find("btnReStart");
        btnReStart.gameObject.SetActive(false);

        // ���� ����� : true
        isRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ȭ�� ��Ʈ�ѷ����� HP ������ ǥ�ø� ���̴� ó���� ȣ���ϱ� ���ؼ� public �޼���� �ۼ�
    public void DecreaseHp()
    {
        // ȭ��� �÷��̾ �浹���� �� ȭ�� ��Ʈ�ѷ��� Image ������Ʈ(hpGauge)�� fillAmount�� �ٿ� HP �������� ǥ��
        hpGauge.fillAmount -= 0.1f;

        // ���� ����!!
        if (hpGauge.fillAmount <= 0)
        {
            // �������� Text Ȱ��ȭ
            txtGameOverMessage.gameObject.SetActive(true);

            // ��������� Text Ȱ��ȭ
            btnReStart.gameObject.SetActive(true);

            // ���� ����� : false
            isRun = false;
        }
    }

    public void GameInitialized()
    {
        // ������ �ʱ�ȭ
        hpGauge.fillAmount = 1;
        // �ؽ�Ʈ �ʱ�ȭ
        txtGameOverMessage.gameObject.SetActive(false);
        // ��ư �ʱ�ȭ
        btnReStart.SetActive(false);

        // ���� ����� : true
        isRun = true;

        ArrowController[] arrowControllers = FindObjectsOfType<ArrowController>();
        for (int i = arrowControllers.Length -1 ; i >= 0; i++)
        {
            Destroy(arrowControllers[i].gameObject);
        }
    }

    // ���� ����� return �Լ�
    public bool GetIsRun()
    {
        return isRun;
    }
}
