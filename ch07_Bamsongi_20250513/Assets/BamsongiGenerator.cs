// ȭ���� ���� ������ ����̸� �� ���� ����� ���ʷ����� ��ũ��Ʈ �ۼ��ϱ� : BamsongiGenerator

using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{
    // ������ ���赵 ������ ���� public GameObject ���� ����
    public GameObject gBamsongiPrefab = null;

    // Instantiate�� ����� ������Ʈ ���� ����
    GameObject insBamsongiPrefab = null;

    // ����� ���� ��ǥ 
    Vector3 vBamsongiWorldDir = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ����ȭ���� ���콺�� Ŭ������ �� �۵��ϴ� �Լ�
        if (Input.GetMouseButtonDown(0))
        {
            // ������ �����ϴ� ���߿� ����� ������Ʈ�� ����
            insBamsongiPrefab = Instantiate(gBamsongiPrefab);

            /*
             * Ray Ŭ����
             *   Ray(����)�� �̸� �״�� �����̸�, ������ ��ǥ(Origin)�� ������ ����(direction)�� ��� ������ ����
             *   Ray�� �ݶ��̴��� ����� ������Ʈ�� �浹�� �����ϴ� Ư¡�� ����
             *   ScreenPointToRay �޼����� ��ȯ������ ���� �� �ִ� Ray�� Origin�� Main camera�� ��ǥ��,
             *      direction�� ī�޶󿡼� ���� ��ǥ�� ���ϴ� ����
             *   direction �������� ����̸� ������ ������ direction ���Ͱ� ���� normalized ������ ����� ���̰� 1�� ���ͷ� ���� ��
             *      ���� 2000 ���Ѵ�. �ϴ� ���̸� 1 ���ͷ� �ؼ� direction ���� ũ�⿡ ������� ����̿� ������ ���� ���� �� ����
            */

            Ray ScreenPointToRayBamsongi = Camera.main.ScreenPointToRay(Input.mousePosition);

            vBamsongiWorldDir = ScreenPointToRayBamsongi.direction;

            insBamsongiPrefab.GetComponent<BamsongiController>().f_TargetShoot(vBamsongiWorldDir.normalized * 2000);
        }
    }
}
