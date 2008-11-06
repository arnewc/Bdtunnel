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
    /// Une demande d'écriture
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct WriteRequest : IConnectionContextRequest
    {

        #region " Attributs "
        private byte[] m_data;
        private int m_cid;
        private int m_sid;
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
        /// Les données à écrire
        /// </summary>
        /// -----------------------------------------------------------------------------
        public byte[] Data
        {
            get
            {
                return m_data;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sid">Le jeton de session</param>
        /// <param name="cid">Le jeton de connexion</param>
        /// <param name="data">Les données à écrire</param>
        /// -----------------------------------------------------------------------------
        public WriteRequest(int sid, int cid, byte[] data)
        {
            this.m_sid = sid;
            this.m_cid = cid;
            this.m_data = data;
        }
        #endregion

    }

}

