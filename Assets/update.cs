using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class update : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
               
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Domino")
    //    {
    //        string materialPath = "Assets/green.mat"; // Путь к вашему физическому материалу
    //        Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath); ;//Resources.Load<Material>(materialPath);
    //        other.gameObject.GetComponent<MeshRenderer>().material = material;
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Domino")
        {
            string materialPath = "Assets/green.mat"; // Путь к вашему физическому материалу
            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath); ;//Resources.Load<Material>(materialPath);
            collision.gameObject.GetComponent<MeshRenderer>().material = material;
        }
    }

}
