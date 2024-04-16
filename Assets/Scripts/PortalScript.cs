using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player") // Player 태그를 가진 오브젝트와 충돌 감지
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                SceneManager.LoadScene("다음 건물씬"); // 건물 씬으로 전환
            }
        }
    }
}