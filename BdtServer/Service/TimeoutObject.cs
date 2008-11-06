// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections;
using System.Collections.Generic;

using Bdt.Shared.Logs;
#endregion

namespace Bdt.Server.Service
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un objet soumis au timeout
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class TimeoutObject
    {
        #region " Attributs "
        protected DateTime m_lastAccess;
        protected int m_timeoutdelay = 0; // heures -> CheckTimeout <=0 pour disabled
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Delai de Timeout
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int TimeoutDelay
        {
            get
            {
                return m_timeoutdelay;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La date de dernière opération I/O
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

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="timeoutdelay">valeur de timeout</param>
        /// -----------------------------------------------------------------------------
        protected TimeoutObject(int timeoutdelay)
        {
            m_timeoutdelay = timeoutdelay;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Timeout de l'objet
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected abstract void Timeout(ILogger logger);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout de l'objet
        /// </summary>
        /// <returns>true en cas de timeout</returns>
        /// -----------------------------------------------------------------------------
        protected virtual bool CheckTimeout(ILogger logger)
        {
            if (TimeoutDelay > 0)
            {
                return DateTime.Now.Subtract(LastAccess).TotalHours > TimeoutDelay;
            }
            return false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout dans une collection
        /// </summary>
        /// <typeparam name="T">un type TimeoutObject</typeparam>
        /// <param name="logger">un logger</param>
        /// <param name="collection">les elements à vérifier</param>
        /// -----------------------------------------------------------------------------
        public static void CheckTimeout<T>(ILogger logger, Dictionary<int, T> collection) where T : TimeoutObject
        {
            foreach (int key in new ArrayList(collection.Keys))
            {
                T item = collection[key];
                if (item.CheckTimeout(logger))
                {
                    item.Timeout(logger);
                    collection.Remove(key);
                }
            }
        
        }
        #endregion

    }

}
