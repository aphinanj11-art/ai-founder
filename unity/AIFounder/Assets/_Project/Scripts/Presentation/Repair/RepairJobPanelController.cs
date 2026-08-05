using System.Text;
using AIFounder.Application.Repair;
using AIFounder.Domain.Repair;
using AIFounder.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace AIFounder.Presentation.Repair
{
    public sealed class RepairJobPanelController : MonoBehaviour
    {
        private static readonly Color ActiveMethodColor = new Color(0.95f, 0.74f, 0.28f, 1f);
        private static readonly Color NormalButtonColor = new Color(0.20f, 0.26f, 0.30f, 1f);
        private static readonly Color DisabledButtonColor = new Color(0.24f, 0.24f, 0.24f, 0.76f);

        [SerializeField] private GameObject panelRoot;
        [SerializeField] private Text titleText;
        [SerializeField] private Text bodyText;
        [SerializeField] private Text cashText;
        [SerializeField] private Text feedbackText;
        [SerializeField] private Text objectiveText;
        [SerializeField] private Text loopProgressText;
        [SerializeField] private InteractionPromptHud promptHud;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject scrollContentRoot;
        [SerializeField] private GameObject actionBarRoot;
        [SerializeField] private GameObject jobInfoSection;
        [SerializeField] private GameObject repairMethodsSection;
        [SerializeField] private GameObject selectedMethodSection;
        [SerializeField] private GameObject deliverySection;
        [SerializeField] private GameObject outcomeSection;
        [SerializeField] private GameObject upgradeSection;
        [SerializeField] private GameObject nextJobSection;
        [SerializeField] private GameObject acceptCloseActions;
        [SerializeField] private GameObject methodActions;
        [SerializeField] private GameObject confirmActions;
        [SerializeField] private GameObject deliverActions;
        [SerializeField] private GameObject postDeliveryActions;
        [SerializeField] private Text jobInfoText;
        [SerializeField] private Text repairMethodsText;
        [SerializeField] private Text selectedMethodText;
        [SerializeField] private Text deliveryText;
        [SerializeField] private Text outcomeText;
        [SerializeField] private Text upgradeText;
        [SerializeField] private Text nextJobText;
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
        public string VisibleBodyText => BuildVisibleBodyText();
        public string VisibleFeedbackText => feedbackText != null ? feedbackText.text : string.Empty;
        public string VisibleCashText => cashText != null ? cashText.text : string.Empty;
        public string VisibleObjectiveText => objectiveText != null ? objectiveText.text : string.Empty;
        public string VisibleProgressText => loopProgressText != null ? loopProgressText.text : string.Empty;

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

            SetPromptSuppressed(true);
            Refresh("Workshop job panel opened.");
        }

        public void ClosePanel()
        {
            isOpen = false;
            if (panelRoot != null)
            {
                panelRoot.SetActive(false);
            }

            SetPromptSuppressed(false);
            RefreshObjectiveAndProgress();
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

            RefreshObjectiveAndProgress();
            if (bodyText != null)
            {
                bodyText.text = BuildBodyText();
            }

            RefreshSectionText();
            RefreshButtonState();
        }

        private string BuildBodyText()
        {
            RepairJobState job = session.CurrentJob;
            var builder = new StringBuilder();
            builder.AppendLine($"State: {job.Status}");

            if (job.Status == RepairJobStatus.Available)
            {
                builder.AppendLine($"Job: {job.Definition.Title}");
                builder.AppendLine($"Requirement: {job.Definition.Requirement}");
                builder.AppendLine($"Reward: {job.Definition.Reward}");
                builder.AppendLine($"Deadline: {job.Definition.TimeAllowance}");
                return builder.ToString();
            }

            if (job.Status == RepairJobStatus.Accepted || job.Status == RepairJobStatus.MethodSelected)
            {
                builder.AppendLine($"Job: {job.Definition.Title}");
                builder.AppendLine("Decision: choose a repair method.");
                builder.AppendLine("Repair Methods:");
                foreach (RepairMethodDefinition method in job.Definition.Methods)
                {
                    int cost = method.GetCost(session.UpgradeState, session.UpgradeDefinition);
                    builder.AppendLine($"{method.DisplayName}: Cost {cost}, Reliability {method.Reliability}");
                }
            }

            if (job.SelectedMethod != null)
            {
                builder.AppendLine($"Selected Method: {job.SelectedMethod.DisplayName}");
                builder.AppendLine($"Selected Cost: {job.SelectedMethod.GetCost(session.UpgradeState, session.UpgradeDefinition)}");
            }

            if (job.Status == RepairJobStatus.Repaired)
            {
                builder.AppendLine("Next Action: Deliver repaired work.");
            }

            if (job.Outcome != null)
            {
                builder.AppendLine("Outcome Breakdown:");
                builder.AppendLine($"Revenue: {job.Outcome.Revenue}");
                builder.AppendLine($"Repair Cost: {job.Outcome.RepairCost}");
                builder.AppendLine($"Profit: {job.Outcome.Profit}");
                builder.AppendLine($"Cash Before: {job.Outcome.CashBefore}");
                builder.AppendLine($"Cash After: {job.Outcome.CashAfter}");
            }

            if (job.Status == RepairJobStatus.Delivered)
            {
                builder.AppendLine(session.UpgradeState.IsPurchased ? "Upgrade: Purchased" : $"Upgrade: {session.UpgradeDefinition.Title} ({session.UpgradeDefinition.Cost})");
                builder.AppendLine(session.UpgradeState.IsPurchased ? "Next job method costs are reduced." : session.UpgradeDefinition.EffectDescription);
            }

            if (session.NextJobPreview != null)
            {
                builder.AppendLine($"Next Repair Job Available: {session.NextJobPreview.Definition.Title}");
                foreach (RepairMethodDefinition method in session.NextJobPreview.Definition.Methods)
                {
                    builder.AppendLine($"{method.DisplayName}: Cost {method.GetCost(session.UpgradeState, session.UpgradeDefinition)}");
                }
            }

            return builder.ToString();
        }

        private void RefreshSectionText()
        {
            RepairJobState job = session.CurrentJob;
            if (jobInfoText != null)
            {
                jobInfoText.text = BuildJobInfoText(job);
            }

            if (repairMethodsText != null)
            {
                repairMethodsText.text = BuildRepairMethodsText(job);
            }

            if (selectedMethodText != null)
            {
                selectedMethodText.text = job.SelectedMethod != null
                    ? $"Selected Method\n{job.SelectedMethod.DisplayName}\nCost: {job.SelectedMethod.GetCost(session.UpgradeState, session.UpgradeDefinition)}\nReliability: {job.SelectedMethod.Reliability}\n{job.SelectedMethod.TradeOff}"
                    : "Selected Method\nChoose a repair method to continue.";
            }

            if (deliveryText != null)
            {
                deliveryText.text = "Delivery\nRepair complete. Go to the Delivery Point or use Deliver to submit the finished work.";
            }

            if (outcomeText != null)
            {
                outcomeText.text = job.Outcome != null ? BuildOutcomeText(job.Outcome) : "Outcome Breakdown\nNo delivered outcome yet.";
            }

            if (upgradeText != null)
            {
                upgradeText.text = BuildUpgradeText();
            }

            if (nextJobText != null)
            {
                nextJobText.text = session.NextJobPreview != null ? BuildNextJobText() : "Next Repair Job\nNo next job available yet.";
            }
        }

        private string BuildJobInfoText(RepairJobState job)
        {
            if (job.Status == RepairJobStatus.Repaired)
            {
                return $"Job\n{job.Definition.Title}";
            }

            return $"Job\n{job.Definition.Title}\nNeed: {job.Definition.Requirement}\nReward: {job.Definition.Reward}\nDeadline: {job.Definition.TimeAllowance}";
        }

        private string BuildRepairMethodsText(RepairJobState job)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Choose Repair Method");
            foreach (RepairMethodDefinition method in job.Definition.Methods)
            {
                int cost = method.GetCost(session.UpgradeState, session.UpgradeDefinition);
                bool selected = job.SelectedMethod != null && job.SelectedMethod.Id == method.Id;
                builder.AppendLine($"{(selected ? "> " : string.Empty)}{method.DisplayName}");
                builder.AppendLine($"Cost {cost} | Reliability {method.Reliability}");
                builder.AppendLine(method.TradeOff);
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static string BuildOutcomeText(RepairOutcome outcome)
        {
            return $"Outcome\nRevenue {outcome.Revenue}\n- Repair Cost {outcome.RepairCost}\n= Profit {outcome.Profit}\nCash Before: {outcome.CashBefore}\nCash After: {outcome.CashAfter}\nCash {outcome.CashBefore} -> {outcome.CashAfter}\n{outcome.Explanation}";
        }

        private string BuildUpgradeText()
        {
            return session.UpgradeState.IsPurchased
                ? $"Upgrade\nPurchased\nNext-job repair costs reduced"
                : $"Upgrade\n{session.UpgradeDefinition.Title}\nCost {session.UpgradeDefinition.Cost}\nEffect: Next Repair Job methods cost {session.UpgradeDefinition.MethodCostReduction} less";
        }

        private string BuildNextJobText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Next Repair Job");
            builder.AppendLine(session.NextJobPreview.Definition.Title);
            builder.AppendLine($"Reward: {session.NextJobPreview.Definition.Reward}");
            builder.AppendLine(session.UpgradeState.IsPurchased ? "Upgrade effect: repair costs reduced" : "Upgrade effect: not purchased");
            foreach (RepairMethodDefinition method in session.NextJobPreview.Definition.Methods)
            {
                builder.AppendLine($"{method.DisplayName}: Cost {method.GetCost(session.UpgradeState, session.UpgradeDefinition)}");
            }

            return builder.ToString();
        }

        private void RefreshButtonState()
        {
            RepairJobStatus status = session.CurrentJob.Status;
            bool isAvailable = status == RepairJobStatus.Available;
            bool isAccepted = status == RepairJobStatus.Accepted;
            bool isMethodSelected = status == RepairJobStatus.MethodSelected;
            bool isRepaired = status == RepairJobStatus.Repaired;
            bool isDelivered = status == RepairJobStatus.Delivered;
            bool canPurchaseUpgrade = isDelivered && !session.UpgradeState.IsPurchased && session.CashLedger.Cash >= session.UpgradeDefinition.Cost;
            bool canAcceptNextJob = session.IsNextJobAvailable;

            SetActive(jobInfoSection, isAvailable || isAccepted || isMethodSelected || isRepaired);
            SetActive(repairMethodsSection, isAccepted || isMethodSelected);
            SetActive(selectedMethodSection, isAccepted || isMethodSelected || isRepaired);
            SetActive(deliverySection, isRepaired);
            SetActive(outcomeSection, isDelivered);
            SetActive(upgradeSection, isDelivered);
            SetActive(nextJobSection, canAcceptNextJob);
            SetActive(acceptCloseActions, isAvailable);
            SetActive(methodActions, isAccepted || isMethodSelected);
            SetActive(confirmActions, isMethodSelected);
            SetActive(deliverActions, isRepaired);
            SetActive(postDeliveryActions, isDelivered);

            SetVisibleAndInteractable(acceptButton, isAvailable, isAvailable);
            SetVisibleAndInteractable(closeButton, isAvailable, isAvailable);
            SetVisibleAndInteractable(quickPatchButton, isAccepted || isMethodSelected, isAccepted);
            SetVisibleAndInteractable(standardRepairButton, isAccepted || isMethodSelected, isAccepted);
            SetVisibleAndInteractable(reliableReplacementButton, isAccepted || isMethodSelected, isAccepted);
            SetVisibleAndInteractable(confirmRepairButton, isMethodSelected, isMethodSelected);
            SetVisibleAndInteractable(deliverButton, isRepaired, isRepaired);
            SetVisibleAndInteractable(purchaseUpgradeButton, isDelivered, canPurchaseUpgrade);
            SetVisibleAndInteractable(acceptNextJobButton, canAcceptNextJob, canAcceptNextJob);
            RefreshMethodButtonLabels();
            RefreshMethodButtonVisuals();

            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition = 1f;
            }
        }

        private void RefreshObjectiveAndProgress()
        {
            if (objectiveText != null)
            {
                objectiveText.text = $"Objective: {BuildObjectiveText()}";
            }

            if (loopProgressText != null)
            {
                loopProgressText.text = BuildProgressText();
            }
        }

        private string BuildObjectiveText()
        {
            if (!isOpen)
            {
                return "Go to the Workshop";
            }

            RepairJobStatus status = session.CurrentJob.Status;
            if (status == RepairJobStatus.Available)
            {
                return "Review the repair job";
            }

            if (status == RepairJobStatus.Accepted)
            {
                return "Select a repair method";
            }

            if (status == RepairJobStatus.MethodSelected)
            {
                return "Confirm the repair";
            }

            if (status == RepairJobStatus.Repaired)
            {
                return "Deliver the repaired pump";
            }

            if (status == RepairJobStatus.Delivered)
            {
                return session.UpgradeState.IsPurchased ? "Review the next job" : "Purchase the tool upgrade or continue";
            }

            return "Go to the Workshop";
        }

        private string BuildProgressText()
        {
            string currentStep = BuildCurrentProgressStep();
            string[] steps = { "Job", "Repair", "Delivery", "Outcome", "Upgrade", "Next Job" };
            var builder = new StringBuilder();
            for (int i = 0; i < steps.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append(" -> ");
                }

                builder.Append(steps[i] == currentStep ? $"[{steps[i]}]" : steps[i]);
            }

            return builder.ToString();
        }

        private string BuildCurrentProgressStep()
        {
            if (!isOpen || session.CurrentJob.Status == RepairJobStatus.Available)
            {
                return "Job";
            }

            if (session.CurrentJob.Status == RepairJobStatus.Accepted || session.CurrentJob.Status == RepairJobStatus.MethodSelected)
            {
                return "Repair";
            }

            if (session.CurrentJob.Status == RepairJobStatus.Repaired)
            {
                return "Delivery";
            }

            if (session.CurrentJob.Status == RepairJobStatus.Delivered && !session.UpgradeState.IsPurchased)
            {
                return "Upgrade";
            }

            if (session.CurrentJob.Status == RepairJobStatus.Delivered && session.UpgradeState.IsPurchased)
            {
                return "Next Job";
            }

            return "Job";
        }

        private void RefreshMethodButtonLabels()
        {
            SetMethodButtonLabel(quickPatchButton, "quick");
            SetMethodButtonLabel(standardRepairButton, "standard");
            SetMethodButtonLabel(reliableReplacementButton, "reliable");
        }

        private void SetMethodButtonLabel(Button button, string methodId)
        {
            RepairMethodDefinition method = session.CurrentJob.Definition.FindMethod(methodId);
            Text label = button != null ? button.GetComponentInChildren<Text>(true) : null;
            if (method == null || label == null)
            {
                return;
            }

            label.text = $"{method.DisplayName}\nCost {method.GetCost(session.UpgradeState, session.UpgradeDefinition)} | Rel {method.Reliability}";
        }

        private void RefreshMethodButtonVisuals()
        {
            string selectedId = session.CurrentJob.SelectedMethod != null ? session.CurrentJob.SelectedMethod.Id : string.Empty;
            SetMethodButtonVisual(quickPatchButton, selectedId == "quick");
            SetMethodButtonVisual(standardRepairButton, selectedId == "standard");
            SetMethodButtonVisual(reliableReplacementButton, selectedId == "reliable");
        }

        private static void SetMethodButtonVisual(Button button, bool selected)
        {
            if (button == null)
            {
                return;
            }

            Image image = button.GetComponent<Image>();
            if (image != null)
            {
                image.color = selected ? ActiveMethodColor : (button.interactable ? NormalButtonColor : DisabledButtonColor);
            }
        }

        private static void SetVisibleAndInteractable(Button button, bool visible, bool interactable)
        {
            if (button != null)
            {
                button.gameObject.SetActive(visible);
                button.interactable = interactable;
                Image image = button.GetComponent<Image>();
                if (image != null)
                {
                    image.color = interactable ? NormalButtonColor : DisabledButtonColor;
                }
            }
        }

        private static void SetActive(GameObject target, bool active)
        {
            if (target != null)
            {
                target.SetActive(active);
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

        private void SetPromptSuppressed(bool suppressed)
        {
            if (promptHud != null)
            {
                promptHud.SetSuppressed(suppressed);
            }
        }

        private string BuildVisibleBodyText()
        {
            if (bodyText != null)
            {
                return bodyText.text;
            }

            var builder = new StringBuilder();
            AppendVisibleText(builder, jobInfoText);
            AppendVisibleText(builder, repairMethodsText);
            AppendVisibleText(builder, selectedMethodText);
            AppendVisibleText(builder, deliveryText);
            AppendVisibleText(builder, outcomeText);
            AppendVisibleText(builder, upgradeText);
            AppendVisibleText(builder, nextJobText);
            return builder.ToString();
        }

        private static void AppendVisibleText(StringBuilder builder, Text text)
        {
            if (text != null && text.gameObject.activeInHierarchy)
            {
                builder.AppendLine(text.text);
            }
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
