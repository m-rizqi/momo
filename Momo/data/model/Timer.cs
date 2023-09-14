using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class Timer
    {
        private String id;
        private long countdown;

        public String Id { get { return id; } }
        public long Countdown { get { return countdown; } }

        public Timer(string id, long countdown)
        {
            this.id = id;
            this.countdown = countdown;
        }
    }
}
