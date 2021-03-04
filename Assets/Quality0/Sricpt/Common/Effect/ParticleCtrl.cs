using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleCtrl : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> particleList = new List<ParticleSystem>();
    // Update is called once per frame
    void Update()
    {
        if (this.particleList != null)
        {
            if (!particleList.Any(particle => particle.isPlaying))
            {
                Destroy(gameObject);
            }
        }
    }
}
