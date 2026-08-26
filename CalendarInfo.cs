using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Test.ExchangeService.Entity
{
    /// <summary>
    /// The CalendarInfo class represents a Microsoft Exchange calendar event.
    /// </summary>
    [DataContract(Name = "CalendarInfo", Namespace = "http://test.com/esc/esdexchangeservice/entity")]
    public class CalendarInfo
    {
        /// <summary>
        /// Gets or sets the start time of the event.
        /// </summary>
        [DataMember(Name = "StartTime", IsRequired = true, Order = 1)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the event.
        /// </summary>
        [DataMember(Name = "EndTime", IsRequired = true, Order = 2)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the title of the event.
        /// </summary>
        [DataMember(Name = "Subject", IsRequired = true, Order = 3)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the text or body of the event.
        /// </summary>
        [DataMember(Name = "Body", IsRequired = true, Order = 4)]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the location of the event.
        /// </summary>
        [DataMember(Name = "Location", IsRequired = true, Order = 5)]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the body type.
        /// </summary>
        [DataMember(Name = "BodyType", IsRequired = true, Order = 6)]
        public ExchangeBodyType BodyType { get; set; }

        ///// <summary>
        ///// Gets or sets the change key used for identifying the item. Used for looking up an Exchange item.
        ///// </summary>
        //[DataMember(Name = "ChangeKey", IsRequired = true, Order = 7)]
        //public string ChangeKey { get; set; }

        /// <summary>
        /// Gets or sets the id used for identifying the item. Used for looking up an Exchange item.
        /// </summary>
        [DataMember(Name = "Id", IsRequired = true, Order = 8)]
        public string Id { get; set; }

    }

    /// <summary>
    /// The ExchangeBodyType enumeration represents the body type of an exchange item.
    /// </summary>
    public enum ExchangeBodyType
    {
        /// <summary>
        /// The item was retrieved but the criteria specified not to retrieve the body.
        /// </summary>
        NotRetrieved,

        /// <summary>
        /// The body contains html.
        /// </summary>
        Html,

        /// <summary>
        /// The body is plain text.
        /// </summary>
        Text
    }
}
