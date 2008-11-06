// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Bdt.Client.Sockets;
using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Socks
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Serveur Socks avec gestion v4, v4A, v5
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SocksServer : Client.Sockets.TcpServer
    {

        #region " Attributs "
        protected ITunnel m_tunnel;
        protected int m_sid;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="port">port local côté client</param>
        /// <param name="shared">bind sur toutes les ip/ip locale</param>
        /// <param name="tunnel">tunnel de communication</param>
        /// <param name="sid">session-id</param>
        /// -----------------------------------------------------------------------------
        public SocksServer(int port, bool shared, ITunnel tunnel, int sid)
            : base(port, shared)
        {
            Log(string.Format(Strings.SOCKS_SERVER_STARTED, Ip, port), ESeverity.INFO);

            m_tunnel = tunnel;
            m_sid = sid;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Callback en cas de nouvelle connexion
        /// </summary>
        /// <param name="client">le socket client</param>
        /// -----------------------------------------------------------------------------
        protected override void OnNewConnection(TcpClient client)
        {
            GenericSocksHandler handler = null;
            try
            {
                handler = GenericSocksHandler.GetInstance(client);
            }
            catch (Exception ex)
            {
                Log(ex.Message, ESeverity.ERROR);
                Log(ex.ToString(), ESeverity.DEBUG);
            }
            if (handler != null)
            {
                new Gateway(client, m_tunnel, m_sid, handler.Address, handler.RemotePort);
            }
        }

        #endregion

    }

}

