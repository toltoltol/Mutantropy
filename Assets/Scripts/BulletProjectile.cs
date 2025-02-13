using UnityEngine;
using TMPro;

public class BulletProjectile : MonoBehaviour
{
    // How long the projectile exists before being destroyed
    float _lifetime;
    Vector2 _direction;
    float _speed;
    public float damage;

    public GameObject hitPopupText;
    private float _hitPopupOffset = 1f;
    
    public void Init(float lifetime, Vector2 direction, float speed, float attackPower)
    {
        _lifetime = lifetime;
        _direction = direction;
        _speed = speed;
        damage = attackPower;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("EnemyProjectile"))
        {
            SpawnHitPopupText(other.gameObject);
        }
        else if (other.CompareTag("Enemy") && gameObject.CompareTag("PlayerProjectile"))
        {
            SpawnHitPopupText(other.gameObject);
        }
    }

    private void SpawnHitPopupText(GameObject other)
    {
        if (hitPopupText != null)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f * _hitPopupOffset, _hitPopupOffset), Random.Range(-1f * _hitPopupOffset, _hitPopupOffset), 0f);
            Vector3 spawnPosition = other.transform.position + randomOffset;

            GameObject hitPopupTextObject = Instantiate(hitPopupText, spawnPosition, Quaternion.identity);

            TextMeshProUGUI textComponent = hitPopupTextObject.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = damage.ToString("F1");
            }
            Destroy(hitPopupTextObject, 0.2f);
        }
    }
}