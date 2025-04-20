using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100; 
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText; // 新增游戏结束文本引用[1,3](@ref)

    void Start()
    {
        UpdateHealthDisplay();
        gameOverText.gameObject.SetActive(false); // 初始化隐藏失败提示[3](@ref)
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        ShowGameOver(); // 调用游戏结束提示[3](@ref)
        gameObject.SetActive(false);
    }

    void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "YOU LOSE!"; // 设置失败文本内容[1](@ref)
            gameOverText.gameObject.SetActive(true); // 激活文本显示[3](@ref)
        }
        else
        {
            Debug.LogError("GameOverText 未赋值！");
        }
    }
}