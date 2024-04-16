using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 코루틴을 사용하기 위해 필요

public class LoadThirdSceneAfterDelay : MonoBehaviour
{
    // Start 메서드에서 코루틴을 시작합니다.
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(5)); // 5초 후에 씬 로딩
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간(초) 동안 대기

        // 대기 시간이 끝나면 세 번째 씬으로 이동
        SceneManager.LoadScene(3);
    }
}
