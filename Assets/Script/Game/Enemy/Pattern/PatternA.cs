namespace Script.Game.Enemy
{
    /// <summary>
    /// 간단한 2연격
    /// </summary>
    public class PatternA : Pattern
    {
        public PatternA() : base(2)
        {
        }

        public override float NextAction(Professor pf, Player.Player p)
        {
            
            return 0.0f;
        }
    }
}