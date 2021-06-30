using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1RecordQuery.Data
{
    class GetServers
    {
        public List<ServersItem> servers { get; set; }
        public bool cache { get; set; }
    }

    public class ServersItem
    {
        public string prefix { get; set; }
        public string serverInfo { get; set; }
        public int inQue { get; set; }
        public string smallMode { get; set; }
        public string url { get; set; }
        public string mode { get; set; }
        public string currentMap { get; set; }
        public int playerAmount { get; set; }
        public int maxPlayerAmount { get; set; }
    }

    public class ListBoxServerInfo
    {
        public string ServerImage { get; set; }
        public string ServerName { get; set; }
        public string ServerMode { get; set; }
        public string ServerPlayerCount { get; set; }
        public string ServerPing { get; set; }

    }
}
