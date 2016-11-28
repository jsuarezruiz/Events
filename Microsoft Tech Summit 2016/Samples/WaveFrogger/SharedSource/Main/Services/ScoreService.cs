using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common;

namespace WaveFrogger.Services
{
    public class ScoreService : Service
    {
        public int MaxScore { get; private set; }

        public int CurrentScore { get; private set; }

        public void RegisterScore(int score)
        {
            this.CurrentScore = score;

            if (this.CurrentScore > this.MaxScore)
            {
                this.MaxScore = this.CurrentScore;
            }
        }
    }
}
