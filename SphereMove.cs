
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(1f, 0f, 0f) * _speed * Time.deltaTime);
    }
}
