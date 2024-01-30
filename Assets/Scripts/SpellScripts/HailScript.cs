using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailScript : MonoBehaviour
{
    public Transform[] starts;
    public GameObject hailBall;
    public bool firing;

    // Update is called once per frame
    void Update()
    {
        if(firing)
        {
            StartCoroutine(Hailing());
        }
    }

    public IEnumerator Hailing()
    {
        hailBall = Resources.Load("SpellPrefabs/IceBall") as GameObject;
        // Generate a random index within the range of the array length
        int randomIndex = Random.Range(0, starts.Length);

        for(int i = 0; i < starts.Length; i++)
        {
            // Get the Transform at the random index
            Transform randomTransform = starts[randomIndex];

            // Instantiate the prefab at the random Transform's position and rotation
            GameObject hailball = Instantiate(hailBall, randomTransform.position, randomTransform.rotation); 
            Rigidbody hailballRB = hailball.GetComponent<Rigidbody>();
            hailballRB.velocity = hailballRB.transform.forward * 15;

            yield return new WaitForSeconds(0.1f);
        }
        firing = false;
    }
}