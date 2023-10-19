using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    internal class TimerEntity
    {
        private String id;
        private long countdown;

        public String Id { get { return id; } }
        public long Countdown { get { return countdown; } }

        public TimerEntity(string id, long countdown)
        {
            this.id = id;
            this.countdown = countdown;
        }
    }
}
