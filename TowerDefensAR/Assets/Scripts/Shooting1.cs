using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public enum ShootingMode
{
    Manual, Automatic
}

public class Shooting1 : MonoBehaviour //by serega
{
    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private GameObject Gun;
    private LayerMask Mask;
    private ARRaycastManager RaycastManager;

    public float ProjectileSpeed = 0.1f;
    public float ShootingRate = 1; //Shots per second
    public float ProjectileDamage = 0.4f;

    [SerializeField]
    private ShootingMode Mode = ShootingMode.Automatic;
    [SerializeField]
    private IntelectCreeps target;
    private IntelectCreeps Target
    {
        get
        {
            return target;
        }
        set
        {
            if (target != null)
            {
                target.Deselect();
            }
            if (value == null)
            {
                IsShooting = false;
            }
            else
            {
                value.Select();
            }
            target = value;
        }
    }
    private Spawnr Spawner;

    private bool IsShooting = false;
    private IEnumerator Shooting;


    [SerializeField]
    private Camera Camera;

    private void OnDrawGizmos()
    {
    }


    void Start()
    {
        Spawner = FindObjectOfType<Spawnr>();
        Mask = LayerMask.GetMask("Enemy");
        Camera = FindObjectOfType<Camera>();
        Shooting = ShootingCoroutine();
    }

    private void SwitchShootingMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Mode == ShootingMode.Manual)
                Mode = ShootingMode.Automatic;
            else if (Mode == ShootingMode.Automatic)
                Mode = ShootingMode.Manual;
        }
    }

    private IEnumerator ShootingCoroutine()
    {
        IsShooting = true;
        while (Target != null && Target.IsAlive)
        {
            GameObject projectile = GameObject.Instantiate(ProjectilePrefab, Gun.transform.position, Quaternion.identity);
            Projectile projComp = projectile.GetComponent<Projectile>();
            projComp.Initialize(Target, ProjectileDamage, ProjectileSpeed);

            yield return new WaitForSeconds(1 / ShootingRate);
        }
        IsShooting = false;
        if (Target != null)
            Target.Deselect();
        Mode = ShootingMode.Automatic;
    }

    public void AimToTarget(int ss)
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Mask))
        {
            Mode = ShootingMode.Manual;
            StopCoroutine(Shooting);
            IntelectCreeps newTarget = hit.collider.gameObject.GetComponent<IntelectCreeps>();
            Target = newTarget;
            StartCoroutine(Shooting);
        }
    }

    private void ShootProjectile()
    {
        if (Mode == ShootingMode.Automatic)
        {
            if (Spawner.SpawnedEnemies.Count > 0)
            {
                if (!IsShooting)
                {
                    StopCoroutine(Shooting);
                    float distance = 100;
                    GameObject targ = null;
                    foreach (GameObject enemy in Spawner.SpawnedEnemies)
                    {
                        float newDistance = Vector3.Distance(enemy.transform.position, transform.position);
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            targ = enemy;
                        }
                    }
                    Target = targ.GetComponent<IntelectCreeps>();
                    Shooting = ShootingCoroutine();
                    StartCoroutine(Shooting);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray TheRay = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(TheRay, out RaycastHit hit, Mathf.Infinity, Mask))
            {
                Mode = ShootingMode.Manual;
                if (hit.collider.gameObject.GetComponent<IntelectCreeps>() != Target)
                {
                    StopCoroutine(Shooting);
                    Target = hit.collider.gameObject.GetComponent<IntelectCreeps>();
                    Shooting = ShootingCoroutine();
                    StartCoroutine(Shooting);
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        ShootProjectile();
        SwitchShootingMode();
    }
}