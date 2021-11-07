using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color lineColor;

    //Contains all the nodes in the list
    private List<Transform> nodes = new List<Transform>();

    //OnDrawGizmosSelected is when the path object is selected, the line and nodes will be visible
    void OnDrawGizmosSelected ()
    {
        //Set line color
        Gizmos.color = lineColor;

        //GetComponentsInChildren find all the child objects
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();

        //Making sure our list is empty at the beginning, so we set this to a new list
        nodes = new List<Transform>();

        //Looping through the array
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            //If the transform is not our own transform, 
            if (pathTransforms[i] != transform)
            {
                //we're going to add it to the node array which node array only contains our child nodes
                nodes.Add(pathTransforms[i]);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            //Refers to current node
            Vector3 currentNode = nodes[i].position;
            //Refers to the previous node
            Vector3 previousNode = Vector3.zero;

            //If is not index 0
            if (i > 0)
            {
                //Will take previous node 
                previousNode = nodes[i - 1].position;
            }//If index is equal to 0 and nodes count > 1
            else if (i == 0 && nodes.Count > 1)
            {
                //Will take the previous node which is the last node in our array list
                //So the total amount of nodes minus 1 because we start counting at 1 and our index starts at 0
                previousNode = nodes[nodes.Count - 1].position;
            }

            //Draw line between the starting node and ending node
            Gizmos.DrawLine(previousNode, currentNode);

            //To draw the circle icon for the node
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }

    }
}
