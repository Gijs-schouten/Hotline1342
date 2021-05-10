using System;

namespace PadZex
{
    public static class HitStun
    {
        /// <summary>
        /// How long is a single frame?
        /// </summary>
        private const float FRAME_DURATION = 0.16f / 8;

        private const float PAUSE_FRAME_TIME = 0.16f / 4;

        private static int hitStunAmount = 0;
        private static float currentHitStunTime = FRAME_DURATION;
        private static bool isPauseFrame = false;

        public static void Add(int time)
        {
            if (time < 1) return;
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

            if (isPauseFrame)
            {
                currentHitStunTime -= deltaTime;
                if (currentHitStunTime <= 0)
                {
                    currentHitStunTime = FRAME_DURATION;
                    isPauseFrame = false;
                }
                return true;
            }

            currentHitStunTime -= deltaTime;
            
            if (currentHitStunTime <= 0)
            {
                hitStunAmount--;
                isPauseFrame = true;
                currentHitStunTime = PAUSE_FRAME_TIME;
                return false;
            }

            return true;
        }
    }
}