using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    /// <summary>
    /// Enum used to represent the Umbraco Object Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum UmbracoObjectTypes
    {
        /// <summary>
        /// Default value
        /// </summary>
        Unknown,


        /// <summary>
        /// Root
        /// </summary>
        ROOT,

        /// <summary>
        /// Document
        /// </summary>
        Document,

        /// <summary>
        /// Media
        /// </summary>
        Media,

        /// <summary>
        /// Member Type
        /// </summary>
        MemberType,

        /// <summary>
        /// Template
        /// </summary>
        Template,

        /// <summary>
        /// Member Group
        /// </summary>
        MemberGroup,

        /// <summary>
        /// "Media Type
        /// </summary>
        MediaType,

        /// <summary>
        /// Document Type
        /// </summary>
        DocumentType,

        /// <summary>
        /// Recycle Bin
        /// </summary>
        RecycleBin,

        /// <summary>
        /// Stylesheet
        /// </summary>
        Stylesheet,

        /// <summary>
        /// Member
        /// </summary>
        Member,

        /// <summary>
        /// Data Type
        /// </summary>
        DataType,

        /// <summary>
        /// Document type container
        /// </summary>
        DocumentTypeContainer,

        /// <summary>
        /// Media type container
        /// </summary>
        MediaTypeContainer,

        /// <summary>
        /// Media type container
        /// </summary>
        DataTypeContainer,

        /// <summary>
        /// Relation type
        /// </summary>
        RelationType,

        /// <summary>
        /// Forms Form
        /// </summary>
        FormsForm,

        /// <summary>
        /// Forms PreValue
        /// </summary>
        FormsPreValue,

        /// <summary>
        /// Forms DataSource
        /// </summary>
        FormsDataSource,

        /// <summary>
        /// Language
        /// </summary>
        Language,

        /// <summary>
        /// Document Blueprint
        /// </summary>
        DocumentBlueprint,

        /// <summary>
        /// Reserved Identifier
        /// </summary>
        IdReservation
    }
}
