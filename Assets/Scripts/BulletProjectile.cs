using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    // How long the projectile exists before being destroyed
    float _lifetime;
    Vector2 _direction;
    float _speed;

    
    public void Init(float lifetime, Vector2 direction, float speed)
    {
        _lifetime = lifetime;
        _direction = direction;
        _speed = speed;
        Destroy(gameObject, _lifetime);
    }
    
    // Function to inform projectile what way it is going lol 
    //Deprecated
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        _lifetime = 5f;
        _speed = 10f;
        Destroy(gameObject, _lifetime);
    }
    
    void Update()
    {
        // Move the projectile in the set direction
        transform.Translate( _speed * Time.deltaTime * _direction);
    }
}