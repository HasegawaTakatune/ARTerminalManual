using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manual : MonoBehaviour
{

    [SerializeField] private string Name;

    [SerializeField, TextArea] private string Explanation;

    [SerializeField] private Image[] RealImage = new Image[2];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
