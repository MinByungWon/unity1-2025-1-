/*
 * ���� ������ Manager ��ũ��Ʈ���� �ʱ�ȭ �� Ÿ��Ʋ ������ �̵��ϱ� ���� ��ũ��Ʈ
 * �� ��(ManagersScene) �� Manager ������Ʈ �ʱ�ȭ �� TitleScene���� �̵�
 * 
 * Manager ��ũ��Ʈ���� �̱��� �������� �ۼ��Ǿ��⿡ �� ��ȯ���� �������� ����
 * ���� �� �ε� �켱������ ���� �տ��� �� ���� �ε��ϸ� ������ ������ ���� ��� ����
 */
using UnityEngine;
using UnityEngine.SceneManagement; //���� �ε��ϱ� ���� SceneManagement ����Ʈ

public class ManagerLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TitleScene���� ��ȯ
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
