using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 14f;
    [SerializeField] private float topBoundary = 6.5f; // 超出屏幕上方的阈值

    private void Update()
    {
        // 沿 Y 轴向上移动，不使用重力物理
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // 超出屏幕立即销毁，防止内存泄漏和 Hierarchy 堆积
        if (transform.position.y > topBoundary)
        {
            Destroy(gameObject);
        }
    }
}
