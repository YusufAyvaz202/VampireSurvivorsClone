using Misc;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Prize", menuName = "ScriptableObjects/Prize")]
    public class PrizeDataSO : ScriptableObject
    {
        public GameObject PrizeObject;
        public PrizeType PrizeType;
        
        public string PrizeName;
        public string PrizeNotCollectedDescription;
        public string PrizeCollectedDescription;
        public string PrizeDescription;
    }
}