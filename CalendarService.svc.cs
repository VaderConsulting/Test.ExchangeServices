using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Test.ExchangeService.Entity;

using Test.ExchangeServices.ExchangeWebServices;

namespace Test.ExchangeServices
{
    /// <summary>
    /// The CalendarService class provides operations for working with exchange calendars.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Name = "CalendarService", Namespace = "http://Testing.com/ExchangeServices")]
    public class CalendarService : ICalendarContract
    {

        #region ICalendarContract Members

        /// <summary>
        /// The GetCalendarEvents method gets a list of calendar info for a user based on a search criteria.
        /// </summary>
        /// <param name="credentials">Specifies the credentials of the user to get calendar info for.</param>
        /// <param name="criteria">Specifies the search criteria.</param>
        /// <returns>A List of CalendarInfo.</returns>
        [OperationBehavior(TransactionScopeRequired = true)]
        public List<CalendarInfo> GetCalendarEvents(ExchangeCredential credentials, CalendarInfoSearchCriteria criteria)
        {
            CheckCredentials(credentials);
            return GetCalendarEventsAPI(credentials, criteria);
        }

        #endregion


        #region Private Methods

        private void CheckCredentials(ExchangeCredential credentials)
        {
            if (string.IsNullOrEmpty(credentials.UserName))
                throw new ArgumentNullException("The user name cannot be blank.");

            if (string.IsNullOrEmpty(credentials.Password))
                throw new ArgumentNullException("The password cannot be blank.");

            if (string.IsNullOrEmpty(credentials.Domain))
                throw new ArgumentNullException("The domain cannot be blank.");


        }

        private List<CalendarInfo> GetCalendarEventsAPI(ExchangeCredential credentials, CalendarInfoSearchCriteria criteria)
        {
            // Resources 
            // http://msdn2.microsoft.com/en-us/library/bb508824(EXCHG.80).aspx
            // How to get body, attachments and other large parts that don't come across on the FindItem call.

            List<CalendarInfo> calendarEvents = new List<CalendarInfo>();

            ExchangeServiceBinding esb = new ExchangeHelper().GetExchangeBinding(credentials);

            // Form the FindItem request.
            FindItemType findItemRequest = new FindItemType();

            CalendarViewType calendarView = new CalendarViewType();
            calendarView.StartDate = criteria.StartDateAndTime;
            calendarView.EndDate = criteria.EndDateAndTime;

            if (criteria.MaxItemsToReturn > 0)
            {
                calendarView.MaxEntriesReturned = criteria.MaxItemsToReturn;
                calendarView.MaxEntriesReturnedSpecified = true;
            }

            findItemRequest.Item = calendarView;

            // Define which item properties are returned in the response.
            ItemResponseShapeType itemProperties = new ItemResponseShapeType();
            // Use the Default shape for the response. 
            //itemProperties.BaseShape = DefaultShapeNamesType.IdOnly;
            itemProperties.BaseShape = DefaultShapeNamesType.AllProperties;
            findItemRequest.ItemShape = itemProperties;

            DistinguishedFolderIdType[] folderIDArray = new DistinguishedFolderIdType[1];
            folderIDArray[0] = new DistinguishedFolderIdType();
            folderIDArray[0].Id = DistinguishedFolderIdNameType.calendar;

            if (!string.IsNullOrEmpty(criteria.EmailAddress))
            {
                folderIDArray[0].Mailbox = new EmailAddressType();
                folderIDArray[0].Mailbox.EmailAddress = criteria.EmailAddress.Trim();
            }
            
            findItemRequest.ParentFolderIds = folderIDArray;

            // Define the traversal type.
            findItemRequest.Traversal = ItemQueryTraversalType.Shallow;

            try
            {
                // Send the FindItem request and get the response.
                FindItemResponseType findItemResponse = esb.FindItem(findItemRequest);

                // Access the response message.
                ArrayOfResponseMessagesType responseMessages = findItemResponse.ResponseMessages;
                ResponseMessageType[] rmta = responseMessages.Items;

                int folderNumber = 0;

                foreach (ResponseMessageType rmt in rmta)
                {
                    // One FindItemResponseMessageType per folder searched.
                    FindItemResponseMessageType firmt = rmt as FindItemResponseMessageType;

                    if (firmt.RootFolder == null)
                        continue ;

                    FindItemParentType fipt = firmt.RootFolder;
                    object obj = fipt.Item;

                    // FindItem contains an array of items.
                    if (obj is ArrayOfRealItemsType)
                    {
                        ArrayOfRealItemsType items = (obj as ArrayOfRealItemsType);
                        if (items.Items == null)
                        {
                            // Console.WriteLine("Folder {0}: No items in folder", folderNumber);
                            folderNumber++;
                        }
                        else
                        {
                            foreach (ItemType it in items.Items)
                            {

                                if (it is CalendarItemType)
                                {
                                    CalendarItemType cal = (CalendarItemType)it;
                                    CalendarInfo ce = new CalendarInfo();

                                    ce.Location = cal.Location;
                                    ce.StartTime = cal.Start;
                                    ce.EndTime = cal.End;
                                    ce.Subject = cal.Subject;
                                    ce.Body = GetMeetingBody(esb, cal);

                                    calendarEvents.Add(ce);
                                }


                                //Console.WriteLine("Folder {0}: Item identifier: {1}", folderNumber, it.ItemId.Id);

                            }

                            folderNumber++;
                        }
                    }
                }

                //Console.ReadLine();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                
            }


            return calendarEvents;
        }

        private string GetMeetingBody(ExchangeServiceBinding binding, CalendarItemType meeting)
        {
            string meetingBody = string.Empty;
            CalendarItemType temp = null;

            // Call GetItem on each ItemId to retrieve the 
            // item’s Body property and any AttachmentIds.
            //
            // Form the GetItem request.
            GetItemType getItemRequest = new GetItemType();

            getItemRequest.ItemShape = new ItemResponseShapeType();
            // AllProperties on a GetItem request WILL return 
            // the message body.
            getItemRequest.ItemShape.BaseShape = DefaultShapeNamesType.AllProperties;

            getItemRequest.ItemIds = new ItemIdType[1];
            getItemRequest.ItemIds[0] = (BaseItemIdType)meeting.ItemId;

            // Here is the call to exchange.
            GetItemResponseType getItemResponse = binding.GetItem(getItemRequest);

            // We only passed in one ItemId to the GetItem
            // request. Therefore, we can assume that
            // we got at most one Item back.
            ItemInfoResponseMessageType getItemResponseMessage = getItemResponse.ResponseMessages.Items[0] as ItemInfoResponseMessageType;

            if (getItemResponseMessage != null)
            {
                if (getItemResponseMessage.ResponseClass == ResponseClassType.Success 
                    && getItemResponseMessage.Items.Items != null 
                    && getItemResponseMessage.Items.Items.Length > 0)
                {
                    temp = (CalendarItemType)getItemResponseMessage.Items.Items[0];

                    if (temp.Body != null)
                        meetingBody = temp.Body.Value;
                }
            }

            return meetingBody;
        }


        #endregion
    }
}
