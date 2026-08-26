using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Test.ExchangeService.Entity
{
    /// <summary>
    /// The CalendarInfoSearchCriteria class represents search critieria for getting Exchange Calendar events.
    /// </summary>
    [DataContract(Name = "CalendarInfoSearchCriteria", Namespace = "http://test.com/esc/esdexchangeservice/entity")]
    public class CalendarInfoSearchCriteria
    {
        /// <summary>
        /// Gets or sets the email address to get items for.
        /// </summary>
        [DataMember(Name = "EmailAddress", IsRequired = false, Order = 1)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the end date and time.
        /// </summary>
        [DataMember(Name = "EndDateAndTime", IsRequired = true, Order = 2)]
        public DateTime EndDateAndTime { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items to return. Default is 0 which returns all items.
        /// </summary>
        [DataMember(Name = "MaxItemsToReturn", IsRequired = false, Order = 3)]
        public int MaxItemsToReturn { get; set; }

        /// <summary>
        /// Gets or sets the start date and time.
        /// </summary>
        [DataMember(Name = "StartDateAndTime", IsRequired = true, Order = 4)]
        public DateTime StartDateAndTime { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether or not to populate the calendar body.
        /// </summary>
        [DataMember(Name = "PopulateCalendarBody", IsRequired = true, Order = 5)]
        public bool PopulateCalendarBody { get; set; }

    }
}
