using System;

namespace AIFounder.Domain.Repair
{
    public sealed class RepairMethodDefinition
    {
        public RepairMethodDefinition(string id, string displayName, int baseCost, int reliability, string tradeOff)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Method id is required.", nameof(id));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Method name is required.", nameof(displayName));

            Id = id;
            DisplayName = displayName;
            BaseCost = baseCost;
            Reliability = reliability;
            TradeOff = tradeOff ?? string.Empty;
        }

        public string Id { get; }
        public string DisplayName { get; }
        public int BaseCost { get; }
        public int Reliability { get; }
        public string TradeOff { get; }

        public int GetCost(RepairUpgradeState upgradeState, RepairUpgradeDefinition upgradeDefinition)
        {
            if (upgradeState == null || upgradeDefinition == null || !upgradeState.IsPurchased)
            {
                return BaseCost;
            }

            return Math.Max(0, BaseCost - upgradeDefinition.MethodCostReduction);
        }
    }
}
