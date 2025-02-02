using System;

namespace SignalR
{
    public class DefaultConfigurationManager : IConfigurationManager
    {
        public DefaultConfigurationManager()
        {
            ConnectionTimeout = TimeSpan.FromSeconds(110);
            DisconnectTimeout = TimeSpan.FromSeconds(20);
            HeartBeatInterval = TimeSpan.FromSeconds(10);
            TransportConnectTimeout = TimeSpan.FromSeconds(20);
        }

        public TimeSpan ConnectionTimeout
        {
            get;
            set;
        }

        public TimeSpan DisconnectTimeout
        {
            get;
            set;
        }

        public TimeSpan? KeepAlive
        {
            get;
            set;
        }

        public TimeSpan HeartBeatInterval
        {
            get;
            set;
        }
        public TimeSpan TransportConnectTimeout { get; set; }
    }
}
