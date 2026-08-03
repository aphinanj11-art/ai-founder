using System.Collections;
using AIFounder.Domain.Repair;
using AIFounder.Presentation;
using AIFounder.Presentation.Repair;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace AIFounder.Tests.PlayMode
{
    public sealed class RepairJobVerticalSlicePlayModeTests
    {
        private const string FirstPlayableSceneName = "MVP_A_FirstPlayable";

        [UnityTest]
        public IEnumerator WorkshopInteraction_OpensJobPanelInScene()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var controller = Object.FindFirstObjectByType<RepairJobPanelController>();
            var workshop = FindInteractionPoint("Workshop");

            Assert.IsNotNull(controller);
            Assert.IsNotNull(workshop);
            Assert.IsFalse(controller.IsOpen);

            workshop.Interact();

            Assert.IsTrue(controller.IsOpen);
            Assert.That(controller.VisibleBodyText, Does.Contain("Repair Damaged Workshop Pump"));
        }

        [UnityTest]
        public IEnumerator AcceptingJobUpdatesVisibleState()
        {
            var fixture = CreateFixture();
            fixture.Controller.OpenPanel();

            fixture.Controller.AcceptCurrentJob();

            Assert.AreEqual(RepairJobStatus.Accepted, fixture.Controller.Session.CurrentJob.Status);
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("State: Accepted"));
            Assert.That(fixture.Controller.VisibleFeedbackText, Does.Contain("accepted"));
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator SelectingAndConfirmingMethodRepairsJob()
        {
            var fixture = CreateFixture();
            fixture.Controller.OpenPanel();
            fixture.Controller.AcceptCurrentJob();

            fixture.Controller.SelectStandardRepair();
            fixture.Controller.ConfirmRepair();

            Assert.AreEqual(RepairJobStatus.Repaired, fixture.Controller.Session.CurrentJob.Status);
            Assert.AreEqual(380, fixture.Controller.Session.CashLedger.Cash);
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("Selected Method: Standard Repair"));
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator DeliveryPointBlocksDeliveryBeforeRepair()
        {
            var fixture = CreateFixture();
            fixture.Controller.OpenPanel();
            fixture.Controller.AcceptCurrentJob();
            int cashBefore = fixture.Controller.Session.CashLedger.Cash;

            fixture.Controller.DeliverFromDeliveryPoint();

            Assert.AreEqual(cashBefore, fixture.Controller.Session.CashLedger.Cash);
            Assert.That(fixture.Controller.VisibleFeedbackText, Does.Contain("blocked"));
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator DeliveryAfterRepairOpensOutcomeBreakdown()
        {
            var fixture = CreateFixture();
            CompleteRepair(fixture.Controller, "standard");

            fixture.Controller.DeliverFromDeliveryPoint();

            Assert.AreEqual(RepairJobStatus.Delivered, fixture.Controller.Session.CurrentJob.Status);
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("Outcome Breakdown"));
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("Profit: 180"));
            Assert.That(fixture.Controller.VisibleCashText, Does.Contain("680"));
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator UpgradePurchaseUpdatesCashAndVisibleNextJobEffect()
        {
            var fixture = CreateFixture();
            CompleteRepair(fixture.Controller, "quick");
            fixture.Controller.DeliverFromDeliveryPoint();

            fixture.Controller.PurchaseUpgrade();

            Assert.IsTrue(fixture.Controller.Session.UpgradeState.IsPurchased);
            Assert.AreEqual(600, fixture.Controller.Session.CashLedger.Cash);
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("Purchased"));
            Assert.That(fixture.Controller.VisibleBodyText, Does.Contain("Standard Repair: Cost 120"));
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator NextJobCanBeAcceptedFromDecisionPoint()
        {
            var fixture = CreateFixture();
            CompleteRepair(fixture.Controller, "standard");
            fixture.Controller.DeliverFromDeliveryPoint();

            fixture.Controller.AcceptNextJob();

            Assert.AreEqual("Repair Overheated Electric Motor", fixture.Controller.Session.CurrentJob.Definition.Title);
            Assert.AreEqual(RepairJobStatus.Accepted, fixture.Controller.Session.CurrentJob.Status);
            DestroyFixture(fixture);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ButtonClicksDoNotDuplicateEconomicMutation()
        {
            var fixture = CreateFixture();
            fixture.Controller.OpenPanel();
            fixture.Controller.AcceptCurrentJob();
            fixture.StandardRepairButton.onClick.Invoke();
            fixture.ConfirmRepairButton.onClick.Invoke();
            fixture.ConfirmRepairButton.onClick.Invoke();

            Assert.AreEqual(380, fixture.Controller.Session.CashLedger.Cash);
            fixture.DeliverButton.onClick.Invoke();
            fixture.DeliverButton.onClick.Invoke();
            Assert.AreEqual(680, fixture.Controller.Session.CashLedger.Cash);
            DestroyFixture(fixture);
            yield return null;
        }

        private static PrototypeInteractionPoint FindInteractionPoint(string label)
        {
            foreach (PrototypeInteractionPoint point in Object.FindObjectsByType<PrototypeInteractionPoint>(FindObjectsSortMode.None))
            {
                if (point.PromptLabel == label)
                {
                    return point;
                }
            }

            return null;
        }

        private static void CompleteRepair(RepairJobPanelController controller, string method)
        {
            controller.OpenPanel();
            controller.AcceptCurrentJob();
            if (method == "quick")
            {
                controller.SelectQuickPatch();
            }
            else if (method == "reliable")
            {
                controller.SelectReliableReplacement();
            }
            else
            {
                controller.SelectStandardRepair();
            }

            controller.ConfirmRepair();
        }

        private static RepairPanelFixture CreateFixture()
        {
            var root = new GameObject("repair-panel-test-root");
            root.SetActive(false);
            var controller = root.AddComponent<RepairJobPanelController>();
            var panelRoot = new GameObject("panel-root");
            panelRoot.transform.SetParent(root.transform);

            var titleText = CreateText(root.transform, "title");
            var bodyText = CreateText(root.transform, "body");
            var cashText = CreateText(root.transform, "cash");
            var feedbackText = CreateText(root.transform, "feedback");
            var acceptButton = CreateButton(root.transform, "accept");
            var closeButton = CreateButton(root.transform, "close");
            var quickButton = CreateButton(root.transform, "quick");
            var standardButton = CreateButton(root.transform, "standard");
            var reliableButton = CreateButton(root.transform, "reliable");
            var confirmButton = CreateButton(root.transform, "confirm");
            var deliverButton = CreateButton(root.transform, "deliver");
            var upgradeButton = CreateButton(root.transform, "upgrade");
            var nextButton = CreateButton(root.transform, "next");

            SetPrivateField(controller, "panelRoot", panelRoot);
            SetPrivateField(controller, "titleText", titleText);
            SetPrivateField(controller, "bodyText", bodyText);
            SetPrivateField(controller, "cashText", cashText);
            SetPrivateField(controller, "feedbackText", feedbackText);
            SetPrivateField(controller, "acceptButton", acceptButton);
            SetPrivateField(controller, "closeButton", closeButton);
            SetPrivateField(controller, "quickPatchButton", quickButton);
            SetPrivateField(controller, "standardRepairButton", standardButton);
            SetPrivateField(controller, "reliableReplacementButton", reliableButton);
            SetPrivateField(controller, "confirmRepairButton", confirmButton);
            SetPrivateField(controller, "deliverButton", deliverButton);
            SetPrivateField(controller, "purchaseUpgradeButton", upgradeButton);
            SetPrivateField(controller, "acceptNextJobButton", nextButton);
            root.SetActive(true);

            return new RepairPanelFixture(root, controller, standardButton, confirmButton, deliverButton);
        }

        private static Text CreateText(Transform parent, string name)
        {
            var gameObject = new GameObject(name);
            gameObject.transform.SetParent(parent);
            return gameObject.AddComponent<Text>();
        }

        private static Button CreateButton(Transform parent, string name)
        {
            var gameObject = new GameObject(name);
            gameObject.transform.SetParent(parent);
            return gameObject.AddComponent<Button>();
        }

        private static void DestroyFixture(RepairPanelFixture fixture)
        {
            Object.DestroyImmediate(fixture.Root);
        }

        private static void SetPrivateField<T>(object target, string fieldName, T value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field.SetValue(target, value);
        }

        private readonly struct RepairPanelFixture
        {
            public RepairPanelFixture(GameObject root, RepairJobPanelController controller, Button standardRepairButton, Button confirmRepairButton, Button deliverButton)
            {
                Root = root;
                Controller = controller;
                StandardRepairButton = standardRepairButton;
                ConfirmRepairButton = confirmRepairButton;
                DeliverButton = deliverButton;
            }

            public GameObject Root { get; }
            public RepairJobPanelController Controller { get; }
            public Button StandardRepairButton { get; }
            public Button ConfirmRepairButton { get; }
            public Button DeliverButton { get; }
        }
    }
}
