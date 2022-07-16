using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    None, Ignite, Freeze, Weakness, Mechanical
}

public class Effects : MonoBehaviour
{
    //Negative effects coroutines
    IEnumerator igniteEffect;
    IEnumerator freezeEffect;
    IEnumerator weaknessEffect;

    private float originalDamage;
    private float originalSpeed;

    //Target of an effect (self)
    IntelectCreeps Target;


    void Start()
    {
        Target = GetComponent<IntelectCreeps>();
        originalDamage = Target.attackDamage;
        originalSpeed = Target.Speed;
    }

    /// <summary>
    /// The weakness effect. Decreases the damage
    /// </summary>
    /// <param name="weaknessPercentage">Amount of damage decreasing in percents</param>
    /// <param name="duration">Duration of the effect in seconds</param>
    IEnumerator Weakness(float weaknessPercentage, float duration)
    {
        //Decrease the damage by <weaknessPercentage> percents
        Target.attackDamage = originalDamage * (1 - weaknessPercentage / 100);

        //Wait for <duration> seconds
        yield return new WaitForSeconds(duration);

        //Get damage back to original
        Target.attackDamage = originalDamage;
    }

    /// <summary>
    /// The freeze effect. Slows down
    /// </summary>
    /// <param name="freezePercentage">Slow strength in percents</param>
    /// <param name="duration">Duration of the effect in seconds</param>
    IEnumerator Freeze(float freezePercentage, float duration)
    {
        //Decrease the speed by <freezePercentage> percents
        Target.Speed = originalSpeed * (1 - freezePercentage / 100);

        //Wait for <duration> seconds
        yield return new WaitForSeconds(duration);

        //Get the speed back to original
        Target.Speed = originalSpeed;
    }

    /// <summary>
    /// The igniting effect. Deals damage over time
    /// </summary>
    /// <param name="percentageOfMaxHealth">Amount of damage dealing in percents of max health</param>
    /// <param name="duration">Duration of the effect in seconds</param>
    IEnumerator Igniting(float percentageOfMaxHealth, float duration)
    {
        float timer = duration;
        float fireDamage = Target.maxHealthPoint * (percentageOfMaxHealth / 100);

        //While <duration> is not over, deal <percentageOfMaxHealth> percents of max health of damage every second
        while (timer > 0)
        {
            Target.GetDamage(fireDamage);
            timer -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Set the target on fire
    /// </summary>
    /// <param name="_strength">Fire damage in percents of max health</param>
    /// <param name="_time">Duration of the effect in seconds</param>
    public void IgniteTarget(float _strength, float _time)
    {
        //If there already is an ignite effect, clear it and set the new one
        if (igniteEffect != null)
            StopCoroutine(igniteEffect);

        igniteEffect = Igniting(_strength, _time);
        StartCoroutine(igniteEffect);
    }

    /// <summary>
    /// Freeze the target
    /// </summary>
    /// <param name="_freezePercentage">Slow strength in percents</param>
    /// <param name="_duration">Duration of the effect in seconds</param>
    public void FreezeTarget(float _freezePercentage, float _duration)
    {
        //If there already is a freeze effect, clear it and set the new one
        if (freezeEffect != null)
            StopCoroutine(freezeEffect);

        Target.speed = originalSpeed;
        freezeEffect = Freeze(_freezePercentage, _duration);
        StartCoroutine(freezeEffect);
    }

    /// <summary>
    /// Weacken the target
    /// </summary>
    /// <param name="_weaknessPercentage">Weackness strength in percents</param>
    /// <param name="_duration">Duration of the effect in seconds</param>
    public void WeaknessTarget(float _weaknessPercentage, float _duration)
    {
        //If there already is a weackness effect, clear it and set the new one
        if (weaknessEffect != null)
            StopCoroutine(weaknessEffect);

        Target.attackDamage = originalDamage;
        weaknessEffect = Weakness(_weaknessPercentage, _duration);
        StartCoroutine(weaknessEffect);
    }
}
