using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector2 destiny;
    public float speed = 1;
    public float distanceThreshold = 0.01f;

    public float distance = 0.5f;

    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;
    private SpriteRenderer spriteRenderer;
    public LineRenderer lr;
    public int vertexCount = 2;
    public List<GameObject> nodes = new List<GameObject>();
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();

        nodes.Add(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destiny, speed);

        if ((Vector2)transform.position != destiny)
        {
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
              
            }

        }
        else if (done == false)
        {
            done = true;

            while (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
              
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }
        RenderLine();
    }
    void RenderLine()
    {
        lr.positionCount = nodes.Count + 1;

        for (int i = 0; i < nodes.Count; i++)
        {

            lr.SetPosition(i, nodes[i].transform.position);
        }
        lr.SetPosition(nodes.Count, player.transform.position);
    }
    void CreateNode()
    {
        Vector2 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();

        lastNode = go;

        nodes.Add(lastNode);
       
        vertexCount++;
    }
  
}


