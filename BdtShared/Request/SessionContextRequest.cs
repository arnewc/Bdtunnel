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
    /// Une demande générique dans le contexte d'une session
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct SessionContextRequest : ISessionContextRequest 
    {

        #region " Attributs "
        private int m_sid;
        #endregion

        #region " Proprietes "
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
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="sid">Le jeton de session</param>
        /// -----------------------------------------------------------------------------
        public SessionContextRequest(int sid)
        {
            this.m_sid = sid;
        }
        #endregion

    }

}

