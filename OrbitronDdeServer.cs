/*
 * microp11 2022, Paul Maxan
 * 
 * This file is part of the telescope below.
 * 
 * The telescope is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with the telescope.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using NDde.Server;
using System;
using System.Diagnostics;
using System.Timers;

namespace telescope
{
    public class OrbitronDdeServer : DdeServer
    {
        private readonly Timer timer = new();
        readonly TrackingDataItem trackingDataItem;

        public OrbitronDdeServer(TrackingDataItem trackingDataItem) : base("Orbitron")
        {
            this.trackingDataItem = trackingDataItem;
            // Create a timer that will be used to advise clients of new data.
            timer.Elapsed += OnTimerElapsed;
            timer.Interval = 1000;
            timer.SynchronizingObject = Context;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            // Advise all topic name and item name pairs.
            Advise("*", "*");
            //Debug.WriteLine("Advise(\"*\", \"*\")");
        }

        public override void Register()
        {
            base.Register();
            timer.Start();
        }

        public override void Unregister()
        {
            timer.Stop();
            base.Unregister();
        }

        protected override bool OnBeforeConnect(string topic)
        {
            Debug.WriteLine("OnBeforeConnect:".PadRight(16)
                + " Service='" + Service + "'"
                + " Topic='" + topic + "'");

            return true;
        }

        protected override void OnAfterConnect(DdeConversation conversation)
        {
            Debug.WriteLine("OnAfterConnect:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString());
        }

        protected override void OnDisconnect(DdeConversation conversation)
        {
            Debug.WriteLine("OnDisconnect:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString());
        }

        protected override bool OnStartAdvise(DdeConversation conversation, string item, int format)
        {
            Debug.WriteLine("OnStartAdvise:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString()
                + " Item='" + item + "'"
                + " Format=" + format.ToString());

            // Initiate the advisory loop only if the format is CF_TEXT.
            return format == 1;
        }

        protected override void OnStopAdvise(DdeConversation conversation, string item)
        {
            Debug.WriteLine("OnStopAdvise:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString()
                + " Item='" + item + "'");
        }

        protected override ExecuteResult OnExecute(DdeConversation conversation, string command)
        {
            Debug.WriteLine("OnExecute:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString()
                + " Command='" + command + "'");

            // Tell the client that the command was processed.
            return ExecuteResult.Processed;
        }

        protected override PokeResult OnPoke(DdeConversation conversation, string item, byte[] data, int format)
        {
            Debug.WriteLine("OnPoke:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString()
                + " Item='" + item + "'"
                + " Data=" + data.Length.ToString()
                + " Format=" + format.ToString());

            // Tell the client that the data was processed.
            return PokeResult.Processed;
        }

        protected override RequestResult OnRequest(DdeConversation conversation, string item, int format)
        {
            Debug.WriteLine($"{DateTime.Now} OnRequest:".PadRight(16)
                + " Service='" + conversation.Service + "'"
                + " Topic='" + conversation.Topic + "'"
                + " Handle=" + conversation.Handle.ToString()
                + " Item='" + item + "'"
                + " Format=" + format.ToString());

            // Return data to the client only if the format is CF_TEXT.
            if (format == 1)
            {
                TrackingDataItem tdi = new();
                lock (Locker.locker)
                {
                    tdi = trackingDataItem;
                }
                tdi.SN = tdi.SN.Replace(" ", "-");
                return new RequestResult(System.Text.Encoding.ASCII.GetBytes($"SN{tdi.SN} AZ{tdi.newAZ} EL{tdi.EL}\0"));
            }
            return RequestResult.NotProcessed;
        }

        protected override byte[] OnAdvise(string topic, string item, int format)
        {
            Debug.WriteLine("OnAdvise:".PadRight(16)
                + " Service='" + Service + "'"
                + " Topic='" + topic + "'"
                + " Item='" + item + "'"
                + " Format=" + format.ToString());

            // Send data to the client only if the format is CF_TEXT.
            if (format == 1)
            {
                return System.Text.Encoding.ASCII.GetBytes("Time=" + DateTime.Now.ToString() + "\0");
                //return System.Text.Encoding.ASCII.GetBytes($"SN\"MOL\" AZ250.9 EL49.1 DN145001280 UP144998720 RR-2.646 LO343.3813 LA35.5460 AL15170.080 TU2022 TL2022\0");
            }
            return null;
        }
    }
}
