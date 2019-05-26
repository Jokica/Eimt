using Eimt.Application.DAL;
using Eimt.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.TagHelpers
{
    [HtmlTargetElement("sector-dropdown")]
    public class SectorDropDown:TagHelper
    {
        private readonly ISectorRepository repository;
        public ModelExpression AspFor { get; set; }
        public SectorDropDown(IUnitOfWork unitOfWork)
        {
            repository = unitOfWork.SectorRepository;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Append($@"

            ");
        }
    }
}
