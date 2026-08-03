namespace AIFounder.Domain.Repair
{
    public sealed class RepairUpgradeState
    {
        public bool IsPurchased { get; private set; }

        public bool TryPurchase(CashLedger ledger, RepairUpgradeDefinition definition)
        {
            if (IsPurchased || ledger == null || definition == null || !ledger.TrySpend(definition.Cost))
            {
                return false;
            }

            IsPurchased = true;
            return true;
        }
    }
}
