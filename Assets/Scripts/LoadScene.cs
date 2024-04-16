using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // �ڷ�ƾ�� ����ϱ� ���� �ʿ�

public class LoadThirdSceneAfterDelay : MonoBehaviour
{
    // Start �޼��忡�� �ڷ�ƾ�� �����մϴ�.
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(5)); // 5�� �Ŀ� �� �ε�
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð�(��) ���� ���

        // ��� �ð��� ������ �� ��° ������ �̵�
        SceneManager.LoadScene(3);
    }
}
