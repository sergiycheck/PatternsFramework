
using System.Net.PeerToPeer;

namespace PatternsP2PFramwork.p2p
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
    }
}