using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceRollScript : MonoBehaviour
{
    Rigidbody rBody;
    Vector3 position;
   [SerializeField] private float maxRandForceVal, startRollForce;
    float forceX, forceY, forceZ;
    public string diceFaceNum;
    public bool isLanded = false;
    public bool firstThrow = false;


    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

   void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.isKinematic = true;
        position = transform.position;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (rBody != null) { 
            if(Input.GetMouseButtonDown(0) && isLanded || Input.GetMouseButtonDown(0) && !firstThrow)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider != null && hit.collider.gameObject == this.gameObject)
                    {
                        if (!firstThrow)
                            firstThrow = true;

                        DiceRoll();
                    }
                }
            }
        }


    }
    public void DiceRoll()
    {
        rBody.isKinematic = false;
        forceX = Random.Range(0,maxRandForceVal);
        forceY = Random.Range(0,maxRandForceVal);
        forceZ = Random.Range(0,maxRandForceVal);
        rBody.AddForce(Vector3.up * Random.Range(800, startRollForce));
        rBody.AddTorque(forceX, forceY, forceZ);
    }
    public void ResetDice()
    {
        rBody.isKinematic = true;
        firstThrow = false;
        isLanded = false;
        transform.position = position;
    }
}
