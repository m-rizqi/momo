using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.datasource.database.entity
{
    public class TimerEntity
    {
        private int id;
        private long countdown;

        public int Id { get { return id; } }
        public long Countdown { get { return countdown; } }

        public TimerEntity(int id, long countdown)
        {
            this.id = id;
            this.countdown = countdown;
        }
    }
}
