using UnityEngine;

namespace DataLayer.StaticData.Bug
{
    [CreateAssetMenu(menuName = "Battle/SyntaxBag", fileName ="SyntaxBag")]
    public class SyntaxBag : BugTask
    {
        [TextArea(5, 20)]
        public string Ansver;
        private void OnValidate()
        {
            Type = BugType.Syntax;
        }
    }
}