using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mustache2
{
    class gameTimer
    {
        DispatcherTimer timer;
        DispatcherPriority priority;
        public TimeSpan interval;
        public int duration;
        EventHandler timer_event;
        /// <summary>
        /// Construct a timer
        /// </summary>
        /// <param name="eve">The event you want to be called on each tick</param>
        /// <param name="inter">The interval that you want the event to be called</param>
        /// <param name="dur">The amount of times you want the event to be called</param>
        public gameTimer(EventHandler eve, TimeSpan inter, int dur)
        {
            interval = inter;
            duration = dur;
            timer_event = eve;
            timer = new DispatcherTimer();
        }
        /// <summary>
        /// Construct a timer with a certian Priority
        /// </summary>
        /// <param name="eve">The event you want to be called on each tick</param>
        /// <param name="inter">The interval that you want the event to be called</param>
        /// <param name="dur">The number of times you want the event to be called</param>
        /// <param name="proi">The proiority of the timer event</param>
        public gameTimer(EventHandler eve, TimeSpan inter, int dur, DispatcherPriority proi)
        {
            interval = inter;
            duration = dur;
            timer_event = eve;
            priority = proi;
            timer = new DispatcherTimer(priority);
        }
        /// <summary>
        /// Starts the timer
        /// </summary>
        /// <returns>Returns the DispatcherTimer object</returns>
        public DispatcherTimer start()
        {
            timer.Tag = duration;
            timer.Interval = interval;
            timer.Tick += timer_event;
            timer.Start();
            return timer;
        }

    }
}
