using BlazorAdminTemplate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class FilterItem : IEquatable<FilterItem>
    {
        public FilterType Type { get; set; }
        public string Guid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public bool Equals(FilterItem? other)
        {
            return other != null && Type == other.Type && Guid == other.Guid;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FilterItem);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Guid);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
