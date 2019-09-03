using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicPathfindingAI : MonoBehaviour
{
    public List<GameObject> Nodes;
    public NavMeshAgent Agent;
    public bool ChasePlayer;
    
    private int _currentNode;
    private GameObject _player;
    private System.Random _rand;
    
    public void Start()
    {
        _rand = new System.Random();
        
        if (ChasePlayer)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            var ind = _rand.Next(players.Length);

            _player = players[ind];
            Agent.destination = _player.transform.position;
        }
        else
        {
            Agent.destination = Nodes[0].transform.position;
            _currentNode = 0;
        }
    }
    public void Update()
    {
        if (ChasePlayer)
        {
            Agent.destination = _player.transform.position;
        }
        else if(Vector3.Distance(transform.position, Nodes[_currentNode].transform.position) < 0.25f)
        {
            if(_currentNode >= Nodes.Count-1)
            {
                _currentNode = 0;
            }
            else
            {
                _currentNode++;
            }
            
            Agent.destination = Nodes[_currentNode].transform.position;
        }
    }
}
