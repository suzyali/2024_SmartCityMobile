using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player") // Player �±׸� ���� ������Ʈ�� �浹 ����
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                SceneManager.LoadScene("���� �ǹ���"); // �ǹ� ������ ��ȯ
            }
        }
    }
}