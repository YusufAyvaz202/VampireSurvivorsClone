using Abstract;
using UnityEngine;

namespace Guns
{
    public class Axe : BaseGun
    {
        public override void Attack()
        {
            Debug.Log("Axe Attack");
        }
    }
}