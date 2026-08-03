using AIFounder.Application.Repair;
using AIFounder.Domain.Repair;
using NUnit.Framework;

namespace AIFounder.Tests.EditMode
{
    public sealed class RepairJobDomainTests
    {
        [Test]
        public void JobCanBeAcceptedFromAvailableStateButNotTwice()
        {
            var session = new RepairJobVerticalSliceSession();

            Assert.IsTrue(session.AcceptCurrentJob());
            Assert.AreEqual(RepairJobStatus.Accepted, session.CurrentJob.Status);
            Assert.IsFalse(session.AcceptCurrentJob());
        }

        [Test]
        public void RejectedJobDoesNotChangeCashOrCreateOutcome()
        {
            var session = new RepairJobVerticalSliceSession();
            int startingCash = session.CashLedger.Cash;

            Assert.IsTrue(session.RejectCurrentJob());

            Assert.AreEqual(startingCash, session.CashLedger.Cash);
            Assert.IsNull(session.CurrentJob.Outcome);
            Assert.AreEqual(RepairJobStatus.Rejected, session.CurrentJob.Status);
        }

        [Test]
        public void RepairCannotResolveBeforeAcceptance()
        {
            var session = new RepairJobVerticalSliceSession();

            Assert.IsFalse(session.ResolveRepair());
            Assert.AreEqual(500, session.CashLedger.Cash);
        }

        [Test]
        public void SelectedRepairMethodAppliesExpectedCostOnce()
        {
            var session = new RepairJobVerticalSliceSession();

            session.AcceptCurrentJob();
            session.SelectMethod("standard");

            Assert.IsTrue(session.ResolveRepair());
            Assert.AreEqual(380, session.CashLedger.Cash);
            Assert.IsFalse(session.ResolveRepair());
            Assert.AreEqual(380, session.CashLedger.Cash);
        }

        [Test]
        public void DeliveryAppliesRevenueOnceAndCalculatesProfit()
        {
            var session = CompleteFirstRepairThroughDelivery("standard");

            Assert.AreEqual(680, session.CashLedger.Cash);
            Assert.AreEqual(300, session.CurrentJob.Outcome.Revenue);
            Assert.AreEqual(120, session.CurrentJob.Outcome.RepairCost);
            Assert.AreEqual(180, session.CurrentJob.Outcome.Profit);
            Assert.IsFalse(session.DeliverCurrentJob());
            Assert.AreEqual(680, session.CashLedger.Cash);
        }

        [Test]
        public void DeliveryCannotOccurBeforeRepair()
        {
            var session = new RepairJobVerticalSliceSession();
            session.AcceptCurrentJob();

            Assert.IsFalse(session.DeliverCurrentJob());
            Assert.IsNull(session.CurrentJob.Outcome);
        }

        [Test]
        public void InsufficientCashPreventsUpgradePurchase()
        {
            var ledger = new CashLedger(50);
            var upgradeState = new RepairUpgradeState();
            var upgrade = new RepairUpgradeDefinition("Better Repair Tool", 120, 25, "Reduce method costs.");

            Assert.IsFalse(upgradeState.TryPurchase(ledger, upgrade));
            Assert.AreEqual(50, ledger.Cash);
            Assert.IsFalse(upgradeState.IsPurchased);
        }

        [Test]
        public void UpgradeCannotBePurchasedTwice()
        {
            var session = CompleteFirstRepairThroughDelivery("quick");

            Assert.IsTrue(session.PurchaseUpgrade());
            int cashAfterPurchase = session.CashLedger.Cash;
            Assert.IsFalse(session.PurchaseUpgrade());
            Assert.AreEqual(cashAfterPurchase, session.CashLedger.Cash);
        }

        [Test]
        public void UpgradeAffectsNextRepairJobMethodCosts()
        {
            var session = CompleteFirstRepairThroughDelivery("quick");
            session.RefreshNextJobPreview();
            int standardBeforeUpgrade = session.NextJobPreview.Definition.FindMethod("standard").GetCost(session.UpgradeState, session.UpgradeDefinition);

            Assert.IsTrue(session.PurchaseUpgrade());
            int standardAfterUpgrade = session.NextJobPreview.Definition.FindMethod("standard").GetCost(session.UpgradeState, session.UpgradeDefinition);

            Assert.AreEqual(145, standardBeforeUpgrade);
            Assert.AreEqual(120, standardAfterUpgrade);
        }

        [Test]
        public void CompletedJobCannotResolveOrDeliverAgain()
        {
            var session = CompleteFirstRepairThroughDelivery("reliable");

            Assert.IsFalse(session.ResolveRepair());
            Assert.IsFalse(session.DeliverCurrentJob());
        }

        [Test]
        public void OutcomeExplanationContainsRevenueCostAndProfitCauses()
        {
            var session = CompleteFirstRepairThroughDelivery("standard");
            string explanation = session.CurrentJob.Outcome.Explanation;

            Assert.That(explanation, Does.Contain("120"));
            Assert.That(explanation, Does.Contain("300"));
            Assert.That(explanation, Does.Contain("180"));
        }

        [Test]
        public void NextRepairJobCanBecomeDecisionPointAfterDelivery()
        {
            var session = CompleteFirstRepairThroughDelivery("standard");

            Assert.IsTrue(session.IsNextJobAvailable);
            Assert.IsNotNull(session.NextJobPreview);
            Assert.IsTrue(session.AcceptNextJob());
            Assert.AreEqual(RepairJobStatus.Accepted, session.CurrentJob.Status);
        }

        private static RepairJobVerticalSliceSession CompleteFirstRepairThroughDelivery(string methodId)
        {
            var session = new RepairJobVerticalSliceSession();
            Assert.IsTrue(session.AcceptCurrentJob());
            Assert.IsTrue(session.SelectMethod(methodId));
            Assert.IsTrue(session.ResolveRepair());
            Assert.IsTrue(session.DeliverCurrentJob());
            return session;
        }
    }
}
