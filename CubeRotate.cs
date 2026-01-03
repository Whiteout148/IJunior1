using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        Rotating();   
    }

    private void Rotating()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f) * _rotateSpeed * Time.deltaTime);
    }
}
