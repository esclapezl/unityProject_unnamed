using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScript : MonoBehaviour
{

    public ParticleSystem positivePolarity;
    public ParticleSystem negativePolarity;
    


    public void changePolarity(int polarity) //appelé par levelScript
    {
        if(polarity == 1)
        {
            positivePolarity.Play();
        }
        else if(polarity == -1)
        {
            negativePolarity.Play();
        }
    }
}
