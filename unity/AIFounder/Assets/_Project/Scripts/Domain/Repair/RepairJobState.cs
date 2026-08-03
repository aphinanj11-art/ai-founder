namespace AIFounder.Domain.Repair
{
    public sealed class RepairJobState
    {
        private int cashBeforeRepair;
        private int appliedRepairCost;

        public RepairJobState(RepairJobDefinition definition)
        {
            Definition = definition;
            Status = RepairJobStatus.Available;
        }

        public RepairJobDefinition Definition { get; }
        public RepairJobStatus Status { get; private set; }
        public RepairMethodDefinition SelectedMethod { get; private set; }
        public RepairOutcome Outcome { get; private set; }

        public bool Accept()
        {
            if (Status != RepairJobStatus.Available)
            {
                return false;
            }

            Status = RepairJobStatus.Accepted;
            return true;
        }

        public bool Reject()
        {
            if (Status != RepairJobStatus.Available)
            {
                return false;
            }

            Status = RepairJobStatus.Rejected;
            return true;
        }

        public bool SelectMethod(string methodId)
        {
            if (Status != RepairJobStatus.Accepted)
            {
                return false;
            }

            RepairMethodDefinition method = Definition.FindMethod(methodId);
            if (method == null)
            {
                return false;
            }

            SelectedMethod = method;
            Status = RepairJobStatus.MethodSelected;
            return true;
        }

        public bool ResolveRepair(CashLedger ledger, RepairUpgradeState upgradeState, RepairUpgradeDefinition upgradeDefinition)
        {
            if (Status != RepairJobStatus.MethodSelected || ledger == null || SelectedMethod == null)
            {
                return false;
            }

            int methodCost = SelectedMethod.GetCost(upgradeState, upgradeDefinition);
            cashBeforeRepair = ledger.Cash;
            if (!ledger.TrySpend(methodCost))
            {
                return false;
            }

            appliedRepairCost = methodCost;
            Status = RepairJobStatus.Repaired;
            return true;
        }

        public bool Deliver(CashLedger ledger)
        {
            if (Status != RepairJobStatus.Repaired || ledger == null)
            {
                return false;
            }

            ledger.AddRevenue(Definition.Reward);
            Outcome = new RepairOutcome(
                Definition.Title,
                SelectedMethod.DisplayName,
                Definition.Reward,
                appliedRepairCost,
                cashBeforeRepair,
                ledger.Cash,
                BuildExplanation());
            Status = RepairJobStatus.Delivered;
            return true;
        }

        public bool ReviewOutcome()
        {
            if (Status != RepairJobStatus.Delivered)
            {
                return false;
            }

            Status = RepairJobStatus.OutcomeReviewed;
            return true;
        }

        private string BuildExplanation()
        {
            return $"{SelectedMethod.DisplayName} used {appliedRepairCost} cash in repair cost and earned {Definition.Reward} revenue, creating {Definition.Reward - appliedRepairCost} profit.";
        }
    }
}
