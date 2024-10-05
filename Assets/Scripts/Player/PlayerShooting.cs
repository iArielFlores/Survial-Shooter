using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;

    private bool isFiring; 

    void Awake()
    {
        
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        
        DisableEffects();
    }

    
    void OnEnable()
    {
        var playerInput = new PlayerInputActions();
        playerInput.Player.Shoot.performed += ctx => OnShootStart(ctx);
        playerInput.Player.Shoot.canceled += ctx => OnShootEnd(ctx);
        playerInput.Enable();
    }

    void OnDisable()
    {
        var playerInput = new PlayerInputActions();
        playerInput.Player.Shoot.performed -= ctx => OnShootStart(ctx);
        playerInput.Player.Shoot.canceled -= ctx => OnShootEnd(ctx);
        playerInput.Disable();
    }

    
    private void OnShootStart(InputAction.CallbackContext context)
    {
        isFiring = true;
    }

    
    private void OnShootEnd(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    void Update()
    {
        
        if (isFiring && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

       
        timer += Time.deltaTime;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        
        gunAudio.Play();
        gunLight.enabled = true;

        
        gunParticles.Stop();
        gunParticles.Play();

        
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
