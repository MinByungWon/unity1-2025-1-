//ȭ���� Ŭ���ϸ� 'Ŭ���� ��'���� '���� ��'���� ��ȯ

using System.Data.SqlTypes;
using UnityEngine;

public class ClearDirector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ����Ƽ���� ���� �ε��ϴ� ���� SceneManger.LoadScene() �޼ҵ带 ���
         * �� �̸��̳� ���� ���� �ε����� �Ķ���ͷ� �����Ͽ� Ư�� ���� �ε��� �� ����
         * ���� �ε��ϴ� ���� �ٸ� ���� �Բ� �ε��ϰų�, �ε�� ���� ��ε� �ϴ� ���� �پ��� �ɼǵ� ������
         * �� �̸����� �ε� : SceneManger.LoadScene("MySceneName");
         * ���� ���� �ε����� �ε� : SceneManger.LoadScene(1); (�� ��° ���� �ε�)
         * ���콺�� Ŭ���� ���� �����ϸ�, SceneManger Ŭ������ LoadScene �޼ҵ带 ����� ���� ������ ��ȯ
         */
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.f_RestartGame();
        }
    }

    
}
