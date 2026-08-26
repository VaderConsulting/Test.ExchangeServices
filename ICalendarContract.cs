using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Test.ExchangeService.Entity;

namespace Test.ExchangeServices
{
    /// <summary>
    /// The CalendarService class provides operations for working with exchange calendars.
    /// </summary>
    [ServiceContract(Name = "ICalendarContract", Namespace = "http://Testing.com/ExchangeServices")]
    public interface ICalendarContract
    {

        /// <summary>
        /// The GetCalendarEvents method gets a list of calendar events for a user based on a search criteria.
        /// </summary>
        /// <param name="credentials">Specifies the credentials of the user to get calendar events for.</param>
        /// <param name="criteria">Specifies the search criteria.</param>
        /// <returns>A List of CalendarInfo.</returns>
        [OperationContract(Name = "GetCalendarEvents")]
        List<CalendarInfo> GetCalendarEvents(ExchangeCredential credentials, CalendarInfoSearchCriteria criteria);

    }


}
