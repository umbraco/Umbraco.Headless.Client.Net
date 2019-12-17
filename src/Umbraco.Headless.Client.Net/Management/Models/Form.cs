using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Management.Models
{
   public class Form
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }
        public string Indicator { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string NextLabel { get; set; }
        public string PreviousLabel { get; set; }
        public string SubmitLabel { get; set; }
        public bool DisableDefaultStylesheet { get; set; }
        public FormFieldIndication FieldIndicationType { get; set; }
        public bool HideFieldValidation { get; set; }
        public string MessageOnSubmit { get; set; }
        public bool ShowValidationSummary { get; set; }
        public Guid? GotoPageOnSubmit { get; set; }
        public IEnumerable<FormPage> Pages { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum FormFieldIndication
    {
        NoIndicator,
        MarkMandatoryFields,
        MarkOptionalFields
    }

    public class FormPage
    {
        public string Caption { get; set; }
        public IEnumerable<FormFieldset> Fieldsets { get; set; }
    }

    public class FormFieldset
    {
        public string Caption { get; set; }
        public FormCondition Condition { get; set; }
        public IEnumerable<FormFieldsetColumn> Columns { get; set; }
    }

    public class FormCondition
    {
        public FormConditionActionType ActionType { get; set; }
        public FormConditionLogicType LogicType { get; set; }
        public IEnumerable<FormConditionRule> Rules { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum FormConditionLogicType
    {
        All,
        Any
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum FormConditionActionType
    {
        Show,
        Hide
    }

    public class FormConditionRule
    {
        public string Field { get; set; }
        public FormConditionRuleOperator Operator { get; set; }
        public string Value { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum FormConditionRuleOperator
    {
        Is,
        IsNot,
        GreaterThen,
        LessThen,
        Contains,
        StartsWith,
        EndsWith
    }

    public class FormFieldsetColumn
    {
        public string Caption { get; set; }
        public int Width { get; set; }
        public IEnumerable<FormField> Fields { get; set; }
    }

    public class FormField
    {
        public string Caption { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }
        public string CssClass { get; set; }
        public string Alias { get; set; }
        public bool Required { get; set; }
        public string RequiredErrorMessage { get; set; }
        public FormCondition Condition { get; set; }
        public IDictionary<string, string> Settings { get; set; }
        public object PreValues { get; set; }
        public string Type { get; set; }
    }
}
