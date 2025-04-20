using UnityEngine; 

public class CoinCollector : MonoBehaviour 
{ 
public AudioClip coinSound; // 金币被收集的音效 

private void OnTriggerEnter(Collider other) 
{ 
// 假设玩家有一个名为"Player"的标签 
if (other.CompareTag("Player")) 
{ 
// 播放音效 
AudioSource.PlayClipAtPoint(coinSound, transform.position); 
// 销毁金币 
Destroy(gameObject); 
} 
} 
}
