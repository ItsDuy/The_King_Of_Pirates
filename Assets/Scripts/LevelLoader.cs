using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {        
    }

    public void LoadLevel()
    {
        transition.SetTrigger("Start");
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Lvl_1");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
