using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&& !other.isTrigger)
        {
            StartCoroutine(LoadSceneAfterDelay(4.0f));
        }
                
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
