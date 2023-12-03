using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public float limit = 75f;
    public bool randomstart = false;
    private float random = 0;
    private void Awake()
    {
        if (randomstart)
        {
            random = Random.Range(0f,1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angle = limit * Mathf.Sin(Time.time * speed + random);
        transform.localRotation = Quaternion.Euler(90, angle, 0f);
    }


}
