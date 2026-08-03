using System;
using System.Collections.Generic;
using System.Linq;

namespace AIFounder.Domain.Repair
{
    public sealed class RepairJobDefinition
    {
        public RepairJobDefinition(string id, string title, string requirement, int reward, string timeAllowance, IReadOnlyList<RepairMethodDefinition> methods)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Job id is required.", nameof(id));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Job title is required.", nameof(title));
            if (methods == null || methods.Count == 0) throw new ArgumentException("At least one repair method is required.", nameof(methods));

            Id = id;
            Title = title;
            Requirement = requirement ?? string.Empty;
            Reward = reward;
            TimeAllowance = timeAllowance ?? string.Empty;
            Methods = methods.ToArray();
        }

        public string Id { get; }
        public string Title { get; }
        public string Requirement { get; }
        public int Reward { get; }
        public string TimeAllowance { get; }
        public IReadOnlyList<RepairMethodDefinition> Methods { get; }

        public RepairMethodDefinition FindMethod(string methodId)
        {
            return Methods.FirstOrDefault(method => method.Id == methodId);
        }
    }
}
