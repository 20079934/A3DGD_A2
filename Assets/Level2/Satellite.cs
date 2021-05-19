using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Satellite : MonoBehaviour
{
    float rotationalSpeed = 10f;
    float orbitalSpeed = .2f;
    float orbitalAngle = 0.0f;
    float distanceToSun = 150;
    GameObject sun;
    private Color c1 = Color.blue;
    private int lenghthOfLineRenderer = 100;


    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponentInParent<Transform>().gameObject;
        transform.position = new Vector3(distanceToSun, 0, distanceToSun);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationalSpeed * Time.deltaTime, Space.World);
        float tempx, tempy, tempz;
        orbitalAngle += Time.deltaTime * orbitalSpeed;
        tempx = sun.transform.position.x + distanceToSun * Mathf.Cos(orbitalAngle);
        tempz = sun.transform.position.z + distanceToSun * Mathf.Sin(orbitalAngle);
        tempy = sun.transform.position.y;
        transform.position = new Vector3(tempx, tempy, tempz);
    }

    public void SetRotationalSpeed(float s)
    {
        rotationalSpeed = s * rotationalSpeed;
    }
    public void SetOrbitSpeed(float os)
    {
        orbitalSpeed = os * orbitalSpeed;
    }
    public void SetDistanceToSun(float d)
    {
        distanceToSun = d * distanceToSun;
    }
    public void SetName(string name)
    {
        this.name = name;
        transform.Find("label").GetComponent<TextMesh>().text = name;
    }
    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }
}