using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Material arrowMaterial;
    [SerializeField] private float conveyorSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector3 direction;
    private Vector2 arrowMaterialIncreaseOffset;
    [SerializeField]
    private List<GameObject> onBelt;
    void Start()
    {
        arrowMaterialIncreaseOffset = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        arrowMaterial.mainTextureOffset += arrowMaterialIncreaseOffset * conveyorSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Buraya girdi");
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
            Debug.Log("Player a carpti");
        onBelt.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(moveSpeed * direction,ForceMode.Impulse);
        }
    }
}
