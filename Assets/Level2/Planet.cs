using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Planet : MonoBehaviour
{
    float rotationalSpeed = 10f;
    float orbitalSpeed = .2f;
    float orbitalAngle = 0.0f;
    float distanceToSun = 150;
    GameObject sun;
    private Color c1 = Color.blue;
    private int lenghthOfLineRenderer = 100;
    void DrawOrbit()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(1.0f, 1.0f);
        lineRenderer.SetVertexCount(lenghthOfLineRenderer + 1);
        int i = 0;
        while (i <= lenghthOfLineRenderer)
        {
            float unityAngle = (float)(2 * 3.14) / lenghthOfLineRenderer;
            float currentAngle = (float)unityAngle * i;
            Vector3 pos = new Vector3(distanceToSun * Mathf.Cos(currentAngle), 0, distanceToSun * Mathf.Sin(currentAngle));
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DrawOrbit();
        sun = GameObject.Find("Sun");
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
}