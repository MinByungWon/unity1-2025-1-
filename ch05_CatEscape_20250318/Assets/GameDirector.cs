using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;                // UI ���� ���ӽ����̽��� �߰��ϴ� ����


public class GameDirector : MonoBehaviour
{
    /* HP Gauge Image Object�� ������ ��� ����
     *   ���� ��ũ��Ʈ�� ����� HP �������� �����Ϸ��� ���� ��ũ��Ʈ�� HP �������� ��ü�� ������ �� �־�� ��
     *   �׷��� ���ؼ� Object ������ �����ؼ� HP Gauge Image Object�� ����
    */
    GameObject gHpGauge = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* hpGauge ������Ʈ ã��
         *   �� ������Ʈ ���ڿ� �����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־�� �Ѵ�.
         *   �� �ȿ��� ������Ʈ�� ã�� �޼��� : Find
         *   Find �޼���� ������Ʈ �̸��� �μ��� �����ϰ�,
         *   �μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * Find �޼��带 ����� �� �߿��� HP �������� ������Ʈ�� ã�Ƽ� ������Ʈ ������ gHpGauge�� ����  
        */
        gHpGauge = GameObject.Find("hpGauge");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* ���߿� ȭ�� ��Ʈ�ѷ����� HP ������ ǥ�ø� ���̴� ó���� ȣ���� ���� �����
     *   HP �������� ó���� public �޼��带 �ۼ�
     * ȭ��� �÷��̾ �浹���� �� ȭ�� ��Ʈ�ѷ��� f_DecreaseHp() �޼��带 ȣ����
     * �޼����� ����� ȭ��� �÷��̾ �浹���� �� Image ������Ʈ(hpGauge)�� fillAmount�� �ٿ�
     *   HP �������� ǥ���ϴ� ������ 10%�� ����
    */
    public void f_DecreaseHp()
    {
        /* ����Ƽ ������Ʈ�� GameObject��� �� ���ڿ� ���� �ڷ�(���۳�Ʈ)�� �߰��ؼ� ����� Ȯ����
         *   (��) ������Ʈ�� ���������� �����̰� �Ϸ��� Rigidbody ���۳�Ʈ �߰�
         *   (��) �Ҹ��� ���� �Ϸ��� AudioSource ���۳�Ʈ �߰�
         *   (��) ��ü ����� �ø��� �ʹٸ� ��ũ��Ʈ ���۳�Ʈ�� �߰���
         * ���۳�Ʈ ���� ��� : GetComponent<>()
         *   GetComponent�� ���� ������Ʈ�� ���� 'oo ������Ʈ�� �ּ���'��� ��Ź�ϸ�,
         *   �ش�Ǵ� ���۳�Ʈ(���)�� �����ִ� �޼���
         *   (��) AudioSource ���۳�Ʈ�� ���ϸ� -> GetComponent<AudioSource>()
         *   (��) Text ���۳�Ʈ�� ���ϸ�->GetComponent<Text>()
         *   (��) ���� ���� ��ũ��Ʈ�� ���۳�Ʈ�� �����̹Ƿ� GetComponent �޼��带 ����ؼ� ���� �� ����
        */

        // ȭ��� �÷��̾ �浹���� �� Image ������Ʈ(hpGauge)�� fillAmount�� �ٿ�
        //   HP �������� ǥ���ϴ� ������ 10%�� ����
        gHpGauge.GetComponent<Image>().fillAmount -= 0.1f;

    }
}
