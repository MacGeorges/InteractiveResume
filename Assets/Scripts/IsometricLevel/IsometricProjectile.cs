using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IsometricProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float currentLife;
    private float lifespan = 10;

    public bool isShot { get; private set; }

    public void Shot()
    {
        if(isShot)
        {
            return;
        }
        else
        {
            isShot = true;
            currentLife = 0;
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        graphics.LookAt(Camera.main.transform);

        currentLife += Time.deltaTime;
        if(currentLife > lifespan)
        {
            isShot = false;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ammo touched: " + collision.gameObject);
        isShot = false;
        gameObject.SetActive(false);
    }

    public void EntityTouched()
    {
        isShot = false;
        gameObject.SetActive(false);
    }
}
