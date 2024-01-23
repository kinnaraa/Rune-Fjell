using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // Variables for damage and cooldown
    public int damage;
    public float cooldown;
    public string Name;

    // Constructor to initialize the ability with damage and cooldown values
    public Ability(int damage, float cooldown, string Name)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.Name = Name;
    }

    // Common Cast function (can be overridden by subclasses)
    public virtual IEnumerator Cast()
    {
        yield return null;
    }
}

public class Storm : Ability
{
    private GameObject storm;
    private bool cooldownActive = false;

    public Storm() : base(10, 1, "Storm") // Default values, adjust as needed
    {
    }

    public override IEnumerator Cast()
    {
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            storm = Resources.Load("SpellPrefabs/Storm") as GameObject;
            GameObject effect = Instantiate(storm, gameObject.transform.position, storm.transform.rotation);
            gameObject.GetComponent<PlayerMovement>().readyToJump = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(Lift(0.25f));
            effect.GetComponent<ParticleSystem>().Play();
            effect.GetComponent<BoxCollider>().enabled = true;
            //gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed + 5;

            yield return new WaitForSeconds(10f);

            StartCoroutine(Lower(0.25f));
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<PlayerMovement>().readyToJump = true;
            //gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed - 5;
            effect.GetComponent<ParticleSystem>().Stop();
            effect.GetComponent<BoxCollider>().enabled = false;
            Destroy(effect, 2);
            StartCoroutine(Cooldown());
        }
        else
        {
            yield break;
        }
    }

    public IEnumerator Lift(float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * 0.5f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position
        transform.position = targetPosition;
    }

    public IEnumerator Lower(float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition - Vector3.up * 0.5f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position
        transform.position = targetPosition;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class Ice : Ability
{
    private bool cooldownActive = false;
    private Transform firingPoint;
    public GameObject iceBallPrefab;

    public Ice() : base(10, 1, "Ice") // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        iceBallPrefab = Resources.Load("SpellPrefabs/IceBall") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        if(!cooldownActive)
        {
            GameObject newIceBall = Instantiate(iceBallPrefab, firingPoint.position, firingPoint.rotation);
            newIceBall.SetActive(true);
            Rigidbody iceBallRb = newIceBall.GetComponent<Rigidbody>();
            iceBallRb.velocity = firingPoint.transform.forward * 20;

            StartCoroutine(Cooldown());
            yield return null;
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class FireBlast : Ability
{
    private bool cooldownActive = false;
    private Transform firingPoint;
    public GameObject fireBlastPrefab;

    public FireBlast() : base(10, 1, "Fire Blast") // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        fireBlastPrefab = Resources.Load("SpellPrefabs/FireBlast") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        if(!cooldownActive)
        {
            cooldownActive = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            GameObject fireBlast = Instantiate(fireBlastPrefab, firingPoint.position, firingPoint.rotation);
            Destroy(fireBlast, 1.2f);
            yield return new WaitForSeconds(1.5f);  
            StartCoroutine(Cooldown());
            gameObject.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}