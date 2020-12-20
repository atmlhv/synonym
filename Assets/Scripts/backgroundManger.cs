using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManger : MonoBehaviour
{
    [SerializeField]
    GameObject backprefab = default;
    [SerializeField]
    GameObject cloudprefab = default;

    List<GameObject> backlist = new List<GameObject>();

    const float backspeed = 0.001f;
    float backsize;

    // Start is called before the first frame update
    void Start()
    {
        backlist.Add(Instantiate(backprefab));
        backsize = backprefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(backlist[0].transform.position.x <= 0 && backlist.Count <= 1)
        {
            backlist.Add(Instantiate(backprefab, backprefab.transform.position + new Vector3(backsize,0), Quaternion.identity));
        }

        if (backlist[0].transform.position.x + backsize <= 0)
        {
            Destroy(backlist[0]);
            backlist[0] = backlist[1];
            backlist[1] = Instantiate(backprefab, backprefab.transform.position + new Vector3(backsize, 0), Quaternion.identity);
        }

        foreach (var back in backlist) {
            back.transform.position -= new Vector3(backspeed, 0, 0);
        }
    }
}
