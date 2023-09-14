using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momo.data.model
{
    class Timer
    {
        String id;
        long countdown;

        Timer(string id, long countdown)
        {
            this.id = id;
            this.countdown = countdown;
        }
    }
}
