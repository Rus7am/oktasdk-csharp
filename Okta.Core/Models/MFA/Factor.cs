using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Okta.Core.Models
{
    /// <summary>
    /// An MFA factor
    /// </summary>
    public class Factor : OktaObject
    {
        public Factor()
        {
            Profile = new FactorProfile();
        }

        /// <summary>
        /// Builds a question <see cref="Factor"/>.
        /// </summary>
        /// <param name="questionType">Type of the question.</param>
        /// <param name="answer">The answer.</param>
        /// <returns>A question <see cref="Factor"/></returns>
        public static Factor BuildQuestion(string questionType, string answer)
        {
            var profile = new FactorProfile()
            {
                QuestionType = questionType,
                Answer = answer
            };

            return new Factor()
            {
                FactorType = Okta.Core.Models.FactorType.Question,
                Provider = FactorProviderType.Okta,
                Profile = profile
            };
        }

        /// <summary>
        /// Builds an SMS <see cref="Factor"/>.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="forceUpdate">if set to <c>true</c> [force update].</param>
        /// <returns>An SMS <see cref="Factor"/></returns>
        public static Factor BuildSms(string phoneNumber, bool forceUpdate = false)
        {
            var profile = new FactorProfile()
            {
                PhoneNumber = phoneNumber
            };

            profile.SetProperty("updatePhone", forceUpdate);

            return new Factor()
            {
                FactorType = Okta.Core.Models.FactorType.Sms,
                Provider = FactorProviderType.Okta,
                Profile = profile
            };
        }

        /// <summary>
        /// Gets or sets the type of the factor.
        /// </summary>
        /// <value>
        /// The type of the factor.
        /// </value>
        [JsonProperty("factorType")]
        public string FactorType { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [JsonProperty("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when factor was created.
        /// </summary>
        /// <value>
        /// The timestamp when factor was created
        /// </value>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when factor was last updated.
        /// </summary>
        /// <value>
        /// The timestamp when factor was last updated.
        /// </value>
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the profile of a supported factor.
        /// </summary>
        /// <value>
        /// The profile of a supported factor.
        /// </value>
        [JsonProperty("profile")]
        public FactorProfile Profile { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }

        // Important:
        //
        // While Factor Object [0] uses the "_links" key, it is a "Factor Links Object" [1] and not a "Links Object" [2] as used elsewhere.
        //
        // Footnotes:
        //   0: http://developer.okta.com/docs/api/rest/authn.html#factor-object
        //   1: http://developer.okta.com/docs/api/rest/authn.html#factor-links-object
        //   2: http://developer.okta.com/docs/api/rest/authn.html#links-object
        [JsonProperty("_links")]
        new public Dictionary<string, FactorLink> Links { get; set; }
    }
}