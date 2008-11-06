// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

using Bdt.Server.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.Server.Service
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une session utilisateur au sein du tunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class TunnelSession : TimeoutObject 
    {
        #region " Constantes "
        // Le test de la connexion effective
        public const int SOCKET_TEST_POLLING_TIME = 100; // msec
        #endregion

        #region " Attributs "
        protected string m_username;
        protected bool m_admin; 
        protected DateTime m_logon;
        protected int m_connectiontimeoutdelay;
        protected Dictionary<int, TunnelConnection> m_connections = new Dictionary<int, TunnelConnection>();
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les connexions
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected virtual Dictionary<int, TunnelConnection> Connections
        {
            get
            {
                return m_connections;
            }
            set
            {
                m_connections = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le nom associé
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Username
        {
            get
            {
                return m_username;
            }
            set
            {
                m_username = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Utilisateur en mode admin
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool Admin
        {
            get
            {
                return m_admin;
            }
            set
            {
                m_admin = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La date de login
        /// </summary>
        /// -----------------------------------------------------------------------------
        public DateTime Logon
        {
            get
            {
                return m_logon;
            }
            set
            {
                m_logon = value;
            }
        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="timeoutdelay">valeur du timeout de session</param>
        /// <param name="connectiontimeoutdelay">valeur du timeout de connexion</param>
        /// -----------------------------------------------------------------------------
        public TunnelSession(int timeoutdelay, int connectiontimeoutdelay)
            : base(timeoutdelay)
        {
            m_connectiontimeoutdelay = connectiontimeoutdelay;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Timeout de l'objet
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void Timeout(ILogger logger)
        {
            logger.Log(this, String.Format(Strings.SESSION_TIMEOUT, Username), ESeverity.INFO);
            DisconnectAndRemoveAllConnections();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout de l'objet
        /// </summary>
        /// <returns>true en cas de timeout</returns>
        /// -----------------------------------------------------------------------------
        protected override bool CheckTimeout(ILogger logger)
        {
            CheckTimeout(logger, Connections);
            return base.CheckTimeout(logger);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de la connexion associée à une requête
        /// </summary>
        /// <param name="request">la requête</param>
        /// <param name="response">la réponse à préparer</param>
        /// <returns>La connexion si la connexion est valide</returns>
        /// -----------------------------------------------------------------------------
        public TunnelConnection CheckConnection<I, O>(ref I request, ref O response)
            where I : IConnectionContextRequest
            where O : IConnectionContextResponse
        {
            TunnelConnection connection;
            if (!Connections.TryGetValue(request.Cid, out connection))
            {
                response.Success = false;
                response.Message = Strings.SERVER_SIDE + Strings.CID_NOT_FOUND;
                return null;
            }
            else
            {
                connection.LastAccess = DateTime.Now;
                try
                {
                    response.Connected = (!(connection.TcpClient.Client.Poll(SOCKET_TEST_POLLING_TIME, System.Net.Sockets.SelectMode.SelectRead) && connection.TcpClient.Client.Available == 0));
                    response.DataAvailable = connection.TcpClient.Client.Available > 0;
                }
                catch (Exception)
                {
                    response.Connected = false;
                    response.DataAvailable = false;
                }
                response.Success = true;
                response.Message = string.Empty;
                return connection;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generation d'un identifiant de connexion unique
        /// </summary>
        /// <returns>un entier unique</returns>
        /// -----------------------------------------------------------------------------
        protected int GetNewCid()
        {
            return Tunnel.GetNewId(Connections);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ajoute une nouvelle connexion à la table des connexions
        /// </summary>
        /// <param name="connection">la connexion à ajouter</param>
        /// <returns>le jeton de connexion</returns>
        /// -----------------------------------------------------------------------------
        public int AddConnection(TunnelConnection connection)
        {
            int cid = GetNewCid();
            Connections.Add(cid, connection);
            return cid;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Création d'une nouvelle connexion
        /// </summary>
        /// <returns>la connexion</returns>
        /// -----------------------------------------------------------------------------
        public TunnelConnection CreateConnection()
        {
            return new TunnelConnection(m_connectiontimeoutdelay);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Supprime une connexion de table des connexions
        /// </summary>
        /// <param name="cid">le jeton de connexion à supprimer</param>
        /// -----------------------------------------------------------------------------
        public void RemoveConnection(int cid)
        {
            Connections.Remove(cid);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deconnexion de toutes les connexions sous cette session
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void DisconnectAndRemoveAllConnections()
        {
            foreach (int cid in new ArrayList(Connections.Keys))
            {
                TunnelConnection connection = Connections[cid];
                connection.SafeDisconnect();
                RemoveConnection(cid);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne toutes les connexions sous forme "structure" pour l'export par ex
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public Connection[] GetConnectionsStruct()
        {
            List<Connection> result = new List<Connection>();

            foreach (int cid in Connections.Keys)
            {
                TunnelConnection connection = Connections[cid];
                Connection export = new Connection();
                export.Cid = cid.ToString("x");
                IPEndPoint endpoint = connection.TcpClient.Client.RemoteEndPoint as IPEndPoint;
                export.Address = endpoint.Address.ToString();
                export.Port = endpoint.Port;
                export.ReadCount = connection.ReadCount;
                export.WriteCount = connection.WriteCount;
                export.LastAccess = connection.LastAccess;
                result.Add(export);
            }

            return result.ToArray();
        }
        #endregion
    }

}
