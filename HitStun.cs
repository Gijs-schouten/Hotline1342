using System;

namespace PadZex
{
    public static class HitStun
    {
        /// <summary>
        /// How long is a single frame?
        /// </summary>
        private const float FRAME_DURATION = 0.16f;

        private static int hitStunAmount = 0;
        private static float currentHitStunTime = FRAME_DURATION;

        public static void Add(int time)
        {
            if (time < 1) throw new ArgumentOutOfRangeException(nameof(time));
            hitStunAmount += time;
        }

        /// <summary>
        /// Updates the hitstun timing.
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        /// <returns>returns false if there is no stun, returns true if there is a stun.</returns>
        public static bool UpdateStun(float deltaTime)
        {
            if (hitStunAmount == 0) return false;

            currentHitStunTime -= deltaTime;
            
            if (currentHitStunTime <= 0)
            {
                hitStunAmount--;
                currentHitStunTime = FRAME_DURATION;
                return false;
            }

            return true;
        }
    }
}