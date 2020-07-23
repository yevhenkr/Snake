using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private Transform burgerPanel;
    [SerializeField] private Transform target;
    [SerializeField] private Transform rightEdgeScreeen;
    [SerializeField] private float durationBurger
        ;

    // Update is called once per frame
    void Start()
    {
       //burgerPanel.position = Vector3.Lerp(rightEdgeScreeen.position,burgerPanel.position, Time.deltaTime*2f);
       burgerPanel.DOMove(target.position, durationBurger, false);
    }
}
