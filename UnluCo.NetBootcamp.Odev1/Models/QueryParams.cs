using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Models
{
    public class QueryParams
    {
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
        public string Sort { get; set; }// = "BrandName";
        public SortingDirection SortingDirection { get; set; } = SortingDirection.ASC;
        public string SearchingWord { get; set; }
        public string SearchingFilter { get; set; }
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 9999;
        public string ModeStatus { get; set; }// = "LightMode";
    }
    
    public enum SortingDirection
    {
        ASC = 1,
        DESC
    }
}
