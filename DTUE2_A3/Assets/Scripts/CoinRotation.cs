using UnityEngine; 

public class CoinRotation : MonoBehaviour 
{ 
public Vector3 rotationAxis = Vector3.up; // 控制旋转轴 
public float rotationSpeed = 50f; // 控制旋转速度 

void Update() 
{ 
// 围绕指定的轴旋转 
transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime); 
} 
}