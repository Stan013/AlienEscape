using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endscreen : MonoBehaviour
{
    private enum TypeOfUI
    {
        EndScreen,
        Tutorial
    }

    [SerializeField]
    private TypeOfUI UiType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        if(UiType == TypeOfUI.EndScreen)
        {
            SceneManager.LoadScene("Final");
        }
    }
}
