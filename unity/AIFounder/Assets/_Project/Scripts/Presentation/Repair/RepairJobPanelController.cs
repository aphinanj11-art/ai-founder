using System.Text;
using AIFounder.Application.Repair;
using AIFounder.Domain.Repair;
using UnityEngine;
using UnityEngine.UI;

namespace AIFounder.Presentation.Repair
{
    public sealed class RepairJobPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private Text titleText;
        [SerializeField] private Text bodyText;
        [SerializeField] private Text cashText;
        [SerializeField] private Text feedbackText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button quickPatchButton;
        [SerializeField] private Button standardRepairButton;
        [SerializeField] private Button reliableReplacementButton;
        [SerializeField] private Button confirmRepairButton;
        [SerializeField] private Button deliverButton;
        [SerializeField] private Button purchaseUpgradeButton;
        [SerializeField] private Button acceptNextJobButton;

        private RepairJobVerticalSliceSession session;
        private bool isOpen;

        public RepairJobVerticalSliceSession Session => session;
        public bool IsOpen => isOpen;
        public string VisibleBodyText => bodyText != null ? bodyText.text : string.Empty;
        public string VisibleFeedbackText => feedbackText != null ? feedbackText.text : string.Empty;
        public string VisibleCashText => cashText != null ? cashText.text : string.Empty;

        private void Awake()
        {
            session = new RepairJobVerticalSliceSession();
            WireButtons();
            ClosePanel();
        }

        public void OpenPanel()
        {
            isOpen = true;
            if (panelRoot != null)
            {
                panelRoot.SetActive(true);
            }

            Refresh("Workshop job panel opened.");
        }

        public void ClosePanel()
        {
            isOpen = false;
            if (panelRoot != null)
            {
                panelRoot.SetActive(false);
            }
        }

        public void AcceptCurrentJob()
        {
            Refresh(session.AcceptCurrentJob() ? "Repair Job accepted." : "Cannot accept this job right now.");
        }

        public void CloseWithoutMutation()
        {
            Refresh("Panel closed. Cash and job outcome unchanged.");
            ClosePanel();
        }

        public void SelectQuickPatch()
        {
            SelectMethod("quick");
        }

        public void SelectStandardRepair()
        {
            SelectMethod("standard");
        }

        public void SelectReliableReplacement()
        {
            SelectMethod("reliable");
        }

        public void ConfirmRepair()
        {
            Refresh(session.ResolveRepair() ? "Repair completed. Deliver the result at the Delivery Point." : "Repair cannot be completed yet.");
        }

        public void DeliverFromDeliveryPoint()
        {
            if (!isOpen)
            {
                OpenPanel();
            }

            Refresh(session.DeliverCurrentJob() ? "Delivery complete. Review the outcome." : "Delivery blocked: repair must be completed first.");
        }

        public void PurchaseUpgrade()
        {
            Refresh(session.PurchaseUpgrade() ? "Better Repair Tool purchased. Next job methods now cost less." : "Upgrade purchase failed.");
        }

        public void AcceptNextJob()
        {
            Refresh(session.AcceptNextJob() ? "Next Repair Job accepted." : "Next Repair Job is not available yet.");
        }

        private void SelectMethod(string methodId)
        {
            Refresh(session.SelectMethod(methodId) ? $"Selected repair method: {session.CurrentJob.SelectedMethod.DisplayName}." : "Select method failed. Accept the job first.");
        }

        private void Refresh(string feedback)
        {
            if (session == null)
            {
                return;
            }

            if (titleText != null)
            {
                titleText.text = "Repair Job Vertical Slice";
            }

            if (cashText != null)
            {
                cashText.text = $"Cash: {session.CashLedger.Cash}";
            }

            if (feedbackText != null)
            {
                feedbackText.text = feedback;
            }

            if (bodyText != null)
            {
                bodyText.text = BuildBodyText();
            }

            RefreshButtonState();
        }

        private string BuildBodyText()
        {
            RepairJobState job = session.CurrentJob;
            var builder = new StringBuilder();
            builder.AppendLine($"Job: {job.Definition.Title}");
            builder.AppendLine($"Requirement: {job.Definition.Requirement}");
            builder.AppendLine($"Reward: {job.Definition.Reward}");
            builder.AppendLine($"Time Allowance: {job.Definition.TimeAllowance}");
            builder.AppendLine($"State: {job.Status}");
            builder.AppendLine();
            builder.AppendLine("Repair Methods:");
            foreach (RepairMethodDefinition method in job.Definition.Methods)
            {
                int cost = method.GetCost(session.UpgradeState, session.UpgradeDefinition);
                builder.AppendLine($"- {method.DisplayName}: Cost {cost}, Reliability {method.Reliability}. {method.TradeOff}");
            }

            if (job.SelectedMethod != null)
            {
                builder.AppendLine();
                builder.AppendLine($"Selected Method: {job.SelectedMethod.DisplayName}");
            }

            if (job.Outcome != null)
            {
                builder.AppendLine();
                builder.AppendLine("Outcome Breakdown:");
                builder.AppendLine($"Method: {job.Outcome.MethodName}");
                builder.AppendLine($"Revenue: {job.Outcome.Revenue}");
                builder.AppendLine($"Repair Cost: {job.Outcome.RepairCost}");
                builder.AppendLine($"Profit: {job.Outcome.Profit}");
                builder.AppendLine($"Cash: {job.Outcome.CashBefore} -> {job.Outcome.CashAfter}");
                builder.AppendLine(job.Outcome.Explanation);
            }

            builder.AppendLine();
            builder.AppendLine($"Upgrade: {session.UpgradeDefinition.Title} ({session.UpgradeDefinition.Cost})");
            builder.AppendLine(session.UpgradeState.IsPurchased ? "Purchased: next job method costs are reduced." : session.UpgradeDefinition.EffectDescription);

            if (session.NextJobPreview != null)
            {
                builder.AppendLine();
                builder.AppendLine("Next Repair Job Available:");
                builder.AppendLine(session.NextJobPreview.Definition.Title);
                foreach (RepairMethodDefinition method in session.NextJobPreview.Definition.Methods)
                {
                    builder.AppendLine($"- {method.DisplayName}: Cost {method.GetCost(session.UpgradeState, session.UpgradeDefinition)}");
                }
            }

            return builder.ToString();
        }

        private void RefreshButtonState()
        {
            RepairJobStatus status = session.CurrentJob.Status;
            SetInteractable(acceptButton, status == RepairJobStatus.Available);
            SetInteractable(quickPatchButton, status == RepairJobStatus.Accepted);
            SetInteractable(standardRepairButton, status == RepairJobStatus.Accepted);
            SetInteractable(reliableReplacementButton, status == RepairJobStatus.Accepted);
            SetInteractable(confirmRepairButton, status == RepairJobStatus.MethodSelected);
            SetInteractable(deliverButton, status == RepairJobStatus.Repaired);
            SetInteractable(purchaseUpgradeButton, status == RepairJobStatus.Delivered && !session.UpgradeState.IsPurchased && session.CashLedger.Cash >= session.UpgradeDefinition.Cost);
            SetInteractable(acceptNextJobButton, session.IsNextJobAvailable);
        }

        private static void SetInteractable(Button button, bool interactable)
        {
            if (button != null)
            {
                button.interactable = interactable;
            }
        }

        private void WireButtons()
        {
            AddListener(acceptButton, AcceptCurrentJob);
            AddListener(closeButton, CloseWithoutMutation);
            AddListener(quickPatchButton, SelectQuickPatch);
            AddListener(standardRepairButton, SelectStandardRepair);
            AddListener(reliableReplacementButton, SelectReliableReplacement);
            AddListener(confirmRepairButton, ConfirmRepair);
            AddListener(deliverButton, DeliverFromDeliveryPoint);
            AddListener(purchaseUpgradeButton, PurchaseUpgrade);
            AddListener(acceptNextJobButton, AcceptNextJob);
        }

        private static void AddListener(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(action);
                button.onClick.AddListener(action);
            }
        }
    }
}
