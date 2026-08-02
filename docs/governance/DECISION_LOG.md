# Decision Log

## Project

**Project:** AI Founder: From Zero to Autonomous Empire  
**Product Owner:** Bee  
**Current Phase:** Pre-production — Documentation-first repository setup  
**Last Updated:** 2026-08-02

## Status Legend

- **Proposed:** Suggested but not yet reviewed or approved.
- **Under Review:** Currently being evaluated by the Product Owner.
- **Approved:** Accepted as the current product direction or repository decision.
- **Deferred:** Intentionally postponed for a future decision point.
- **Rejected:** Considered and declined.
- **Superseded:** Replaced by a newer approved decision.

## Decisions

| Decision ID | Date | Status | Decision | Rationale | Scope Impact | Approved by |
|---|---|---|---|---|---|---|
| DEC-001 | 2026-08-02 | Approved | Game concept selected: AI Founder: From Zero to Autonomous Empire | Establishes the central project identity and portfolio direction. | Defines the approved game concept for vision documentation. | Bee |
| DEC-002 | 2026-08-02 | Approved | Core genre: Factory + Business Simulation | Keeps the game anchored in production systems and business growth. | Future design work should treat factory/business simulation as the core. | Bee |
| DEC-003 | 2026-08-02 | Approved | Supporting genres: Founder Survival, Life Simulation, AI Sandbox, Strategy | Captures the supporting experience without replacing the core genre. | Supporting genres may inform design but must not expand scope without approval. | Bee |
| DEC-004 | 2026-08-02 | Approved | World: Near-Future Industrial City | Makes the world relatable while allowing credible AI and automation themes. | Establishes the approved setting direction. | Bee |
| DEC-005 | 2026-08-02 | Approved | Tone: Hopeful Struggle | Balances hardship, growth, pressure, and optimism. | Guides writing, systems pressure, and player-facing experience. | Bee |
| DEC-006 | 2026-08-02 | Approved | Player transformation: Survivor -> Maker -> Operator -> Founder -> Architect | Defines the intended long-term player identity arc. | Progression design should support this transformation. | Bee |
| DEC-007 | 2026-08-02 | Approved | Five Game Design Pillars: Start Small, Build Meaningfully; Systems Create Stories; Learn by Doing; AI Changes the Game; Freedom With Consequences | Provides stable design principles for future decisions. | Features and systems should be evaluated against these pillars. | Bee |
| DEC-008 | 2026-08-02 | Approved | Supporting principle: Optimization Feels Satisfying | Ensures improvement and systems tuning feel visible and rewarding. | Guides feedback, UI, simulation, and production-flow design. | Bee |
| DEC-009 | 2026-08-02 | Approved | Development philosophy: Fun First -> AI Adds Value -> Content & Depth -> World Expansion | Prevents premature AI or world-scope expansion before core fun is proven. | Establishes the order of validation for future work. | Bee |
| DEC-010 | 2026-08-02 | Approved | MVP structure: MVP-A validates the core loop without Generative AI; MVP-B validates one bounded AI value-validation use case | Separates deterministic fun validation from AI validation. | MVP-A and MVP-B must remain distinct unless Product Owner approves a change. | Bee |
| DEC-011 | 2026-08-02 | Approved | Open-world means long-term freedom, exploration, and non-linear progression; not a requirement for the first prototype's map size | Prevents open-world language from forcing an oversized prototype. | First prototype can stay within one small industrial district. | Bee |
| DEC-012 | 2026-08-02 | Approved | AI principle: Simulated agency within bounded and testable game systems | Keeps AI useful while limiting instability and uncontrolled scope. | AI design must stay bounded, testable, and player-directed. | Bee |
| DEC-013 | 2026-08-02 | Approved | Game Vision Document v0.1 approved by Product Owner Bee | Locks the approved vision baseline for the next design phase. | Future design should reference docs/product/GAME_VISION.md. | Bee |
| DEC-014 | 2026-08-02 | Approved | Repository structure approved | Enables a minimal documentation-first repository setup. | Only README.md, .gitignore, LICENSE, docs/product/GAME_VISION.md, and docs/governance/DECISION_LOG.md are approved for initial setup. | Bee |
| DEC-015 | 2026-08-02 | Approved | License direction: Proprietary / All Rights Reserved | Protects game concepts, documents, and future IP. | Repository is not open-source. | Bee |
| DEC-016 | 2026-08-02 | Deferred | Engine selection: Not selected | Engine choice requires later technical and product evaluation. | No engine files or engine-specific rules are authorized. | Bee |
| DEC-017 | 2026-08-02 | Deferred | Gameplay implementation: Not authorized | Current phase is documentation-first pre-production. | No source code or gameplay prototype is authorized in this task. | Bee |
| DEC-018 | 2026-08-02 | Deferred | Commit and push: Not authorized in this task | Repository write approval does not include staging, commit, remote setup, or push. | Files remain uncommitted until Product Owner approval. | Bee |
| DEC-019 | 2026-08-02 | Approved | Game Design Constitution v0.1 approved | Establishes the approved decision framework for design, scope control, AI boundaries, evidence standards, and governance. | Future design and implementation work must reference docs/product/GAME_DESIGN_CONSTITUTION.md when evaluating scope and design decisions. | Bee |
| DEC-020 | 2026-08-02 | Approved | Constitution governance strength: Approved Decision Framework | Keeps the Constitution strong enough to guide decisions while still allowing amendment through evidence and Product Owner approval. | Design proposals should be evaluated against the Constitution before entering scope. | Bee |
| DEC-021 | 2026-08-02 | Approved | Governing document hierarchy: Game Vision v0.1 is above Game Design Constitution v0.1 | Prevents the Constitution from silently changing the approved product vision. | Any conflict with Game Vision v0.1 requires escalation, impact assessment, Product Owner approval, and Decision Log updates. | Bee |
| DEC-022 | 2026-08-02 | Approved | Four Design Qualification Gates: Vision Fit, Loop Fit, Player Value, Scope Fit | Creates a practical filter for feature and system proposals before scope authorization. | Features that fail any design qualification gate are not qualified for scope unless an exception is explicitly approved and documented. | Bee |
| DEC-023 | 2026-08-02 | Approved | Separate Product Owner Authorization Gate | Separates design qualification from formal scope approval. | Passing design gates does not authorize implementation without Product Owner approval. | Bee |
| DEC-024 | 2026-08-02 | Approved | Constitution Priority Order approved | Provides a tie-breaker when design principles conflict. | Player experience, core loop integrity, meaningful choice, feasibility, and testability take priority over AI, realism, content volume, and visual ambition. | Bee |
| DEC-025 | 2026-08-02 | Approved | Evidence Quality Hierarchy approved | Clarifies how playtest behavior, prototype results, feedback, measurements, research, and team opinion should be weighed. | Scope expansion should be supported by the strongest practical evidence available for the decision. | Bee |
| DEC-026 | 2026-08-02 | Approved | Core Loop remains a Vision-Level Hypothesis | Avoids treating the current loop statement as an approved gameplay contract before workshop and validation. | Core Gameplay Loop must still be designed, prototyped, validated in MVP-A, and approved separately. | Bee |
| DEC-027 | 2026-08-02 | Approved | AI boundary: bounded simulated agency over deterministic core | Preserves AI value while keeping important game state controlled, explainable, testable, and recoverable. | AI may influence bounded interactions but must not directly override ledger, game rules, or critical state without validation. | Bee |
| DEC-028 | 2026-08-02 | Approved | MVP-A before MVP-B | Keeps deterministic core fun validation separate from AI value validation. | Generative AI remains outside MVP-A and may enter only as one bounded use case in MVP-B after MVP-A is proven fun. | Bee |
| DEC-029 | 2026-08-02 | Approved | Amendment Rule approved | Allows the Constitution to evolve without silent design drift. | Constitution changes require reason, evidence, impact assessment, Product Owner approval, Decision Log entry, and version update. | Bee |
| DEC-030 | 2026-08-02 | Approved | Repository write authorized only for this task | Limits repository changes to adding the approved Constitution document, updating README documentation link, and adding approved Decision Log entries. | No other file or scope change is authorized by this repository write. | Bee |
| DEC-031 | 2026-08-02 | Deferred | Commit and push remain not authorized | Keeps repository write approval separate from commit and push approval. | Changes must remain unstaged and uncommitted until Product Owner approves the next gate. | Bee |