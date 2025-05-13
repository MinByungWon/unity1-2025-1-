/*
 * �����̽��ٸ� ������ ����̰� ������ ���� ���ư��� ���� ���� 
 * 
 */


using UnityEngine;

public class BamsongiController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* 
         * ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
         *   � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         *   ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         *   �����ӷ���Ʈ�� 60���� ����
        */
        Application.targetFrameRate = 60;

        /*
         * ����̰� ȭ�� �������� ���ư����� +Z�� ������ ���͸� �Ű������� �����ϰ� f_TargetShoot �޼��带 ȣ��
         * Y�� �������� ���� 200 ���ϴ� ������ ����̰� ���ῡ ��� ���� �߷��� ������ �޾� �������� �����ϴ� ���� ���� ����
         * Start �޼��忡�� f_TargetShoot �޼��带 ȣ���ϹǷ� ���� ���۰� ���ÿ� ���ư�
        */
        //f_TargetShoot(new Vector3(0, 200, 2000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ȯ�强�� ����� �Ű��������� ������ �������� ���� ���ϴ� �޼���
    public void f_TargetShoot(Vector3 argvDir)
    {
        // �Ű������� ���޵� Vector ������ ���� ���Ѵ�.
        GetComponent<Rigidbody>().AddForce(argvDir);

    }

    // Physics�� ����ϹǷ� ����� ����̰� �浹�ϸ� OnCollisionEnter �޼��尡 ȣ��Ǿ� �����
    private void OnCollisionEnter(Collision collision)
    {
        // ����̰� ���ῡ ��� ���� ����� �������� ���߹Ƿ�, Rigidbody ������Ʈ�� isKinematic �޼��带 true�� ����
        //   isKinematic �޼��带 true�� ���� �ϸ�, ������Ʈ�� �ۿ��ϴ� ���� �����ϰ� ����̸� ������Ŵ
        //   isKinematic �޼��� : �ܺο��� �������� ������ ���� �������� �ʴ� ������Ʈ��� �ǹ�. �߷°� �浹�� �������� �ʵ��� ��
        GetComponent<Rigidbody>().isKinematic = true;

        // GetComponent �޼��带 ����� ParticleSystem ������Ʈ�� ���ϰ�, ParticleSystem ���� Play �޼��带 ȣ���� ��ƼŬ�� ���
        GetComponent<ParticleSystem>().Play();  
    }

}
