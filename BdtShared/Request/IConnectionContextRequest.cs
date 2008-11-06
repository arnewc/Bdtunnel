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
    /// Une demande générique dans le cadre d'une connexion
    /// </summary>
    /// -----------------------------------------------------------------------------
    public interface IConnectionContextRequest : ISessionContextRequest 
    {

        #region " Attributs "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le jeton de connexion
        /// </summary>
        /// -----------------------------------------------------------------------------
        int Cid
        {
            get;
        }
        #endregion

    }

}


