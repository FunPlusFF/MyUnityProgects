using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Shooting : MonoBehaviour //by serega
{
    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private GameObject Gun; //The place where projectiles spawn
    private LayerMask Mask; //The layer mask for aiming raycast

    public float ProjectileSpeed = 0.1f;
    public float ShootingRate = 1; //Shots per second
    public float ProjectileDamage = 0.4f;

    [SerializeField]
    private IntelectCreeps target;
    private IntelectCreeps Target
    {
        get
        {
            return target;
        }
        set //Deselect the old target and select the new one when switching targets
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
    private IEnumerator ShootingProcess;

    [SerializeField]
    private Camera Camera;

    private TextMeshProUGUI DamageLabel, AttackSpeedLabel;

    void Start()
    {
        Spawner = FindObjectOfType<Spawnr>();
        Mask = LayerMask.GetMask("Enemy");
        Camera = FindObjectOfType<Camera>();
        DamageLabel = GameObject.Find("DamageLabel").GetComponent<TextMeshProUGUI>();
        AttackSpeedLabel = GameObject.Find("AttackSpeedLabel").GetComponent<TextMeshProUGUI>();
        ShootingProcess = ShootingCoroutine();
    }

    void Update()
    {
        DamageLabel.text = "Damage: " + ProjectileDamage;
        AttackSpeedLabel.text = "Attack speed: " + ShootingRate;
        SwitchingTarget();
    }

    /// <summary>
    /// The shooting coroutine
    /// </summary>
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
    }

    /// <summary>
    /// Switches to the target at the center of the screen, if there is any
    /// </summary>
    public void AimToTarget()
    {
        //Ray from camera to the center of the screen
        Ray ray = Camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

        //If hit a target, and if it's a new one, switch to it
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Mask) && hit.collider.gameObject.GetComponent<IntelectCreeps>() != Target)
        {
            StopCoroutine(ShootingProcess);
            IntelectCreeps newTarget = hit.collider.gameObject.GetComponent<IntelectCreeps>();
            Target = newTarget;
            StartCoroutine(ShootingProcess);
        }
    }

    /// <summary>
    /// Automaticaly shoot the closest targets
    /// </summary>
    private void SwitchingTarget()
    {
        if (!IsShooting && Spawner.SpawnedEnemies.Count > 0)//If not shooting and there still are targets to shoot
        {
            StopCoroutine(ShootingProcess);
            float distance = 100;
            GameObject targ = null;

            //Choose the closest target
            foreach (GameObject enemy in Spawner.SpawnedEnemies)
            {
                float newDistance = Vector3.Distance(enemy.transform.position, transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    targ = enemy;
                }
            }

            //And start shooting it
            Target = targ.GetComponent<IntelectCreeps>();
            ShootingProcess = ShootingCoroutine();
            StartCoroutine(ShootingProcess);
        }

        if (Input.GetMouseButtonDown(0)) //Switching target by left clicking on it (for testing purposes only)
        {
            Ray TheRay = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(TheRay, out RaycastHit hit, Mathf.Infinity, Mask) && hit.collider.gameObject.GetComponent<IntelectCreeps>() != Target)
            {
                StopCoroutine(ShootingProcess);
                Target = hit.collider.gameObject.GetComponent<IntelectCreeps>();
                ShootingProcess = ShootingCoroutine();
                StartCoroutine(ShootingProcess);
            }
        }

    }
}