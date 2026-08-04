using System.Collections;
using AIFounder.Domain.Repair;
using AIFounder.Presentation;
using AIFounder.Presentation.Repair;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
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
        public IEnumerator SceneContainsInputSystemEventPipelineForRepairPanel()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            yield return null;

            EventSystem[] eventSystems = Object.FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
            Canvas repairCanvas = FindCanvas("Repair Job Panel Canvas");

            Assert.AreEqual(1, eventSystems.Length);
            Assert.IsNotNull(eventSystems[0].currentInputModule);
            Assert.AreEqual("UnityEngine.InputSystem.UI.InputSystemUIInputModule", eventSystems[0].currentInputModule.GetType().FullName);
            Assert.IsNotNull(repairCanvas);
            Assert.IsNotNull(repairCanvas.GetComponent<GraphicRaycaster>());
        }

        [UnityTest]
        public IEnumerator RepairPanelLayoutUsesScrollableContentAndFixedActionBar()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);

            ScrollRect[] scrollRects = Object.FindObjectsByType<ScrollRect>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            GameObject contentRoot = FindGameObject("Content Root");
            GameObject actionBar = FindGameObject("Fixed Action Bar");

            Assert.AreEqual(1, scrollRects.Length);
            Assert.IsNotNull(contentRoot);
            Assert.IsNotNull(contentRoot.GetComponent<VerticalLayoutGroup>());
            Assert.IsNotNull(contentRoot.GetComponent<ContentSizeFitter>());
            Assert.IsNotNull(actionBar);
            Assert.IsNotNull(actionBar.GetComponent<VerticalLayoutGroup>());
            Assert.IsFalse(actionBar.transform.IsChildOf(contentRoot.transform));
        }

        [UnityTest]
        public IEnumerator ObjectiveHudAndProgressReflectRepairLoopStates()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var workshop = FindInteractionPoint("Workshop");

            AssertObjective("Go to the Workshop");
            AssertProgress("[Job]");

            workshop.Interact();
            AssertObjective("Review the repair job");
            AssertProgress("[Job]");

            ClickSceneButton("Accept Button");
            AssertObjective("Select a repair method");
            AssertProgress("[Repair]");

            ClickSceneButton("Standard Repair Button");
            AssertObjective("Confirm the repair");
            AssertProgress("[Repair]");

            ClickSceneButton("Confirm Repair Button");
            AssertObjective("Deliver the repaired pump");
            AssertProgress("[Delivery]");

            ClickSceneButton("Deliver Button");
            AssertObjective("Purchase the tool upgrade or continue");
            AssertProgress("[Upgrade]");

            ClickSceneButton("Purchase Upgrade Button");
            AssertObjective("Review the next job");
            AssertProgress("[Next Job]");
        }

        [UnityTest]
        public IEnumerator InteractionPromptIsSuppressedWhileRepairPanelIsOpenAndReturnsAfterClose()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var promptHud = Object.FindFirstObjectByType<InteractionPromptHud>();
            var workshop = FindInteractionPoint("Workshop");

            promptHud.Show(workshop);
            Assert.IsTrue(promptHud.IsVisible);

            workshop.Interact();
            Assert.IsFalse(promptHud.IsVisible);
            Assert.IsTrue(promptHud.IsSuppressed);

            ClickSceneButton("Close Button");
            promptHud.Show(workshop);
            Assert.IsFalse(promptHud.IsSuppressed);
            Assert.IsTrue(promptHud.IsVisible);
            Assert.That(promptHud.VisibleText, Does.Contain("Workshop"));
        }

        [UnityTest]
        public IEnumerator SceneLocationsHaveReadableLabelsAndDistinctColors()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);

            PrototypeInteractionPoint workshop = FindInteractionPoint("Workshop");
            PrototypeInteractionPoint shop = FindInteractionPoint("Nearby Shop");
            PrototypeInteractionPoint delivery = FindInteractionPoint("Delivery Point");

            AssertLocationLabel(workshop, "Workshop");
            AssertLocationLabel(shop, "Nearby Shop");
            AssertLocationLabel(delivery, "Delivery Point");

            Color workshopColor = workshop.GetComponent<PrototypeLocationVisual>().MarkerColor;
            Color shopColor = shop.GetComponent<PrototypeLocationVisual>().MarkerColor;
            Color deliveryColor = delivery.GetComponent<PrototypeLocationVisual>().MarkerColor;
            Assert.AreNotEqual(workshopColor, shopColor);
            Assert.AreNotEqual(workshopColor, deliveryColor);
            Assert.AreNotEqual(shopColor, deliveryColor);
        }

        [UnityTest]
        public IEnumerator RepairPanelStateVisibilityShowsOnlyRelevantSectionsAndControls()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();

            AssertSectionVisible("Job Information Section", true);
            AssertSectionVisible("Outcome Section", false);
            AssertButtonVisible("Accept Button", true, true);
            AssertButtonVisible("Close Button", true, true);
            AssertButtonVisible("Deliver Button", false, false);
            AssertButtonVisible("Purchase Upgrade Button", false, false);
            AssertButtonVisible("Accept Next Job Button", false, false);

            ClickSceneButton("Accept Button");
            AssertSectionVisible("Repair Methods Section", true);
            AssertButtonVisible("Quick Patch Button", true, true);
            AssertButtonVisible("Standard Repair Button", true, true);
            AssertButtonVisible("Reliable Replacement Button", true, true);
            AssertButtonVisible("Confirm Repair Button", false, false);

            ClickSceneButton("Standard Repair Button");
            AssertSectionVisible("Selected Method Section", true);
            AssertButtonVisible("Confirm Repair Button", true, true);

            ClickSceneButton("Confirm Repair Button");
            AssertSectionVisible("Delivery Section", true);
            AssertSectionVisible("Outcome Section", false);
            AssertButtonVisible("Quick Patch Button", false, false);
            AssertButtonVisible("Standard Repair Button", false, false);
            AssertButtonVisible("Reliable Replacement Button", false, false);
            AssertButtonVisible("Deliver Button", true, true);

            ClickSceneButton("Deliver Button");
            AssertSectionVisible("Outcome Section", true);
            AssertSectionVisible("Upgrade Section", true);
            AssertSectionVisible("Next Job Section", true);
            AssertButtonVisible("Purchase Upgrade Button", true, true);
            AssertButtonVisible("Accept Next Job Button", true, true);
            AssertButtonVisible("Confirm Repair Button", false, false);
        }

        [UnityTest]
        public IEnumerator MethodSelectionAndDisabledControlsHaveDistinctVisualStates()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();
            ClickSceneButton("Accept Button");

            Button standardButton = FindButton("Standard Repair Button");
            Color unselectedColor = standardButton.GetComponent<Image>().color;

            ClickSceneButton("Standard Repair Button");
            Color selectedColor = standardButton.GetComponent<Image>().color;
            Assert.AreNotEqual(unselectedColor, selectedColor);
            Assert.That(FindGameObject("Repair Methods Section Text").GetComponent<Text>().text, Does.Contain("> Standard Repair"));

            ClickSceneButton("Confirm Repair Button");
            ClickSceneButton("Deliver Button");
            ClickSceneButton("Purchase Upgrade Button");

            Button upgradeButton = FindButton("Purchase Upgrade Button");
            Assert.IsTrue(upgradeButton.gameObject.activeInHierarchy);
            Assert.IsFalse(upgradeButton.interactable);
            Assert.Less(upgradeButton.GetComponent<Image>().color.a, selectedColor.a);
        }

        [UnityTest]
        public IEnumerator OutcomeBreakdownUsesCompactFinancialLabels()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();
            ClickSceneButton("Accept Button");
            ClickSceneButton("Quick Patch Button");
            ClickSceneButton("Confirm Repair Button");
            ClickSceneButton("Deliver Button");

            string outcome = FindGameObject("Outcome Section Text").GetComponent<Text>().text;
            Assert.That(outcome, Does.Contain("Revenue: 300"));
            Assert.That(outcome, Does.Contain("Repair Cost: 80"));
            Assert.That(outcome, Does.Contain("Profit: 220"));
            Assert.That(outcome, Does.Contain("Cash Before: 500"));
            Assert.That(outcome, Does.Contain("Cash After: 720"));
        }

        [UnityTest]
        public IEnumerator ActiveRepairPanelButtonsStayInsidePanelBoundsAtFullHd()
        {
            Screen.SetResolution(1920, 1080, false);
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            yield return null;

            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();
            AssertActiveButtonsInsidePanel();

            ClickSceneButton("Accept Button");
            AssertActiveButtonsInsidePanel();
            ClickSceneButton("Reliable Replacement Button");
            AssertActiveButtonsInsidePanel();
            ClickSceneButton("Confirm Repair Button");
            AssertActiveButtonsInsidePanel();
            ClickSceneButton("Deliver Button");
            AssertActiveButtonsInsidePanel();
        }

        [UnityTest]
        public IEnumerator ScrollContentDoesNotOverlapFixedActionBarAtFullHd()
        {
            Screen.SetResolution(1920, 1080, false);
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            yield return null;

            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();

            AssertScrollContentDoesNotOverlapActionBar();
        }

        [UnityTest]
        public IEnumerator RequiredRepairPanelUiFitsAtSixteenHundredByNineHundred()
        {
            Screen.SetResolution(1600, 900, false);
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            yield return null;

            var workshop = FindInteractionPoint("Workshop");
            workshop.Interact();
            AssertActiveButtonsInsidePanel();
            AssertScrollContentDoesNotOverlapActionBar();

            ClickSceneButton("Accept Button");
            AssertActiveButtonsInsidePanel();
            AssertScrollContentDoesNotOverlapActionBar();
            ClickSceneButton("Reliable Replacement Button");
            AssertActiveButtonsInsidePanel();
            ClickSceneButton("Confirm Repair Button");
            AssertActiveButtonsInsidePanel();
            ClickSceneButton("Deliver Button");
            AssertActiveButtonsInsidePanel();
            AssertScrollContentDoesNotOverlapActionBar();
        }

        [UnityTest]
        public IEnumerator SceneRepairPanelButtonsReceivePointerClicks()
        {
            yield return SceneManager.LoadSceneAsync(FirstPlayableSceneName, LoadSceneMode.Single);
            var controller = Object.FindFirstObjectByType<RepairJobPanelController>();
            var workshop = FindInteractionPoint("Workshop");

            workshop.Interact();
            Assert.IsTrue(controller.IsOpen);
            ClickSceneButton("Close Button");
            Assert.IsFalse(controller.IsOpen);
            Assert.AreEqual(500, controller.Session.CashLedger.Cash);
            Assert.AreEqual(RepairJobStatus.Available, controller.Session.CurrentJob.Status);

            workshop.Interact();
            ClickSceneButton("Accept Button");
            Assert.AreEqual(RepairJobStatus.Accepted, controller.Session.CurrentJob.Status);

            ClickSceneButton("Quick Patch Button");
            Assert.AreEqual("Quick Patch", controller.Session.CurrentJob.SelectedMethod.DisplayName);

            ClickSceneButton("Confirm Repair Button");
            Assert.AreEqual(RepairJobStatus.Repaired, controller.Session.CurrentJob.Status);
            Assert.AreEqual(420, controller.Session.CashLedger.Cash);

            ClickSceneButton("Deliver Button");
            Assert.AreEqual(RepairJobStatus.Delivered, controller.Session.CurrentJob.Status);
            Assert.AreEqual(720, controller.Session.CashLedger.Cash);

            ClickSceneButton("Purchase Upgrade Button");
            Assert.IsTrue(controller.Session.UpgradeState.IsPurchased);
            Assert.AreEqual(600, controller.Session.CashLedger.Cash);

            ClickSceneButton("Accept Next Job Button");
            Assert.AreEqual("Repair Overheated Electric Motor", controller.Session.CurrentJob.Definition.Title);
            Assert.AreEqual(RepairJobStatus.Accepted, controller.Session.CurrentJob.Status);
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

        [UnityTest]
        public IEnumerator RepairMethodButtonsReceivePointerClicksWhenValid()
        {
            AssertMethodButtonPointerClick("Quick Patch Button", "Quick Patch");
            AssertMethodButtonPointerClick("Standard Repair Button", "Standard Repair");
            AssertMethodButtonPointerClick("Reliable Replacement Button", "Reliable Replacement");
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

        private static Canvas FindCanvas(string canvasName)
        {
            foreach (Canvas canvas in Object.FindObjectsByType<Canvas>(FindObjectsSortMode.None))
            {
                if (canvas.name == canvasName)
                {
                    return canvas;
                }
            }

            return null;
        }

        private static GameObject FindGameObject(string objectName)
        {
            foreach (Transform transform in Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (transform.name == objectName)
                {
                    return transform.gameObject;
                }
            }

            return null;
        }

        private static void AssertSectionVisible(string sectionName, bool expectedVisible)
        {
            GameObject section = FindGameObject(sectionName);
            Assert.IsNotNull(section, $"Expected section '{sectionName}' to exist.");
            Assert.AreEqual(expectedVisible, section.activeInHierarchy, $"Unexpected visibility for section '{sectionName}'.");
        }

        private static void AssertObjective(string expectedText)
        {
            Text objective = FindGameObject("Objective Text").GetComponent<Text>();
            Assert.That(objective.text, Does.Contain(expectedText));
        }

        private static void AssertProgress(string expectedStep)
        {
            Text progress = FindGameObject("Loop Progress Text").GetComponent<Text>();
            Assert.That(progress.text, Does.Contain(expectedStep));
        }

        private static void AssertLocationLabel(PrototypeInteractionPoint point, string expectedLabel)
        {
            Assert.IsNotNull(point, $"Expected interaction point '{expectedLabel}' to exist.");
            Transform label = point.transform.Find("Location Label");
            Assert.IsNotNull(label, $"Expected '{expectedLabel}' to have a Location Label child.");
            Assert.AreEqual(expectedLabel, label.GetComponent<TextMesh>().text);
        }

        private static void AssertButtonVisible(string buttonName, bool expectedVisible, bool expectedInteractable)
        {
            Button button = FindButton(buttonName);
            Assert.IsNotNull(button, $"Expected button '{buttonName}' to exist.");
            Assert.AreEqual(expectedVisible, button.gameObject.activeInHierarchy, $"Unexpected visibility for button '{buttonName}'.");
            if (expectedVisible)
            {
                Assert.AreEqual(expectedInteractable, button.interactable, $"Unexpected interactable state for button '{buttonName}'.");
            }
        }

        private static void ClickSceneButton(string buttonName)
        {
            Button button = FindButton(buttonName);
            Assert.IsNotNull(button, $"Expected button '{buttonName}' to exist.");
            Assert.IsTrue(button.gameObject.activeInHierarchy, $"Expected button '{buttonName}' to be visible before clicking.");
            Assert.IsTrue(button.interactable, $"Expected button '{buttonName}' to be interactable before clicking.");
            ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }

        private static void AssertActiveButtonsInsidePanel()
        {
            RectTransform panel = FindGameObject("Repair Job Panel Root").GetComponent<RectTransform>();
            var panelCorners = new Vector3[4];
            panel.GetWorldCorners(panelCorners);
            Rect panelRect = RectFromCorners(panelCorners);

            foreach (Button button in Object.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (!button.gameObject.activeInHierarchy)
                {
                    continue;
                }

                var buttonCorners = new Vector3[4];
                button.GetComponent<RectTransform>().GetWorldCorners(buttonCorners);
                foreach (Vector3 corner in buttonCorners)
                {
                    Assert.IsTrue(panelRect.Contains(corner), $"Button '{button.name}' is outside the Repair Job panel bounds.");
                }
            }
        }

        private static Rect GetWorldRect(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return RectFromCorners(corners);
        }

        private static void AssertScrollContentDoesNotOverlapActionBar()
        {
            Rect contentRect = GetWorldRect(FindGameObject("Scrollable Content Area").GetComponent<RectTransform>());
            Rect actionRect = GetWorldRect(FindGameObject("Fixed Action Bar").GetComponent<RectTransform>());
            Assert.IsFalse(contentRect.Overlaps(actionRect));
        }

        private static Rect RectFromCorners(Vector3[] corners)
        {
            float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            return Rect.MinMaxRect(minX, minY, maxX, maxY);
        }

        private static Button FindButton(string buttonName)
        {
            foreach (Button button in Object.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (button.name == buttonName)
                {
                    return button;
                }
            }

            return null;
        }

        private static void AssertMethodButtonPointerClick(string buttonName, string expectedMethodName)
        {
            var fixture = CreateFixture();
            fixture.Controller.OpenPanel();
            fixture.Controller.AcceptCurrentJob();

            Button button = FindFixtureButton(fixture.Root, buttonName);
            Assert.IsNotNull(button);
            Assert.IsTrue(button.interactable);
            ExecuteEvents.Execute(button.gameObject, new PointerEventData(null), ExecuteEvents.pointerClickHandler);

            Assert.AreEqual(expectedMethodName, fixture.Controller.Session.CurrentJob.SelectedMethod.DisplayName);
            DestroyFixture(fixture);
        }

        private static Button FindFixtureButton(GameObject root, string buttonName)
        {
            foreach (Button button in root.GetComponentsInChildren<Button>(true))
            {
                if (button.name == buttonName)
                {
                    return button;
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
            var acceptButton = CreateButton(root.transform, "Accept Button");
            var closeButton = CreateButton(root.transform, "Close Button");
            var quickButton = CreateButton(root.transform, "Quick Patch Button");
            var standardButton = CreateButton(root.transform, "Standard Repair Button");
            var reliableButton = CreateButton(root.transform, "Reliable Replacement Button");
            var confirmButton = CreateButton(root.transform, "Confirm Repair Button");
            var deliverButton = CreateButton(root.transform, "Deliver Button");
            var upgradeButton = CreateButton(root.transform, "Purchase Upgrade Button");
            var nextButton = CreateButton(root.transform, "Accept Next Job Button");

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
