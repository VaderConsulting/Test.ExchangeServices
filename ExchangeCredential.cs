using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Test.ExchangeService.Entity
{    
    /// <summary>
    /// The ExchangeCredential class represents a credential used by an exchange binding.
    /// </summary>
    [DataContract(Name = "ExchangeCredential", Namespace = "http://test.com/ent/esdexchangeservice/entity")]
    public class ExchangeCredential
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [DataMember(Name = "UserName", IsRequired = true, Order = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the pasword.
        /// </summary>
        [DataMember(Name = "Password", IsRequired = true, Order = 2)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the exchange server domain.
        /// </summary>
        [DataMember(Name = "Domain", IsRequired = true, Order = 3)]
        public string Domain { get; set; }

    }
}
