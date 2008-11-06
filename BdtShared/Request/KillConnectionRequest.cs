// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
#endregion

namespace Bdt.Shared.Request
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une demande générique de suppression d'une connexion
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct KillConnectionRequest : IConnectionContextRequest
    {

        #region " Attributs "
        private int m_cid;
        private int m_sid;
        private int m_adminsid;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de connexion
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int Cid
        {
            get
            {
                return m_cid;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de session
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int Sid
        {
            get
            {
                return m_sid;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de session administrateur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int AdminSid
        {
            get
            {
                return m_adminsid;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sid">Le jeton de session</param>
        /// <param name="adminsid">Le jeton de session administrateur</param>
        /// <param name="cid">Le jeton de connexion</param>
        /// -----------------------------------------------------------------------------
        public KillConnectionRequest(int sid, int adminsid, int cid)
        {
            this.m_sid = sid;
            this.m_adminsid = adminsid;
            this.m_cid = cid;
        }
        #endregion

    }

}

