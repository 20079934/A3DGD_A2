using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class LoadPlanets : MonoBehaviour
{
    public GameObject planetTemplate;
    [SerializeField]
    GameObject satellite;
    // Start is called before the first frame update
    void Start()
    {
        LoadAllPlanets();
        hookSatellites();
    }


    void LoadAllPlanets()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("planets");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        foreach (XmlNode planet in doc.SelectNodes("planets/planet"))
        {
            string name, diameter, distanceToSun, rotationPeriod, orbitalVelocity;
            name = planet.Attributes.GetNamedItem("name").Value;
            diameter = planet.Attributes.GetNamedItem("diameter").Value;
            distanceToSun = planet.Attributes.GetNamedItem("distancetoSun").Value;
            rotationPeriod = planet.Attributes.GetNamedItem("rotationPeriod").Value;
            orbitalVelocity = planet.Attributes.GetNamedItem("orbitalVelocity").Value;
            Debug.Log("Planet Name: " + name);
            float diameter2, distanceToSun2, rotationPeriod2, orbitalVelocity2;
            diameter2 = float.Parse(diameter);
            distanceToSun2 = float.Parse(distanceToSun);
            rotationPeriod2 = float.Parse(rotationPeriod);
            orbitalVelocity2 = float.Parse(orbitalVelocity);
            print("Planet" + name + ", Diameter=" + diameter2 + ", Distance=" + distanceToSun2);
            GameObject g = Instantiate(planetTemplate);
            g.GetComponent<Planet>().SetDistanceToSun(distanceToSun2);
            g.GetComponent<Planet>().SetOrbitSpeed(orbitalVelocity2);
            g.GetComponent<Planet>().SetRotationalSpeed(1 / rotationPeriod2);
            g.GetComponent<Planet>().SetRadius(diameter2);
        }
    }

    void hookSatellites()
    {
        Planet[] sat = GameObject.FindObjectsOfType<Planet>();

        for(int i = 0; i<4; i++)
        {
            GameObject pl = sat[Random.Range(0, sat.Length)].gameObject;
            Instantiate(satellite, Vector3.zero, Quaternion.identity, pl.transform);
        }
    }
}