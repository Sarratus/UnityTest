using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollection : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> leftParticles;
    [SerializeField]
    private List<ParticleSystem> rightParticles;

    [SerializeField]
    private FighterController targetFighter;
    [SerializeField]
    private Transform objectToAttach;

    private void Start() {
        transform.parent = objectToAttach;
        transform.localPosition = Vector3.zero;

        targetFighter.GetPunch += Play;
    }

    public void Play(int damage, bool isLeftPunch) {
        var targetParticles = isLeftPunch ? rightParticles: leftParticles;
        foreach(var particle in targetParticles) particle.Play();
    }
}
