using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public Player player; // Place player object here in the inspector
    public int lives = 3;
    public int score = 0;
    public float respawnTime = 3f;

    public GameObject lifeRemainingPrefab;
    private List<GameObject> lifeIcons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeLives()
    {
        for (int i = 0; i < lives; i++)
        {
            Debug.Log("Adding life icon: " + i);
            var lifeIcon = Instantiate(lifeRemainingPrefab, Vector3.zero, Quaternion.identity, transform);
            RectTransform rectTransform = lifeIcon.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2((-860), (rectTransform.anchoredPosition.y-515));
            lifeIcons.Add(lifeIcon);
        }
    }

    private void RemoveLifeIcon(){
        if (lifeIcons.Count > 0)
        {
            GameObject lifeIcon = lifeIcons[lifeIcons.Count - 1];
            lifeIcons.RemoveAt(lifeIcons.Count - 1);
            Destroy(lifeIcon);
        }
    }

    public void playerDied(){
        this.lives--;
        RemoveLifeIcon();

        if(lives <= 0){
            Debug.Log("Game Over!");
            // Implement game over logic here
        }
        else{
            Debug.Log("Player died! Lives remaining: " + lives);
            // Implement respawn logic here
            Invoke(nameof(RespawnPlayer), respawnTime);
        }
    }

    private void RespawnPlayer(){
        // Implement respawn logic here
        Debug.Log("Player respawned!");
        this.player.gameObject.SetActive(true); // Make sure in player logic, to disable the player when it dies
        this.player.transform.position = new Vector3(-4.597f, -4.151f, 0);

    }
}
