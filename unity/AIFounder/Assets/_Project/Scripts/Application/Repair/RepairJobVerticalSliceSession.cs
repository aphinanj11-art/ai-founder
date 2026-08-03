using AIFounder.Domain.Repair;

namespace AIFounder.Application.Repair
{
    public sealed class RepairJobVerticalSliceSession
    {
        private readonly RepairJobDefinition firstJobDefinition;
        private readonly RepairJobDefinition nextJobDefinition;
        private RepairJobState nextJobPreview;

        public RepairJobVerticalSliceSession()
        {
            CashLedger = new CashLedger(500);
            UpgradeDefinition = new RepairUpgradeDefinition(
                "Better Repair Tool",
                120,
                25,
                "Reduces each repair method cost on the next Repair Job by 25 cash.");
            UpgradeState = new RepairUpgradeState();
            firstJobDefinition = CreatePumpJob();
            nextJobDefinition = CreateMotorJob();
            CurrentJob = new RepairJobState(firstJobDefinition);
        }

        public CashLedger CashLedger { get; }
        public RepairUpgradeDefinition UpgradeDefinition { get; }
        public RepairUpgradeState UpgradeState { get; }
        public RepairJobState CurrentJob { get; private set; }
        public RepairJobState NextJobPreview => nextJobPreview;
        public bool IsNextJobAvailable => CurrentJob != null && CurrentJob.Status == RepairJobStatus.Delivered;

        public bool AcceptCurrentJob()
        {
            return CurrentJob != null && CurrentJob.Accept();
        }

        public bool RejectCurrentJob()
        {
            return CurrentJob != null && CurrentJob.Reject();
        }

        public bool SelectMethod(string methodId)
        {
            return CurrentJob != null && CurrentJob.SelectMethod(methodId);
        }

        public bool ResolveRepair()
        {
            return CurrentJob != null && CurrentJob.ResolveRepair(CashLedger, UpgradeState, UpgradeDefinition);
        }

        public bool DeliverCurrentJob()
        {
            if (CurrentJob == null || !CurrentJob.Deliver(CashLedger))
            {
                return false;
            }

            RefreshNextJobPreview();
            return true;
        }

        public bool ReviewOutcome()
        {
            return CurrentJob != null && CurrentJob.ReviewOutcome();
        }

        public bool PurchaseUpgrade()
        {
            bool purchased = UpgradeState.TryPurchase(CashLedger, UpgradeDefinition);
            if (purchased)
            {
                RefreshNextJobPreview();
            }

            return purchased;
        }

        public bool AcceptNextJob()
        {
            if (!IsNextJobAvailable)
            {
                return false;
            }

            CurrentJob = nextJobPreview ?? new RepairJobState(nextJobDefinition);
            nextJobPreview = null;
            return CurrentJob.Accept();
        }

        public void RefreshNextJobPreview()
        {
            if (IsNextJobAvailable)
            {
                nextJobPreview = new RepairJobState(nextJobDefinition);
            }
        }

        private static RepairJobDefinition CreatePumpJob()
        {
            return new RepairJobDefinition(
                "repair-pump-01",
                "Repair Damaged Workshop Pump",
                "Restore a leaking coolant pump enough for safe workshop use.",
                300,
                "Due today",
                new[]
                {
                    new RepairMethodDefinition("quick", "Quick Patch", 80, 55, "Lowest cost and fastest, but reliability remains modest."),
                    new RepairMethodDefinition("standard", "Standard Repair", 120, 75, "Balanced cost and reliable enough for normal use."),
                    new RepairMethodDefinition("reliable", "Reliable Replacement", 170, 92, "Highest cost, strongest reliability and clearest outcome.")
                });
        }

        private static RepairJobDefinition CreateMotorJob()
        {
            return new RepairJobDefinition(
                "repair-motor-02",
                "Repair Overheated Electric Motor",
                "Return a small shop motor to service without adding a full inspection job.",
                360,
                "Due next shift",
                new[]
                {
                    new RepairMethodDefinition("quick", "Quick Patch", 95, 54, "Low upfront cost, still leaves heat risk visible."),
                    new RepairMethodDefinition("standard", "Standard Repair", 145, 76, "Balanced repair with reduced heat risk."),
                    new RepairMethodDefinition("reliable", "Reliable Replacement", 195, 93, "Expensive but produces the strongest reliability explanation.")
                });
        }
    }
}
