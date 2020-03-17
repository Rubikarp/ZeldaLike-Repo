using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface Int_EnnemisLifeSystem : Int_LifeSystem
    {        
        bool IsBleeding { get; set; }
    }
}