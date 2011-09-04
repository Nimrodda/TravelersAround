using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using TravelersAround.Model.Entities;

namespace TravelersAround.Repository
{
    internal class TravelersAroundEntities : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new TravelersAroundEntities object using the connection string found in the 'TravelersAroundEntities' section of the application configuration file.
        /// </summary>
        internal TravelersAroundEntities()
            : base("name=TravelersAroundEntities", "TravelersAroundEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initialize a new TravelersAroundEntities object.
        /// </summary>
        internal TravelersAroundEntities(string connectionString)
            : base(connectionString, "TravelersAroundEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// Initialize a new TravelersAroundEntities object.
        /// </summary>
        internal TravelersAroundEntities(EntityConnection connection)
            : base(connection, "TravelersAroundEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region ObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        internal IObjectSet<Message> Messages
        {
            get
            {
                if ((_Messages == null))
                {
                    _Messages = base.CreateObjectSet<Message>("Messages");
                }
                return _Messages;
            }
        }
        private IObjectSet<Message> _Messages;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        internal IObjectSet<Traveler> Travelers
        {
            get
            {
                if ((_Travelers == null))
                {
                    _Travelers = base.CreateObjectSet<Traveler>("Travelers");
                }
                return _Travelers;
            }
        }
        private IObjectSet<Traveler> _Travelers;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        internal IObjectSet<TravelerMessage> TravelerMessages
        {
            get
            {
                if ((_TravelerMessages == null))
                {
                    _TravelerMessages = base.CreateObjectSet<TravelerMessage>("TravelerMessages");
                }
                return _TravelerMessages;
            }
        }
        private IObjectSet<TravelerMessage> _TravelerMessages;

        #endregion
    } 

    
}
