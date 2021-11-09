using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KeyLoggerVersion1._0.Network
{
    class TCPSocket
    {
        private static TCPSocket _Instance;

        public static TCPSocket Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TCPSocket();
                }
                return _Instance;
            }
            private set
            {

            }
        }
    }
}
