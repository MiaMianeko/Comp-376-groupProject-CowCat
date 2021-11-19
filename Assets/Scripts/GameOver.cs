using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject textGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
        
    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene("BirksScene");
    }
}
