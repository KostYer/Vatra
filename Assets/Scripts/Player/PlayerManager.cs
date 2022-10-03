
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
     
  

    public static int currentHealth = 100;
    public Slider healthBar;

    public static bool gameOver;
    public static bool winLevel;

    public GameObject gameOverPanel;

    public float timer = 0;

    void Start()
    {
       
        gameOver = winLevel = false;
    }

    void Update()
    {
        //Display the number of coins
       

        //Update the slider value
   //     healthBar.value = currentHealth;

        //game over
        if(currentHealth < 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHealth = 100;
        }

        
    }
}
