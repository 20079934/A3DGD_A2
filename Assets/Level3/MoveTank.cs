using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MoveTank : NetworkBehaviour
{

    [SerializeField]
    GameObject tankp, tankr, missilePrefab; 
    Transform endOfCannon;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            GameObject tank = Instantiate(tankr, this.transform.position, this.transform.rotation, this.transform);
            endOfCannon = tank.transform.Find("EndOfCannon");
        }
    }

    public override void OnStartLocalPlayer()
    {
        GameObject tank = Instantiate(tankp, this.transform.position, this.transform.rotation, this.transform);
        endOfCannon = tank.transform.Find("EndOfCannon");


    }
    // Update is called once per frame
    void Update()
    {

        int sprint = Input.GetKey(KeyCode.LeftShift) ? 5 : 1;


        if (!isLocalPlayer) return;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f * sprint;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFireMissile();
        }
    }

    [Command]
    void CmdFireMissile()
    {
        GameObject missile = (GameObject)(Instantiate(missilePrefab, endOfCannon.position, endOfCannon.rotation));
        missile.GetComponent<Rigidbody>().velocity = missile.transform.forward * 6;
        Destroy(missile, 2.0f);
        NetworkServer.Spawn(missile);
    }
}