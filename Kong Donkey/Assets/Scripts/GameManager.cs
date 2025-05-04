using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            rectTransform.anchoredPosition = new Vector2((i*100+1350), (rectTransform.anchoredPosition.y+1000));
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

        int choice = Random.Range(0, 3);
        if (choice == 0) {
            FindObjectOfType<Player>().playPlayerSound(2);
        } else if (choice == 1) {
            FindObjectOfType<Kong>().playKongSound(2);
        } else if (choice == 2) {
            FindObjectOfType<Iggy>().playIggySound(1);
        }
        
        if(lives <= 0){
            Debug.Log("Game Over!");
            // Add some delay before loading the end screen to allow for death animation
            SceneManager.LoadScene("End_Screen");
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
        FindObjectOfType<Player>().playPlayerSound(3);
        this.player.gameObject.layer  = LayerMask.NameToLayer("Default");
        this.player.state = PlayerState.Idle;
        this.player.transform.position = new Vector3(-4.597f, -4.151f, 0);

    }

    public void Victory(){
        Debug.Log("You win!");
        int savedLives = PlayerPrefs.GetInt("PlayerLives", 0);
        
        // Save number of lives player completed the game with
        if(savedLives < lives){
            Debug.Log($"Saved Lives: {savedLives}, Current Lives: {lives}");
            PlayerPrefs.SetInt("PlayerLives", lives);
            PlayerPrefs.Save();
        }
        
        // Add some delay before loading the end screen to allow for victory animation
        SceneManager.LoadScene("Victory_Screen");
        
    }
}
