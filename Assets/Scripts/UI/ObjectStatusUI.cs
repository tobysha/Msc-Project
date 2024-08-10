using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectStatusUI : MonoBehaviour
{
    //public GameObject objectToShowStatus; // ��Ҫ��ʾ״̬����Ϸ����
    [SerializeField]private GameObject statusText; // UI����е��ı����
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI ATKText;

    private GameManager gameManager;

    GameObject lastObj;
    private void Start()
    {
        lastObj = this.gameObject;
        if(SceneManager.GetActiveScene().buildIndex != 0)       //get Gamemanager if it is not game menu
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }
    void Update()
    {
        // ������������
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetMouseButtonDown(0) && (gameManager.GetcurrentStage() == GameManager.GameStage.GamingStage || gameManager.GetcurrentStage() == GameManager.GameStage.crazyStage))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ʹ��2D���߼��
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            // ��������Ƿ��������
            if (hit.collider != null)
            {
                GameObject currentobj = hit.collider.gameObject;
                if (currentobj.CompareTag("Enemy") || currentobj.CompareTag("Tower"))
                {
                    currentobj.GetComponent<ObjectsData>().ATKrange_visable(true);
                    ShowStatus(currentobj);
                    if(lastObj != null)
                    {
                        if ((lastObj.CompareTag("Enemy") || lastObj.CompareTag("Tower")))
                        {
                            lastObj.GetComponent<ObjectsData>().ATKrange_visable(false);
                        }
                    }
                    lastObj = currentobj;
                }
            }
            else
            {
                if (lastObj != this.gameObject && lastObj != null)
                {
                    lastObj.GetComponent<ObjectsData>().ATKrange_visable(false);
                }
                statusText.SetActive(false);
            }
        }
    }

    void ShowStatus(GameObject obj)
    {
        statusText.SetActive(true);
        ObjectsData objectdata = obj.GetComponent<ObjectsData>();// get gameobject and show states
        NameText.text = objectdata.Name;
        HPText.text = "HP: " + objectdata.HP.ToString();
        ATKText.text = "ATK: " + objectdata.atk.ToString();
        SpeedText.text = "Speed: " + objectdata.speed.ToString();

    }
}
