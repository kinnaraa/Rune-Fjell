using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{   
    public int damage;
    public float cooldown;
    public string Name;
    public float pauseTime = 0;
    public string DefaultIcon;
    public string ActivatedIcon;
    public int Modifier;

    public Ability(string DefaultIcon, string ActivatedIcon, int damage, float cooldown, string Name, float pauseTime, int Modifier)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.Name = Name;
        this.pauseTime = pauseTime;
        this.DefaultIcon = DefaultIcon;
        this.ActivatedIcon = ActivatedIcon;
        this.Modifier = Modifier;
    }

    public virtual IEnumerator Cast()
    {
        yield return null;
    }
}

public class Null : Ability
{
    public Null() : base("Socket", "Socket", 0, 0, "Null", 0, 0)
    {
    }
}

public class Storm : Ability
{
    private GameObject storm;
    private bool cooldownActive = false;

    public Storm() : base("Halagaz_Default", "Halagaz_Activated", 10, 1, "Storm", 1, 0) // Default values, adjust as needed
    {
    }

    public override IEnumerator Cast()
    {
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            cooldownActive = true;
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

    public Ice() : base("Isa_Default","Isa_Activated", 10, 1, "Ice", 1, 2) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        iceBallPrefab = Resources.Load("SpellPrefabs/Ice") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        if(!cooldownActive)
        {
            cooldownActive = true;
            GameObject newIceBall = Instantiate(iceBallPrefab, firingPoint.position, firingPoint.rotation);
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

    public FireBlast() : base("Sowilo_Default", "Sowilo_Activated", 10, 1, "FireBlast", 1.5f, 1) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        fireBlastPrefab = Resources.Load("SpellPrefabs/FireBlast") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        if(!cooldownActive)
        {
            cooldownActive = true;
            GameObject fireBlast = Instantiate(fireBlastPrefab, firingPoint.position, firingPoint.rotation);
            Destroy(fireBlast, 1.2f);
            yield return new WaitForSeconds(1.5f);  
            StartCoroutine(Cooldown());
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

public class RadialFireBurst : Ability
{
    private bool cooldownActive = false;
    private Transform firingPoint;
    public GameObject RadialFireBurstPrefab;

    public RadialFireBurst() : base("ThurisazSowilo_Default", "ThurisazSowilo_Activated", 10, 1, "RadialFireBurst", 5.5f, 1) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        RadialFireBurstPrefab = Resources.Load("SpellPrefabs/RadialFireBurst") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        if(!cooldownActive)
        {
            cooldownActive = true;
            GameObject RadialFireBurst = Instantiate(RadialFireBurstPrefab, firingPoint.position, RadialFireBurstPrefab.transform.rotation);
            Destroy(RadialFireBurst, 5.5f);
            yield return new WaitForSeconds(5.5f);  
            StartCoroutine(Cooldown());
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

public class EarthSpike : Ability
{
    private bool cooldownActive = false;
    public GameObject EarthSpikePrefab;

    public EarthSpike() : base("Ehwaz_Default", "Ehwaz_Activated", 10, 1, "EarthSpike", 0.5f, 3) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        EarthSpikePrefab = Resources.Load("SpellPrefabs/EarthSpike") as GameObject;
        Transform Player = GameObject.Find("PlayerModel").transform;
        if(!cooldownActive)
        {
            cooldownActive = true;
            Vector3 pos = new Vector3(Player.position.x, Player.position.y - 4, Player.position.z);
            Quaternion rot = Quaternion.Euler(-20, Player.eulerAngles.y, 0);
            GameObject EarthSpike = Instantiate(EarthSpikePrefab, pos, rot);
            Rigidbody EarthSpikeRb = EarthSpike.GetComponent<Rigidbody>();
            EarthSpikeRb.velocity = EarthSpike.transform.forward * 10;
            
            yield return new WaitForSeconds(0.5f); 

            EarthSpikeRb.velocity = Vector3.zero;
            EarthSpikeRb.isKinematic = true;
            Destroy(EarthSpike, 1f);

            StartCoroutine(Cooldown());
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

public class Shield : Ability
{
    public GameObject ShieldPrefab;
    private Transform firingPoint;
    public Shield() : base("Algiz_Default", "Algiz_Activated", 0, 0, "Shield", 0.0f, 0) // Default values for damage, cooldown, and name
    {
    }
    public override IEnumerator Cast()
    {
        ShieldPrefab = Resources.Load("SpellPrefabs/Shield") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;

        GameObject Shield = Instantiate(ShieldPrefab, firingPoint.position, firingPoint.rotation);

        Destroy(Shield, 0.2f);
        yield return null;
    }
}

public class ForceField : Ability
{
    private bool cooldownActive = false;
    public GameObject forceFieldPrefab;
    public ForceField() : base("Algiz_Default", "Algiz_Activated", 0, 5, "ForceField", 0.1f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        forceFieldPrefab = Resources.Load("SpellPrefabs/ForceField") as GameObject;
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            cooldownActive = true;
            GameObject forceField = Instantiate(forceFieldPrefab, pos, gameObject.transform.rotation);
            Destroy(forceField, 5f);

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

public class HealingForceField : Ability
{
    private bool cooldownActive = false;
    public GameObject forceFieldPrefab;
    public HealingForceField() : base("WunjoAlgiz_Default", "WunjoAlgiz_Activated", 0, 5, "HealingForceField", 0.1f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        forceFieldPrefab = Resources.Load("SpellPrefabs/HealingForceField") as GameObject;
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            cooldownActive = true;
            GameObject forceField = Instantiate(forceFieldPrefab, pos, gameObject.transform.rotation);
            Destroy(forceField, 5f);

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

public class DamageForceField : Ability
{
    private bool cooldownActive = false;
    public GameObject forceFieldPrefab;
    public DamageForceField() : base("UruzAlgiz_Default", "UruzAlgiz_Activated", 0, 5, "DamageForceField", 0.1f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        forceFieldPrefab = Resources.Load("SpellPrefabs/DamageForceField") as GameObject;
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            cooldownActive = true;
            GameObject forceField = Instantiate(forceFieldPrefab, pos, gameObject.transform.rotation);
            Destroy(forceField, 5f);

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

public class Light : Ability
{
    public GameObject LightPrefab;
    private Transform firingPoint;
    public Light() : base("Kenaz_Default", "Kenaz_Activated", 0, 0, "Light", 0.0f, 0) // Default values for damage, cooldown, and name
    {
    }
    public override IEnumerator Cast()
    {
        LightPrefab = Resources.Load("SpellPrefabs/Light") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;

        GameObject Light = Instantiate(LightPrefab, firingPoint.position, firingPoint.rotation);

        Destroy(Light, 0.2f);
        yield return null;
    }
}

public class Wall : Ability
{
    public GameObject WallPrefab;
    private Transform firingPoint;
    private bool cooldownActive;

    public Wall() : base("IsaEhwaz_Default", "IsaEhwaz_Activated", 0, 0, "Wall", 0.5f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        WallPrefab = Resources.Load("SpellPrefabs/Wall") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        
        if(!cooldownActive)
        {
            cooldownActive = true;
            Vector3 pos = new Vector3(firingPoint.position.x, firingPoint.position.y - 4, firingPoint.position.z);
            GameObject wall = Instantiate(WallPrefab, pos, firingPoint.transform.rotation);
            Rigidbody wallRB = wall.GetComponent<Rigidbody>();
            wallRB.velocity = wall.transform.up * 30;
            
            yield return new WaitForSeconds(0.5f); 

            wallRB.velocity = Vector3.zero;
            Destroy(wall, 5f);

            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class Hail : Ability
{
    public GameObject HailPrefab;
    private Transform firingPoint;
    private Transform Player;
    private bool cooldownActive;
    public Hail() : base("IsaHalagaz_Default", "IsaHalagaz_Activated", 0, 5, "Hail", 1f, 2) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        HailPrefab = Resources.Load("SpellPrefabs/HailCloud") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;
        Player = GameObject.Find("PlayerModel").transform;
        
        if(!cooldownActive)
        {
            cooldownActive = true;
            Vector3 pos = new Vector3(Player.position.x, Player.position.y + 10, Player.position.z);
            Quaternion rot = Quaternion.Euler(80, Player.eulerAngles.y, 0);
            GameObject hail = Instantiate(HailPrefab, pos, rot);
            hail.GetComponent<HailScript>().firing = true;

            Destroy(hail, 5f);
            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class LightningSmites : Ability
{
    public GameObject LightningSmitesPrefab;
    private Transform Player;
    private bool cooldownActive;

    public LightningSmites() : base("ThurisazHalagaz_Default", "ThurisazHalagaz_Activated", 10, 5, "LightningSmites", 1f, 1) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        LightningSmitesPrefab = Resources.Load("SpellPrefabs/LightningSmitesSpawner") as GameObject;
        Player = GameObject.Find("PlayerModel").transform;
        
        if(!cooldownActive)
        {
            cooldownActive = true;
            Vector3 spawnPosition = Player.position + Player.forward * 5;
            spawnPosition.y -= 2;
            GameObject lightningSmites = Instantiate(LightningSmitesPrefab, spawnPosition, LightningSmitesPrefab.transform.rotation);
            lightningSmites.GetComponent<LightningSmitesScript>().firing = true;

            Destroy(lightningSmites, 5f);
            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class EnergyBlast : Ability
{
    public GameObject Blast;
    private Transform firingPoint;
    private bool cooldownActive;

    public EnergyBlast() : base("Thurisaz_Default", "Thurisaz_Activated", 10, 1, "EnergyBlast", 1f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        Blast = Resources.Load("SpellPrefabs/EnergyBlast") as GameObject;
        firingPoint = GameObject.Find("FiringPoint").transform;

        if (!cooldownActive)
        {
            cooldownActive = true;

            GameObject blast = Instantiate(Blast, firingPoint.position, firingPoint.transform.rotation);

            Destroy(blast, 1f);

            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class RadialBlast : Ability
{
    private Transform playerTransform;
    public GameObject RadiusBlast;
    private bool cooldownActive;

    public RadialBlast() : base("ThurisazUruz_Default", "ThurisazUruz_Activated", 0, 1, "Radial Blast", 1f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        RadiusBlast = Resources.Load("SpellPrefabs/RadiusBlast") as GameObject;
        playerTransform = GameObject.Find("PlayerModel").transform;

        if (!cooldownActive)
        {
            cooldownActive = true;
            
            Vector3 spawnPos = playerTransform.position;
            spawnPos.y = spawnPos.y - 0.8f;

            GameObject radiusBlast = Instantiate(RadiusBlast, spawnPos, RadiusBlast.transform.rotation);

            Destroy(radiusBlast, 1f);

            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class Heal : Ability
{
    private Player PH;
    private bool cooldownActive;

    public Heal() : base("Wunjo_Default", "Wunjo_Activated", 0, 5, "Heal", 0.5f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        if (!cooldownActive)
        {
            cooldownActive = true;
            
            PH = GameObject.Find("Player").GetComponent<Player>();
            PH.PlayerHealth += 10; 

            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class StamRegen : Ability
{
    private Player PH;
    private bool cooldownActive;

    public StamRegen() : base("Nauthiz_Default", "Nauthiz_Activated", 0, 5, "StamRegen", 0.5f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        if (!cooldownActive)
        {
            cooldownActive = true;
            
            PH = GameObject.Find("Player").GetComponent<Player>();
            PH.StamRegenRate += 20; 


            StartCoroutine(Cooldown());
        }

        yield return null;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        PH.StamRegenRate -= 20;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}

public class Damage : Ability
{
    private PlayerMagic PH;
    private bool cooldownActive;

    public Damage() : base("Uruz_Default", "Uruz_Activated", 0, 10, "Damage", 0.5f, 0) // Default values for damage, cooldown, and name
    {
    }

    public override IEnumerator Cast()
    {
        if (!cooldownActive)
        {
            cooldownActive = true;
            
            PH = GameObject.Find("Player").GetComponent<PlayerMagic>();
            PH.damageModifier += 2; 

            StartCoroutine(Cooldown());
        }

        yield return null;
    }

    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(5f);
        PH.damageModifier += 0; 
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
        
    }
}

public class Sun : Ability
{
    public GameObject SunPrefab;
    private Vector3 firingPoint;
    private bool cooldownActive;
    public Sun() : base("SowiloKenaz_Default", "SowiloKenaz_Activated", 0, 4, "Sun", 0.5f, 3) // Default values for damage, cooldown, and name
    {
    }
    public override IEnumerator Cast()
    {
        if (!cooldownActive)
        {
            SunPrefab = Resources.Load("SpellPrefabs/Sun") as GameObject;
            firingPoint = GameObject.Find("Player").transform.position;

            Vector3 spawnPosition = firingPoint;
            spawnPosition.y += 2f;

            GameObject Sun = Instantiate(SunPrefab, spawnPosition, SunPrefab.transform.rotation);
            Sun.transform.parent = null;

            // Get all colliders within the detection radius of the player
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10);

            // Iterate through each collider found
            foreach (Collider collider in colliders)
            {
                // Check if the collider has the EnemyHealth script attached
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    Instantiate(Resources.Load<GameObject>("Modifiers/Stunned"), collider.gameObject.transform.position, Resources.Load<GameObject>("Modifiers/Stunned").transform.rotation).transform.parent = collider.gameObject.transform;
                }
            }

            StartCoroutine(Cooldown());

            Destroy(Sun, 2f);
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