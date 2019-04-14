using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MovieNight {
    [HtmlTargetElement(Attributes = ClassPrefix + "*")]
    public class ConditionClassTagHelper : TagHelper {
        private const string ClassPrefix = "asp-if-class-";

        [HtmlAttributeName("class")] public string CssClass { get; set; }

        private IDictionary<string, bool> _classValues;

        [HtmlAttributeName("", DictionaryAttributePrefix = ClassPrefix)]
        public IDictionary<string, bool> ClassValues {
            get => _classValues ?? (_classValues = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
            set => _classValues = value;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            var items = _classValues.Where(e => e.Value).Select(e => e.Key).ToList();

            if (!string.IsNullOrEmpty(CssClass)) {
                items.Insert(0, CssClass);
            }

            if (items.Any()) {
                var classes = string.Join(" ", items.ToArray());
                output.Attributes.Add("class", classes);
            }
        }
    }

    [HtmlTargetElement(Attributes = Attribute)]
    public class ConditionDisableTagHelper : TagHelper {
        private const string Attribute = "asp-disable";
        
        [HtmlAttributeName(Attribute)] public bool IsDisabled { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (IsDisabled) {
                var d = new TagHelperAttribute("disabled");
                output.Attributes.Add(d);
            }

            base.Process(context, output);
        }
    }
}