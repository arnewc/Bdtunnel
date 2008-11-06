// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
#endregion

namespace Bdt.Shared.Response
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une session utilisateur au sein du tunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct Session 
    {
        #region " Attributs "
        private string m_username;
        private bool m_admin;
        private DateTime m_logon;
        private DateTime m_lastAccess;
        private Connection[] m_connections;
        private string m_sid;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de session
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Sid
        {
            get
            {
                return m_sid;
            }
            set
            {
                m_sid = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les connexions
        /// </summary>
        /// -----------------------------------------------------------------------------
        public Connection[] Connections
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

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La date de dernier accès
        /// </summary>
        /// -----------------------------------------------------------------------------
        public DateTime LastAccess
        {
            get
            {
                return m_lastAccess;
            }
            set
            {
                m_lastAccess = value;
            }
        }
        #endregion
    }

}
