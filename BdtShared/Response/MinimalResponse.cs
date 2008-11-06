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
    /// Une réponse générique dans le contexte d'une session
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable()]
    public struct MinimalResponse : IMinimalResponse 
    {

        #region " Attributs "
        private bool m_success;
        private string m_message;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La requête a aboutie/échoué ?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool Success
        {
            get
            {
                return m_success;
            }
            set
            {
                m_success = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le message d'information
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Message
        {
            get
            {
                return m_message;
            }
            set
            {
                m_message = value;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="success">réussi/échoué</param>
        /// <param name="message">Le message d'information</param>
        /// -----------------------------------------------------------------------------
        public MinimalResponse(bool success, string message)
        {
            this.m_success = success;
            this.m_message = message;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="success">Login réussi/échoué</param>
        /// -----------------------------------------------------------------------------
        public MinimalResponse(bool success)
        {
            this.m_success = success;
            this.m_message = string.Empty;
        }
        #endregion

    }

}


