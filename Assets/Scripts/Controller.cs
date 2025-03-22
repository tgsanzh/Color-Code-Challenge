using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private int countOfColor;
    [SerializeField] int[] randomColor;
    private List<int> randomColorList = new List<int>();

    [SerializeField] private GameObject[] colors;
    private GameObject currentChosed;
    private Vector2 currentChosedStartPos;
    [SerializeField] private Text rightText;
    [SerializeField] private Text movesText;
    [SerializeField] private Text levelText;
    private int moveCount = 0;
    [SerializeField] private GameObject winScene;

    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip moveClip;

    private Vector3 startMousePos;

    private void Awake()
    {
        
        int currentLevel = PlayerPrefs.GetInt("level", 1);
        print(currentLevel);
        if(currentLevel > 0 && currentLevel <= 3)
        {
            if(SceneManager.GetActiveScene().name != "3")
            {
                SceneManager.LoadScene("3");
            }
        }
        else if (currentLevel > 3 && currentLevel <= 8)
        {
            if (SceneManager.GetActiveScene().name != "4")
            {
                SceneManager.LoadScene("4");
            }
        }
        else if (currentLevel > 8 && currentLevel <= 15)
        {
            if (SceneManager.GetActiveScene().name != "5")
            {
                SceneManager.LoadScene("5");
            }
        }
        else if (currentLevel > 15 && currentLevel <= 25)
        {
            if (SceneManager.GetActiveScene().name != "6")
            {
                SceneManager.LoadScene("6");
            }
        }
        else if (currentLevel > 25 && currentLevel <= 40)
        {
            if (SceneManager.GetActiveScene().name != "7")
            {
                SceneManager.LoadScene("7");
            }
        }
        else if (currentLevel > 40 && currentLevel <= 60)
        {
            if (SceneManager.GetActiveScene().name != "8")
            {
                SceneManager.LoadScene("8");
            }
        }
        else if (currentLevel > 60 && currentLevel <= 85)
        {
            if (SceneManager.GetActiveScene().name != "9")
            {
                SceneManager.LoadScene("9");
            }
        }
        else if (currentLevel > 85 && currentLevel <= 115)
        {
            if (SceneManager.GetActiveScene().name != "10")
            {
                SceneManager.LoadScene("10");
            }
        }
        else if (currentLevel > 115 && currentLevel <= 150)
        {
            if (SceneManager.GetActiveScene().name != "11")
            {
                SceneManager.LoadScene("11");
            }
        }
        else if (currentLevel > 195 && currentLevel <= 245)
        {
            if (SceneManager.GetActiveScene().name != "12")
            {
                SceneManager.LoadScene("12");
            }
        }
        else if (currentLevel > 245)
        {
            if (SceneManager.GetActiveScene().name != "13")
            {
                SceneManager.LoadScene("13");
            }
        }
        levelText.text = "LEVEL " + currentLevel;

    }

    private void Start()
    {
        for (int i = 0; i < countOfColor; i++)
        {
            randomColorList.Add(i);
        }
        randomColor = randomColorList.ToArray();
        while (randomColor[0] == 0 && randomColor[1] == 1 && randomColor[2] == 2)
        {
            ShuffleArray(randomColor);
        }
        Vibration.Init();
        check();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit && color_code.isMoving == false)
            {
                currentChosed = hit.collider.gameObject;
                currentChosedStartPos = currentChosed.transform.position;
                currentChosed.GetComponent<SpriteRenderer>().sortingOrder = 1;
                currentChosed.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.3f);

                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        if(Input.GetMouseButtonUp(0))
        {
            if(currentChosed != null)
            {
                if (currentChosed.GetComponent<color_code>().currentColor != null)
                {
                    currentChosed.transform.position = currentChosed.GetComponent<color_code>().currentColor.transform.position;
                    currentChosed.GetComponent<color_code>().currentColor.GetComponent<color_code>().targetPos = currentChosedStartPos;
                    currentChosed.GetComponent<color_code>().currentColor.GetComponent<color_code>().needMove = true;

                    int i = currentChosed.GetComponent<color_code>().currentColor.GetComponent<color_code>().which;
                    currentChosed.GetComponent<color_code>().currentColor.GetComponent<color_code>().which = currentChosed.GetComponent<color_code>().which;
                    currentChosed.GetComponent<color_code>().which = i;


                    currentChosed.GetComponent<SpriteRenderer>().sortingOrder = 0;


                    currentChosed.transform.GetChild(0).GetComponent<Animation>().Play();


                    color_code.isMoving = true;
                    check();
                    moveCount++;
                    movesText.text = moveCount.ToString();
                    Vibration.VibrateAndroid(30);
                    currentChosed.GetComponent<BoxCollider2D>().size = new Vector2(3f, 3f);
                    GameObject.Find("Audio Source").GetComponent<AudioSource>().PlayOneShot(moveClip);
                }
                else
                {
                    currentChosed.GetComponent<BoxCollider2D>().size = new Vector2(3f, 3f);
                    currentChosed.transform.position = currentChosedStartPos;
                }
                currentChosed = null;
            }
            
        }
        if(currentChosed != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentChosed.transform.position = new Vector3(mousePos.x - startMousePos.x + currentChosedStartPos.x, mousePos.y - startMousePos.y + currentChosedStartPos.y, 0);
        }

    }

    void ShuffleArray<T>(T[] array)
    {
        System.Random random = new System.Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public void check()
    {
        int right = 0;
        for (int i = 0; i < countOfColor; i++)
        {
            if (colors[i].GetComponent<color_code>().which == randomColor[i])
            {
                right++;
            }
        }
        rightText.text = right.ToString();
        if(right == countOfColor)
        {
            winScene.SetActive(true);
            winScene.transform.GetChild(0).GetComponent<Animation>().Play();
            GameObject.Find("Audio Source").GetComponent<AudioSource>().PlayOneShot(winClip);
        }
    }

}
