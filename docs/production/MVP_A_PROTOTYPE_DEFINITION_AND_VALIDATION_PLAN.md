# AI Founder: From Zero to Autonomous Empire
## MVP-A Prototype Definition & Validation Plan v0.1

**Status:** Approved Prototype Definition and Validation Plan  
**Product Owner:** Bee  
**Content Approval:** Approved  
**Governing Documents:** Game Vision v0.1, Game Design Constitution v0.1, Core Gameplay Loop v0.1  
**Purpose:** Define the smallest playable MVP-A prototype, its hard scope boundary, required player experience, validation evidence, and decision criteria before engine selection, technical architecture, or implementation.

---

# Executive Summary

MVP-A exists to answer one question:

> **Is the deterministic core gameplay loop understandable, engaging, and strong enough to make players voluntarily accept one more job?**

MVP-A is not a vertical slice of the full game and is not a compressed version of the complete product vision. It is a focused validation prototype for the transition:

> **Survivor → Early Maker**

The first playable experience must support the loop:

> **Accept Job → Prepare Resources → Perform Meaningful Work → Inspect and Deliver → Review Outcome → Reinvest → Accept Another Job**

The prototype will use a very small graybox area:

> **Workshop + Nearby Shop + Delivery Point**

It will contain three minimal job archetypes:

1. Simple Repair  
2. Small Production  
3. Quality Inspection  

The prototype must remain deterministic and must not include Generative AI, employees, factory automation, a large open world, or city-scale simulation.

---

# 1. Prototype Objective

## Primary Objective

Validate whether the approved Core Gameplay Loop creates:

- Clear goals
- Meaningful decisions
- Understandable cause and effect
- Visible progression
- Motivation to continue playing

## Primary Validation Question

> **After completing one job, does the player voluntarily want to accept another job?**

This is the **One-More-Job Signal**.

## Secondary Validation Questions

- Does the player understand job requirements without lengthy explanation?
- Does choosing between jobs create meaningful trade-offs?
- Does the work step contain decisions rather than passive waiting?
- Does optional quality checking feel strategic rather than mandatory?
- Can the player explain why a job produced profit, loss, success, or failure?
- Do upgrades visibly change the next job?
- Do Repair, Production, and Inspection feel meaningfully different?

---

# 2. Prototype Player Experience Target

The player should feel:

> **I selected an opportunity, solved a problem with limited resources, understood the outcome, and improved my ability to handle the next job.**

The intended emotional sequence is:

> Uncertainty → Understanding → Decision → Execution → Outcome → Learning → Improvement → Curiosity

The prototype does not need cinematic storytelling, polished art, or broad exploration. It must provide a complete playable loop from the first job to the next-job decision.

---

# 3. First Playable Scenario

The player begins with:

- A small old workshop
- Limited cash
- Limited daily energy
- Basic tools
- Small storage capacity
- No employees
- No automated systems
- No Generative AI
- Access to a nearby shop
- Access to a delivery point
- A small set of available jobs

The player can:

1. Review available jobs  
2. Accept or reject a job  
3. Check requirements and resources  
4. Acquire missing materials  
5. Perform the work  
6. Choose whether and how deeply to inspect  
7. Deliver the result  
8. Review money, cost, quality, time, and reputation  
9. Purchase or defer an upgrade  
10. Accept another job or end the day  

---

# 4. Prototype Area Boundary

## First Playable Area

The first prototype includes only:

- **Workshop**
- **Nearby Shop**
- **Delivery Point**
- A minimal number of NPCs or interaction points

These locations may be:

- Adjacent graybox spaces
- Connected by short paths
- Connected through simple scene transitions
- Represented with placeholder geometry

## Area Principle

> **The prototype area must be only large enough to validate the loop.**

The prototype must avoid spending production effort on:

- Roads
- Decorative city spaces
- Long travel
- Vehicles
- Open-world streaming
- Complex navigation
- Environmental storytelling that does not support the loop

## MVP-A Expansion Boundary

Only after the first loop works may the area expand toward:

> **One Small Industrial District**

That expansion requires gameplay evidence and Product Owner approval.

---

# 5. Required Job Archetypes

## 5.1 Simple Repair Job

### Value Model

> Skill + Time + Tools + Parts → Restored Function or Extended Asset Life → Value

### Minimum Decisions

- Diagnose the problem
- Repair or replace
- Choose part quality
- Choose fast repair or reliable repair
- Inspect after repair or deliver immediately

### Prototype Purpose

Validate whether the player enjoys making a practical diagnosis under limited resources.

---

## 5.2 Small Production Job

### Value Model

> Materials + Process + Time → Product → Value

### Minimum Decisions

- Choose material
- Choose process order
- Choose quantity
- Decide whether to allow scrap buffer
- Choose sampling, full inspection, or no inspection

### Prototype Purpose

Validate resource planning, production trade-offs, and the seed of future factory gameplay.

---

## 5.3 Quality Inspection Job

### Formal Definition

> **Quality Inspection is a service that creates value from information, accuracy, requirement interpretation, confidence, traceability, and risk reduction.**

The player sells:

- Information
- Confidence
- Traceability
- Risk reduction
- Decision support

### Minimum Decisions

- Choose inspection method
- Choose measurement points
- Decide whether to repeat
- Interpret a simplified requirement
- Ask for more information or proceed
- Report Pass, Fail, or Insufficient Evidence

### Prototype Purpose

Validate a signature mechanic that differentiates AI Founder from conventional factory and tycoon games.

Quality Inspection must not be reduced to a decorative mini-game or a generic percentage bar.

---

# 6. Shared Deterministic Job Framework

All three job archetypes should conceptually share this design framework:

```text
Job
→ Requirements
→ Resources
→ Player Decisions
→ Process
→ Quality / Risk
→ Outcome
→ Money + Reputation
→ Upgrade
```

This is a prototype design direction, not an approved software architecture.

The prototype should reuse shared concepts where practical:

- Job requirements
- Time
- Energy
- Materials or parts
- Tool capability
- Quality risk
- Delivery state
- Payment
- Reputation
- Outcome explanation

---

# 7. Meaningful Work Interaction

The work phase must not be:

> Press Button → Wait → Receive Result

Each job must contain at least one meaningful choice from this set:

- Tool selection
- Process order
- Material or part selection
- Speed versus quality
- Precision level
- Diagnosis
- Sampling
- Repeat measurement
- Rework
- Stop or continue
- Ask for more information
- Accept or reduce uncertainty

A choice qualifies as meaningful only when:

- The player has enough information to reason about it
- It changes time, cost, quality, reliability, risk, or outcome
- The result can be understood afterward
- It does not rely only on random guessing
- It does not require repetitive clicking to simulate labor

Exact mini-games and controls are not decided in this document.

---

# 8. Quality Mechanic

Quality is:

> **Optional and Risk-Based**

The player may choose:

- Skip inspection
- Basic inspection
- Detailed inspection
- Rework
- Repeat
- Delay delivery

Inspection consumes time, energy, and possibly money, but reduces uncertainty.

## Reasons to Inspect

- High-value job
- Tight requirement
- Unstable process
- Poor tool condition
- Important customer
- High rework cost
- Weak reputation
- Limited historical confidence

## Reasons to Skip or Reduce Inspection

- Simple job
- Stable process
- Tight deadline
- High inspection cost
- Customer accepts risk
- Strong historical confidence

No inspection policy should be optimal in every situation.

---

# 9. Minimal Economy

MVP-A requires only:

- Cash
- Revenue
- Material or part cost
- Tool or maintenance cost
- Inspection cost
- Daily living cost
- Profit or loss
- Reputation

The player must be able to understand:

> **Where did the money go, and why did this job make or lose money?**

The prototype does not require:

- City-wide inflation
- Dynamic macroeconomics
- Banking systems
- Taxes
- Complex loans
- Stock markets
- Corporate finance
- Multiple currencies

Exact formulas are not approved by this document.

---

# 10. Survival Pressure

MVP-A uses:

> **Cash + Energy + Daily Living Cost**

Included:

- Limited daily energy
- Daily food cost
- Daily housing cost
- Consequences for poor cash management

Excluded:

- Detailed hunger meter
- Thirst
- Disease
- Weather survival
- Detailed sleep simulation
- Complex safety system
- Detailed housing simulation

Survival must create priority and trade-offs without replacing the Business and Work Loop.

---

# 11. Minimal Upgrade Set

The prototype should contain approximately 4–5 upgrades, such as:

- Better General Tool
- Precision Tool
- Workbench Upgrade
- Storage Upgrade
- Energy Capacity Upgrade

Each upgrade must visibly affect at least one of:

- Available choices
- Work speed
- Quality confidence
- Reliability
- Storage flexibility
- Energy capacity
- Job eligibility

An upgrade that changes only an invisible percentage without affecting play is insufficient.

Exact upgrade names and balance values remain undecided.

---

# 12. Required UI

## HUD

- Cash
- Energy
- Day
- Reputation

## Job Panel

- Requirement
- Reward
- Deadline
- Required resources
- Risk or uncertainty
- Accept / Reject

## Inventory

- Materials
- Parts
- Tools
- Storage capacity

## Work Panel

- Available method
- Tool choice
- Process choice
- Time / energy effect
- Risk effect

## Inspection Panel

- Skip
- Basic
- Detailed
- Repeat
- Rework
- Confidence or evidence state

## Outcome Screen

- Revenue
- Cost
- Profit / loss
- Delivery result
- Quality result
- Reputation change
- Explanation of cause

## Upgrade Screen

- Upgrade cost
- Gameplay effect
- Requirements

## End-of-Day Summary

- Jobs completed
- Income
- Cost
- Living cost
- Net cash change
- Reputation change
- Pending jobs

The prototype does not require a complete menu framework or final UI art.

---

# 13. Graybox Art Strategy

The prototype should use:

- Primitive geometry
- Placeholder character
- Placeholder icons
- Basic animation
- Text-first UI
- Simple interaction markers
- Minimal sound feedback

The objective is:

> **Fun before Art**

Art polish must not delay validation of the job loop.

The prototype may look rough, but interactions, information, and outcomes must remain readable.

---

# 14. Hard Scope Boundary

The first prototype must not include:

- Generative AI
- Ollama integration
- AI NPCs
- AI Advisor
- Employees
- Factory automation system
- Conveyors
- Robots
- Multiple factories
- Vehicles
- Large open world
- City simulation
- Dynamic city economy
- Full skill tree
- Large story campaign
- Multiplayer
- Combat
- Voice interaction
- Cloud services
- Blockchain
- Play-to-Earn
- Production-grade save architecture
- Production-grade AI architecture

Any addition requires:

- Design Qualification
- Evidence
- Scope impact review
- Product Owner approval
- Decision Log entry

---

# 15. Playtest Stages

## Stage 1 — Internal Functional Test

Participants:

- Bee
- ChatGPT design review
- Codex engineering review

Purpose:

- Complete the loop
- Detect bugs
- Detect logic failures
- Detect missing feedback
- Confirm evidence collection works

This is not sufficient to validate player fun.

## Stage 2 — Guided Playtest

Working target:

> **3–5 external players**

The facilitator should explain as little as possible and observe behavior.

Purpose:

- Identify comprehension problems
- Identify pacing problems
- Identify work-step boredom
- Identify unclear cause and effect
- Detect mandatory-feeling quality checks

## Stage 3 — Blind-ish Playtest

Working target:

> **5–10 external players**

Players should receive minimal instruction.

Purpose:

- Test onboarding clarity
- Test whether players understand requirements
- Test voluntary continuation
- Test whether the loop survives without designer explanation

Participant counts are working targets, not permanent requirements.

---

# 16. Evidence Collection

## Behavior

- Number of jobs accepted
- Number of jobs completed
- Whether another job is voluntarily accepted
- Job archetypes selected
- Quality check choices
- Upgrade choices
- Session stop point
- Restart behavior
- Experimentation with a different method

## Performance

- Time per job
- Success / partial success / failure
- Profit / loss
- Rework count
- Resource waste
- Deadline performance
- Energy usage

## Understanding

- Can the player identify the requirement?
- Can the player explain the outcome?
- Can the player explain profit or loss?
- Can the player explain inspection consequences?
- Can the player explain an upgrade effect?

## Feedback

- What was enjoyable?
- What was boring?
- What was confusing?
- What did the player want to do next?
- Which decision felt most meaningful?
- Which action felt unnecessary?

Observed behavior has more evidentiary weight than a simple answer to “Was it fun?”

---

# 17. Primary and Secondary Validation Signals

## Primary Signal — One-More-Job Signal

> **After completing a job, the player voluntarily accepts or actively seeks another job.**

This is the primary engagement signal for MVP-A.

It must not be interpreted alone. A player may continue because of confusion, facilitator pressure, or curiosity unrelated to the loop.

## Secondary Signals

### Comprehension

The player understands the first job without a long explanation.

### Agency

The player feels the outcome came from their choices.

### Causality

The player understands why money, quality, time, and reputation changed.

### Progression

The player notices that an upgrade changes the next job.

### Variety

Repair, Production, and Inspection feel different.

### Learning

The player begins reasoning about:

- Cost
- Quality
- Risk
- Time
- Efficiency

without receiving a lecture.

---

# 18. Early Playtest Metrics

These are initial validation targets, not release KPIs or permanent balance requirements.

## Comprehension Targets

- Player identifies the first job objective
- Player identifies required resources
- Player identifies at least one meaningful requirement
- Player completes the first job without lengthy facilitator instruction

## Pace Targets

- First job targets approximately 2–5 minutes
- Travel and preparation do not dominate the session
- No long passive waiting period
- Outcome feedback is immediate enough to preserve momentum

## Causality Targets

- Player can explain why the job made or lost money
- Player can explain what inspection changed
- Player can explain at least one upgrade effect

## Engagement Targets

- Player voluntarily accepts at least one additional job
- Player experiments with a different method or job
- Player expresses interest in an upgrade or new opportunity

These targets may be revised after initial observation.

---

# 19. Validation Decision Framework

After evidence review, MVP-A receives one of four outcomes.

## PASS — Validate

Evidence supports the loop strongly enough to recognize it as a:

> **Validated Core Loop**

This does not automatically authorize MVP-B implementation. MVP-B requires a separate Product Owner decision.

## REVISE

The loop appears promising, but one or more steps are confusing, slow, repetitive, or weak.

Action:

- Identify the weak step
- Revise the smallest possible area
- Retest

## PIVOT

One or more job archetypes, interactions, or assumptions fail to create value.

Action:

- Replace or redesign the failing part
- Preserve validated parts
- Update documents and Decision Log
- Retest

## STOP

The central loop fails to produce engagement after reasonable revisions.

Action:

- Stop expansion
- Revisit the loop hypothesis
- Consider a larger design change
- Require Product Owner approval before continuing

Negative results are evidence, not project failure.

---

# 20. Definition of Done — First Playable Prototype

The prototype is not complete merely because it launches.

It must support:

```text
Start
→ Accept Job
→ Prepare
→ Perform Work
→ Inspect or Skip
→ Deliver
→ Review Outcome
→ Upgrade or Save
→ Accept Another Job
```

Required:

- Three minimal job archetypes
- Minimal deterministic economy
- Cash, energy, and daily living pressure
- Optional risk-based quality
- Small upgrade set
- Outcome explanation
- First playable area
- Basic evidence collection
- At least one complete internal test
- At least one external playtest round
- Evidence review
- Product Owner validation decision

---

# 21. Prototype Instrumentation Requirement

The prototype must support simple evidence capture.

Minimum acceptable approaches:

- Structured event log
- Session summary
- CSV or JSON export
- Manual observer sheet combined with in-game summary

Minimum events:

- Session start / end
- Job viewed
- Job accepted / rejected
- Resource acquired
- Method selected
- Quality check selected
- Job delivered
- Outcome
- Upgrade purchased
- Next job accepted
- Day ended

Instrumentation must not require cloud services.

The exact implementation is a later technical decision.

---

# 22. Key Risks and Responses

| Risk | Description | Response |
|---|---|---|
| Prototype Becomes Production | Team over-engineers temporary systems | Explicitly allow disposable prototype code |
| Map Scope Creep | Time shifts toward building a district | Start with Workshop + Shop + Delivery Point |
| Passive Work | Player presses a button and waits | Require at least one meaningful decision per job |
| Mandatory Quality Tax | Inspection becomes repetitive | Keep it optional and risk-based |
| Job Similarity | Three jobs feel like reskins | Give each a distinct value model and decision pattern |
| Survival Distraction | Energy/living costs dominate | Keep survival strategic and minimal |
| Weak Causality | Player cannot explain outcomes | Use explicit outcome breakdown |
| Upgrade Flatness | Upgrades feel like invisible percentages | Require visible gameplay impact |
| Designer Bias | Team mistakes enthusiasm for evidence | Prioritize observed behavior |
| AI Temptation | AI is added before core fun is proven | Hard-exclude Generative AI from MVP-A |
| Evidence Overload | Instrumentation slows prototype | Capture only decisions needed for validation |
| False Pass | One-More-Job signal is misread | Combine with comprehension, agency, and causality |

---

# 23. Explicit Non-Decisions

This document does not select:

- Game engine
- Programming language
- ECS or OOP architecture
- Save architecture
- Database
- Exact formulas
- Exact numerical balance
- Exact mini-games
- Final controls
- Final art style
- Art pipeline
- Audio pipeline
- Ollama integration
- AI architecture
- Production roadmap
- Release platform

These require separate decisions after this plan is approved.

---

# 24. Approval and Validation Status

## Product Owner Approval

Approval of this document means:

- The prototype definition is authorized as a planning baseline
- The validation plan is approved
- Engine and technical research may use these requirements
- Implementation is not yet authorized
- The loop is still not validated

## Future Validation

The loop becomes a **Validated Core Loop** only after:

- Prototype implementation
- Playtest evidence
- Evidence review
- Product Owner validation decision

---

# Approval Record

| Version | Status | Reviewer | Date | Notes |
|---|---|---|---|---|
| v0.1-draft | Superseded | Bee | 2026-08-02 | Initial consolidated prototype definition |
| v0.1 | Approved | Bee | 2026-08-02 | Approved MVP-A Prototype Definition & Validation Plan |

---

# Current Status

> **MVP-A Prototype Definition & Validation Plan v0.1 — Approved**

Implementation remains unauthorized until Engine Selection and Prototype Technical Strategy receive Product Owner approval.
