using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
 
namespace Personal.Portfolio.Helper
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class TagHelpers : TagHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
 
        public TagHelpers(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
 
        private IDictionary<string, string> routeValues;
 
        /// <summary>The name of the action method.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }
 
        /// <summary>The name of the controller.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }
 
        [HtmlAttributeName("asp-page")]
        public string Page { get; set; }
         
        /// <summary>Additional parameters for the route.</summary>
        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (routeValues == null)
                    routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return routeValues;
            }
            set
            {
                this.routeValues = value;
            }
        }
 
        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
 
            if (ShouldBeActive())
            {
                MakeActive(output);
            }
 
            output.Attributes.RemoveAll("is-active-route");
        }
 
        private bool ShouldBeActive()
        {
            string currentController = string.Empty;
            string currentAction = string.Empty;
 
            if (ViewContext.RouteData.Values["Controller"] != null)
            {
                currentController = ViewContext.RouteData.Values["Controller"].ToString();
            }
 
            if (ViewContext.RouteData.Values["Action"] != null)
            {
                currentAction = ViewContext.RouteData.Values["Action"].ToString();
            }
 
            if(Controller != null)
            {
                if (!string.IsNullOrWhiteSpace(Controller) && Controller.ToLower() != currentController.ToLower())
                {
                    return false;
                }
 
                if (!string.IsNullOrWhiteSpace(Action) && Action.ToLower() != currentAction.ToLower())
                {
                    return false;
                }
            }
 
            if (Page != null)
            {
                if (!string.IsNullOrWhiteSpace(Page) && Page.ToLower() != contextAccessor.HttpContext.Request.Path.Value.ToLower())
                {
                    return false;
                }
            }
 
            foreach (KeyValuePair<string, string> routeValue in RouteValues)
            {
                if (!ViewContext.RouteData.Values.ContainsKey(routeValue.Key) ||
                    ViewContext.RouteData.Values[routeValue.Key].ToString() != routeValue.Value)
                {
                    return false;
                }
            }
 
            return true;
        }
 
        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active", StringComparison.Ordinal) < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }
    }
}