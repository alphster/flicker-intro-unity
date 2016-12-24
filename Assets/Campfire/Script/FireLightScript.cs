using UnityEngine;

public class FireLightScript : MonoBehaviour
{
	public float minIntensity = 0.25f;
	public float maxIntensity = 0.5f;

    public float minRange = 0f;
    public float maxRange = 150f;

    public float positionChange = 1f;

	public Light fireLight;
    //public Light fireLight2;
    //public Light fireLight3;
    
	float random1, random2, random3;
    float noise1, noise2, noise3;
    Vector3 fireLightOriginalPosition;


    private void Start()
    {
        fireLightOriginalPosition = fireLight.transform.position;
    }

    void Update()
	{
        random1 = Mathf.Lerp(-positionChange, positionChange, Mathf.PerlinNoise(Random.Range(minRange, maxRange), Time.time));        
        random2 = Mathf.Lerp(-positionChange, positionChange, Mathf.PerlinNoise(Random.Range(minRange, maxRange), Time.time));        
        random3 = Mathf.Lerp(-positionChange, positionChange, Mathf.PerlinNoise(Random.Range(minRange, maxRange), Time.time));
        fireLight.transform.position = new Vector3(fireLightOriginalPosition.x + random1, fireLightOriginalPosition.y + random2, fireLightOriginalPosition.z + random3);

        random1 = Random.Range(minRange, maxRange);
        noise1 = Mathf.PerlinNoise(random1, Time.time);
        fireLight.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise1);

        /*random = Random.Range(randomRangeMin, randomRangeMax);
        noise = Mathf.PerlinNoise(random, Time.time);
        fireLight2.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        random = Random.Range(randomRangeMin, randomRangeMax);
        noise = Mathf.PerlinNoise(random, Time.time);
        fireLight3.GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        */
    }
}