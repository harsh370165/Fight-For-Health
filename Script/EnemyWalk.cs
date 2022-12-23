using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyWalk : MonoBehaviour
{
    public GameObject DownCamera;
    public GameObject UpCamera;
    public GameObject Enemy;
    public Animator enemyroar;
    void Update()
    {
        StartCoroutine(WaitNewScene());
    }
    IEnumerator WaitNewScene()
    {
        Debug.Log(Time.time);

        DownCamera.SetActive(true);
        UpCamera.SetActive(false);
        if (Enemy.transform.position.z < 225)
        {
            transform.Translate(0, 0, 10 * Time.deltaTime);
            DownCamera.SetActive(false);
            UpCamera.SetActive(true);
        }
        if (Enemy.transform.position.z > 225)
        {
            enemyroar.SetBool("roar", true);
        }
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(1);
    }
}
