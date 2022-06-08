using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DataLayer.StaticData.Bug
{
    // should be abstract, but unity says no
    
    public class BugTask : ScriptableObject
    {
        [ReadOnlyField]
        public BugType Type;
        [Header(" ")]
        public uint Level;
        [TextArea(5, 20)]
        public string Code;

        public string[] helpNotes;
    }
}