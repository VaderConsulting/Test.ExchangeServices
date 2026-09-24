# Test.ExchangeServices

Test.ExchangeServices is a C# WCF ASP.NET website that exposes GetCalendarEvents for Microsoft Exchange calendars. Callers pass an `ExchangeCredential` (username, password, domain) and `CalendarInfoSearchCriteria` (optional mailbox email, start/end window, max items); `CalendarService` uses a generated Exchange 2007 EWS proxy to FindItem/GetItem on the calendar folder and returns `CalendarInfo` (subject, location, times, body). `ExchangeHelper` builds the SOAP binding from the `appSettings` `ExchangeServer` URL and a `NetworkCredential`. The assembly is named `DMS.ExchangeServices` (company Testing, copyright 2008).

**Source last updated:** 2013-12-06 · **Language:** C# · **Target:** .NET Framework 3.5 · **Output:** WCF ASP.NET website (Library)

## Solution structure

| Project | Language | Type | Purpose |
|---------|----------|------|---------|
| `Test.ExchangeServices` | C# | WCF ASP.NET website (.NET 3.5) | `CalendarService.svc` GetCalendarEvents via Exchange Web Services |

## How to open

Open `Test.ExchangeServices.sln` in Visual Studio Express 2013 for Windows Desktop (solution format 12.00). The project is a Visual Studio 2008 C# web application (`ToolsVersion` 3.5, `ProductVersion` 9.0.30729). Point `Web.config` `appSettings` `ExchangeServer` at your EWS URL before hosting `CalendarService.svc`. The `.csproj` still references a sibling `DMS.Entity.Common.Exchange` project that is not in this tree; the calendar and credential types also live locally here (`CalendarInfo`, `CalendarInfoSearchCriteria`, `ExchangeCredential`).

## Requirements

- Visual Studio 2013, .NET Framework 3.5

## Attribution and provenance

Working copy from my Historical Dev folder `Test.ExchangeServices`. Assembly title/product `Test.ExchangeServices`, company Testing, copyright © Testing 2008. Root namespace and assembly name `DMS.ExchangeServices`. Includes a Visual Studio Web Reference to Microsoft Exchange Web Services (`Web References/ExchangeWebServices/`, WSDL target namespace `http://schemas.microsoft.com/exchange/services/2006/messages`). An internal EWS hostname in the `.csproj` web-reference URL was replaced with `https://exchange.example/EWS/Services.wsdl`. `Web.config` `ExchangeServer` is already a placeholder (`https://yourserverTesting.pvt/EWS/Exchange.asmx`); credentials are not stored in the tree (callers supply `ExchangeCredential` on each request).

## License

MIT. Copyright (c) 2026 VaderConsulting, for Dave Robinson's code. See `LICENSE`. The Exchange Web Services WSDL/XSD and generated `Reference.cs` are Microsoft Exchange schema artifacts.
