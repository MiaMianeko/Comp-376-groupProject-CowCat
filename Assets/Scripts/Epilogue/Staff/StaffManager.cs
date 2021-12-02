using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaffManager : MonoBehaviour
{
    [SerializeField] private GameObject staffObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        staffObject.transform.localPosition =
            Vector3.MoveTowards(staffObject.transform.localPosition, new Vector3(0, 1353, 0), 1.0f);
        if (staffObject.transform.localPosition.Equals(new Vector3(0, 1353, 0)))
        {
            Invoke("GoToMenu", 3.0f);
        }
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}