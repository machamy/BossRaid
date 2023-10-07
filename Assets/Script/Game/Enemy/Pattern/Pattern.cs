namespace Script.Game.Enemy
{
    public class Pattern
    {
        public int Frequency { get; private set; }
        private int actionIdx;
        private int MAX_ACTION_IDX;

        public Pattern(int maxActionIdx)
        {
            MAX_ACTION_IDX = maxActionIdx;
        }
        /// <summary>
        /// 다음 행동을 실행하고 대기 시간을 반환한다.
        /// </summary>
        /// <returns>대기시간(딜레이)</returns>
        public virtual float NextAction(Professor pf, Player.Player p)
        {
            return 0.0f;
        }
        
    }
}