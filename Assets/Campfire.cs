using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Campfire : MonoBehaviour {

    public static Campfire Instance { get { return GameObject.FindGameObjectWithTag("Campfire").GetComponent<Campfire>(); } }
    
    float lightIntensityNoise = .15f;
    float lightPositionNoise = 1f;

    Dictionary<FireSize, FireDetails> fireDict = new Dictionary<FireSize, FireDetails>
    {
        {
            FireSize.Embers, new FireDetails {
                FlameTransparency = 0f,
                FlameMaxParticles = 0,
                LightRange = 4f,
                LightIntensity = .25f,
                SparkMaxParticles = 3
            }
        },
        {
            FireSize.Flicker, new FireDetails {
                FlameTransparency = .5f,
                FlameMaxParticles = 4,
                LightRange = 8f,
                LightIntensity = 1f,
                SparkMaxParticles = 8
            }
        },
        {
            FireSize.Burning, new FireDetails {
                FlameTransparency = 1f,
                FlameMaxParticles = 15,
                LightRange = 15f,
                LightIntensity = 4f,
                SparkMaxParticles = 15
            }
        },
        {
            FireSize.Roaring, new FireDetails {
                FlameTransparency = 1f,
                FlameMaxParticles = 20,
                LightRange = 25f,
                LightIntensity = 4f,
                SparkMaxParticles = 50
            }
        }
    };

    public bool ClickEnabled = true;

    public GameObject LightObject;
    public GameObject FlameObject;
    public GameObject SparkObject;
    public GameObject HeatObject;    

    FireSize currentFireSize = FireSize.Flicker;
    float currentLightIntensity, currentLightRange;
    Vector3 currentLightPosition;

    ParticleSystem flameParticleSystem, sparkParticleSystem;
    
    new Light light;
    float random1, random2, random3, noise1, floor, ceiling;

    private void Awake()
    {
        flameParticleSystem = FlameObject.GetComponent<ParticleSystem>();
        sparkParticleSystem = SparkObject.GetComponent<ParticleSystem>();
        light = LightObject.GetComponent<Light>();
        currentLightPosition = LightObject.transform.position;

        currentLightIntensity = 0;
        light.intensity = 0;
        currentLightRange = 0;
        light.range = 0;
        flameParticleSystem.maxParticles = 0;
        sparkParticleSystem.maxParticles = 0;
    }

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {

        // Adding noise light position noise to fire for realism.
        //random1 = Mathf.Lerp(-lightPositionNoise, lightPositionNoise, Mathf.PerlinNoise(Random.Range(0f, 150f), Time.time));
        //random2 = Mathf.Lerp(-lightPositionNoise, lightPositionNoise, Mathf.PerlinNoise(Random.Range(0f, 150f), Time.time));
        //random3 = Mathf.Lerp(-lightPositionNoise, lightPositionNoise, Mathf.PerlinNoise(Random.Range(0f, 150f), Time.time));
        //LightObject.transform.position = new Vector3(currentLightPosition.x + random1, currentLightPosition.y + random2, currentLightPosition.z + random3);
        LightObject.transform.position = currentLightPosition;

        // Adding intensity noise to fire to realism.
        floor = currentLightIntensity - lightIntensityNoise;
        ceiling = currentLightIntensity + lightIntensityNoise;
        random1 = Random.Range(floor, ceiling);
        noise1 = Mathf.PerlinNoise(random1, Time.time);
        light.intensity = Mathf.Lerp(floor, ceiling, noise1);

        //light.intensity = currentLightIntensity;
        light.range = currentLightRange;

    }

    void OnMouseDown()
    {
        if (ClickEnabled)
        {
            GameController.Instance.FireClickListener();
            ChangeFire(true);
        }
    }

    public void ChangeFire(bool increase)
    {
        if (increase && currentFireSize < FireSize.Burning)
        {
            currentFireSize++;
            StartCoroutine(TransitionFire(currentFireSize, true));
        }

        if (!increase && currentFireSize > FireSize.Embers)
        {
            currentFireSize--;
            StartCoroutine(TransitionFire(currentFireSize, false));
        }
    }

    public void ChangeFire(FireSize size)
    {
        currentFireSize = size;
        StartCoroutine(TransitionFire(size, false));
    }

    IEnumerator TransitionFire(FireSize fs, bool stoked)
    {
        float time = 6f;
        float t = 0.0f;

        var fd = fireDict[fs];

        var startRange = light.range;
        var startIntensity = light.intensity;
        var startFlameParticles = flameParticleSystem.maxParticles;
        var startSparkParticles = stoked ? fd.SparkMaxParticles * 5 : sparkParticleSystem.maxParticles;
        
        Color color;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / time);
            var smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            currentLightRange = Mathf.Lerp(startRange, fd.LightRange, smoothT);
            currentLightIntensity = Mathf.Lerp(startIntensity, fd.LightIntensity, smoothT);

            flameParticleSystem.maxParticles = Mathf.RoundToInt(Mathf.Lerp(startFlameParticles, fd.FlameMaxParticles, smoothT));
            sparkParticleSystem.maxParticles = Mathf.RoundToInt(Sinerp(startSparkParticles, fd.SparkMaxParticles, t));
            color = flameParticleSystem.startColor;
            color.a = fd.FlameTransparency;
            flameParticleSystem.startColor = color;
            yield return 0;
        }
    }

    float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    float Coserp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
    }

    class FireDetails
    {
        public float FlameTransparency { get; set; }
        public int FlameMaxParticles { get; set; }
        public float LightRange { get; set; }
        public float LightIntensity { get; set; }
        public int SparkMaxParticles { get; set; }
    }
}

public enum FireSize
{
    Embers,
    Flicker,
    Burning,
    Roaring
}
