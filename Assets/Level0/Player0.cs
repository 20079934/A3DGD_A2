using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player0 : MonoBehaviour
{
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Launcher");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));// middle of the screen
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Enemy":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Token":
                score++;
                Destroy(other.gameObject);
                if(score==4)
                {
                    FindObjectOfType<GameManager>().nextLevel();
                }
                break;
            default:
                break;
        }
    }
}
