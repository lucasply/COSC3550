using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stars : MonoBehaviour
{
    public GameObject starPrefab;
    private List<GameObject> starIcons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        getStars();
    }

    private void getStars()
    {
        if(SceneManager.GetActiveScene().name == "Victory_Screen"){
            int lives = PlayerPrefs.GetInt("PlayerLives", defaultValue: 0);
            for (int i = 0; i < lives; i++)
            {
                Debug.Log("Adding star icon: " + i);
                var starIcon = Instantiate(starPrefab, Vector3.zero, Quaternion.identity, transform);
                RectTransform rectTransform = starIcon.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((i*175-175), (rectTransform.anchoredPosition.y+500));
                starIcons.Add(starIcon);
            }
        }
        else{
            int lives = PlayerPrefs.GetInt("PlayerLives", defaultValue: 0);
            for (int i = 0; i < lives; i++)
            {
                Debug.Log("Adding star icon: " + i);
                var starIcon = Instantiate(starPrefab, Vector3.zero, Quaternion.identity, transform);
                starIcon.transform.localScale = new Vector3(100f, 100f, 100f);
                RectTransform rectTransform = starIcon.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((i*45-45), (rectTransform.anchoredPosition.y+400));
                starIcons.Add(starIcon);
            }
        }
    }
}
