using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 0.5f;
    private IntelectCreeps Target;
    [SerializeField]
    private float Damage = 0.3f;
    private TowerScript Tower;

    public void Initialize(IntelectCreeps target, float damage, float speed)
    {
        Target = target;
        Damage = damage;
        Speed = speed;
    }

    void Start()
    {
        Tower = FindObjectOfType<TowerScript>();
    }

    private void FixedUpdate()
    {
        //Moving the projectile towards the target
        if (Target != null) //If the target exists
        {
            Vector3 destination = new Vector3(Target.transform.position.x, Target.transform.position.y + Target.transform.localScale.y / 2, Target.transform.position.z);
            if (Vector3.Distance(transform.position, destination) > 0.01) //If not approached the target yet
            {
                transform.rotation = Quaternion.LookRotation(destination - transform.position); //Rotate towards the target
                transform.position += transform.forward.normalized * Speed * Time.deltaTime; //Go towards the target
            }
            else //If reached the target
            {
                //Damage the target
                Target.GetComponent<IntelectCreeps>().GetDamage(Damage);

                //Put an effect on the target if the tower has one
                switch (Tower.EffectType)
                {
                    case EffectType.Ignite:
                        Target.GetComponent<Effects>().IgniteTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                    case EffectType.Freeze:
                        Target.GetComponent<Effects>().FreezeTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                    case EffectType.Weakness:
                        Target.GetComponent<Effects>().WeaknessTarget(Tower.EffectStrength, Tower.EffectDuration);
                        break;
                }

                //Destroy the projectile
                Destroy(gameObject, 0);
            }
        }
        else //If the target doesn't exist, destroy the projectile
        {
            Destroy(gameObject, 0);
        }
    }
}
