namespace AIFounder.Domain.Repair
{
    public sealed class RepairUpgradeDefinition
    {
        public RepairUpgradeDefinition(string title, int cost, int methodCostReduction, string effectDescription)
        {
            Title = title;
            Cost = cost;
            MethodCostReduction = methodCostReduction;
            EffectDescription = effectDescription ?? string.Empty;
        }

        public string Title { get; }
        public int Cost { get; }
        public int MethodCostReduction { get; }
        public string EffectDescription { get; }
    }
}
