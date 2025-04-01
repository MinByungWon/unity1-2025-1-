using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ȭ�� Prefab�� �������� ȭ�� �ν��Ͻ��� 1�ʸ��� �� ���� �����ϴ� ��ũ��Ʈ
public class ArrowGenerator : MonoBehaviour
{
    // ȭ�� Prefab�� ���� ����� ����
    // �ƿ﷿ ������ ���� arrowPrefab ������ ������ ��ü�� �����ϱ� ���� �ܼ�Ʈ ������ ����� ���ؼ� public ���� ������
    public GameObject arrowPrefab = null;

    // ȭ�� ���� ���� : ȭ���� 1�ʸ��� ���� ����
    [SerializeField]
    float fSpan = 1.0f;

    // �� �����Ӱ� ���� ������ ������ �ð� �� ����
    [SerializeField]
    float fDelta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ArrowGenerator ���������� ���� ����� return GetIsRun()�Լ� ȣ��
        if (FindObjectOfType<GameDirector>() != null)
        {
            if(FindObjectOfType<GameDirector>().GetIsRun() == true)
            {
                // Update �޼���������Ӹ��� ����ǰ� �� �����Ӱ� ���� ������ ������ �ð� ���� ������ ����
                // �����Ӱ� ������ ������ �ð� ���̸� fDelta ������ ����
                fDelta += Time.deltaTime;

                // �����Ӱ� ������ ������ �ð� ���� 1�� �̻��̸�,
                if (fDelta > fSpan)
                {
                    fDelta = 0;  // �볪���� ����(fDelta) �ʱ�ȭ

                    // Instantiate �޼��� : ȭ�� �ν��Ͻ� ����
                    //    �Ű������� �������� �����ϸ�, ��ȯ������ ������ �ν��Ͻ��� �����ش�.
                    GameObject go = Instantiate(arrowPrefab);

                    // Random.Range �޼��� : ȭ���� X ��ǥ�� -6 ~ 6 ���̿� �ұ�Ģ�ϰ� ��ġ
                    //    ù ��° �Ű��������� ũ�ų� ����,�� ��° �Ű��������� ���� �������� ������ ���� ������ ��ȯ
                    int px = Random.Range(-6, 7);
                    go.transform.position = new Vector3(px, 7, 0);
                }
            }
        }

       
        
    }
}
