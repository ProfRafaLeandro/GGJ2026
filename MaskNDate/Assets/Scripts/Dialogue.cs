using UnityEngine;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    [SerializeField] public int speachNumber;
    [SerializeField] public List<string> speach;
    [SerializeField] public List<int> who;
    [SerializeField] public List<char> expectedExpression;
    [SerializeField] public List<char> feeling;
    [SerializeField] public List<int> lovePoints;
}
