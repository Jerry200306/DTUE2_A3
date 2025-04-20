using UnityEngine;    // using 放在命名空间外
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Slider slider;
    public AudioSource bgMusic;  // 变量名使用驼峰式

    private void Start()
    {
        // 初始化Slider值
        if (slider != null && bgMusic != null)
        {
            slider.value = bgMusic.volume;
            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)  // 方法名清晰
    {
        if (bgMusic != null)
        {
            bgMusic.volume = volume;     // 补充分号
        }
    }
}