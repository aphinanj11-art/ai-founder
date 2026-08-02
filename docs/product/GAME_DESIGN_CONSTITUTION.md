# AI Founder: From Zero to Autonomous Empire
## Game Design Constitution v0.1

**Status:** Approved Decision Framework  
**Product Owner:** Bee  
**Content Approval:** Approved  
**Governing Product Document:** Game Vision v0.1  
**Document Purpose:** Define the design principles, scope controls, AI boundaries, feature admission rules, evidence standards, and governance requirements used to guide future game design and implementation decisions.

---

# Executive Summary

เอกสารนี้ทำหน้าที่เป็นกรอบตัดสินใจหลักสำหรับการออกแบบและพัฒนา **AI Founder: From Zero to Autonomous Empire**

Constitution ไม่ได้กำหนดรายละเอียดของ Economy, Skill Tree, เครื่องจักร หรือ Technical Architecture แต่ใช้ตอบคำถามสำคัญ เช่น:

- Feature ใดควรเข้าสู่ Scope
- ระบบใดควรถูก Prototype ก่อน
- ความสมจริงควรถูกลดเมื่อใด
- AI ควรมีอำนาจเพียงใด
- Scope ควรขยายเมื่อมีหลักฐานแบบใด
- Codex สามารถดำเนินการเรื่องใดได้
- Portfolio ต้องสื่อสารสถานะอย่างไร

หลักสำคัญคือ:

> **เกมเล็กที่สนุก มีคุณค่ามากกว่าเกมใหญ่ที่ยังไม่มีหัวใจ**

และลำดับการพัฒนาคือ:

> **Fun First → AI Adds Value → Content & Depth → World Expansion**

---

# Approved Constitution Baseline v0.1

| Decision | Approved Baseline |
|---|---|
| Framework Type | Approved Decision Framework |
| Governing Product Document | Game Vision v0.1 |
| Number of Design Articles | 14 |
| Main Categories | Player Experience, System Design, AI Design, Scope & Prototype, Governance & Portfolio |
| Highest Priority | Player Experience and Fun |
| Feature Qualification | Four Design Qualification Gates must all pass |
| Feature Authorization | Separate Product Owner Approval required |
| AI Strategy | Bounded simulated agency over a deterministic core |
| Prototype Strategy | MVP-A before MVP-B |
| Scope Expansion | Requires evidence and Product Owner approval |
| Evidence Principle | Team opinion is a hypothesis, not confirmation |
| Amendment | Reason, evidence, impact assessment, PO approval, Decision Log, version update |
| Repository Governance | No silent scope, engine, architecture, feature, commit, or push decisions |

---

# Governing Document Hierarchy

1. **Game Vision v0.1**
2. **Game Design Constitution v0.1**
3. Approved design documents
4. Approved technical and implementation documents
5. Implementation tasks and repository changes

Constitution ไม่สามารถเปลี่ยน Game Vision โดยปริยายได้

หาก Constitution ขัดกับ Game Vision:

1. หยุดการตัดสินใจที่เกี่ยวข้อง
2. ระบุจุดที่ขัดกัน
3. ตรวจสอบว่าเอกสารใดอาจล้าสมัย
4. จัดทำ Amendment Proposal
5. ประเมินผลกระทบ
6. ขอ Product Owner Approval
7. อัปเดต Decision Log และ Version ที่เกี่ยวข้อง

---

# A. Player Experience Rules

## Article 1 — Fun Before Scale

> **ความสนุกต้องมาก่อนขนาดโลก จำนวนระบบ และความทะเยอทะยานทางเทคนิค**

Article นี้ใช้กำหนด **ลำดับความสำคัญของประสบการณ์ผู้เล่น**

- ห้ามขยายแผนที่เพื่อชดเชย Gameplay ที่ไม่สนุก
- ห้ามเพิ่ม Content จำนวนมากก่อนพิสูจน์ Core Experience
- Prototype ต้องตอบคำถามด้านความสนุก
- กราฟิกและเทคโนโลยีไม่สามารถแทน Gameplay ที่อ่อนแอได้
- Feature ที่ดูน่าตื่นเต้นแต่ไม่เพิ่ม Player Value ต้องถูกเลื่อน

> หากลด Scale ลงแล้วประสบการณ์หลักยังน่าเล่นอยู่หรือไม่

---

## Article 2 — Accessible Depth

> **เกมต้องเริ่มเข้าใจง่าย แต่เปิดความลึกอย่างค่อยเป็นค่อยไป**

- ช่วงต้นใช้ระบบจำนวนน้อย
- Feedback ต้องตรงและเข้าใจง่าย
- ระบบใหม่ปลดล็อกตามบทบาทผู้เล่น
- ความซับซ้อนควรเกิดจากปฏิสัมพันธ์ของระบบ
- ผู้เล่นสายลึกต้องสามารถค้นพบ Layer เพิ่มเติม
- ไม่ต้องมีพื้นฐานวิศวกรรมหรือ MBA เพื่อเริ่มเล่น

> เข้าใจง่าย → ทดลอง → เชื่อมโยงระบบ → เชี่ยวชาญ → ออกแบบระบบของตนเอง

---

## Article 3 — Learn Through Consequences

> **ผู้เล่นเรียนรู้ผ่านการลงมือ ผลลัพธ์ และการสะท้อนกลับของระบบ**

เกมต้องช่วยให้ผู้เล่นเข้าใจว่าเกิดอะไรขึ้น เพราะอะไร ตัวแปรใดเกี่ยวข้อง รอบหน้าปรับอะไรได้ และความเสี่ยงของแต่ละทางเลือกคืออะไร

Preferred methods:

- ทดลอง
- สังเกต
- เปรียบเทียบผล
- แก้ปัญหา
- รับ Feedback จากโลก
- เรียนรู้จากความผิดพลาดที่ฟื้นตัวได้

Prohibited:

- บทเรียนยาวที่หยุด Gameplay
- ข้อสอบที่ไม่เชื่อมกับการเล่น
- การลงโทษโดยไม่อธิบาย
- ศัพท์วิชาการที่ไม่มีบริบท

---

## Article 4 — Failure Must Teach and Allow Recovery

> **ความล้มเหลวต้องมีสาเหตุที่เข้าใจได้ และควรสร้างเรื่องราวมากกว่าหยุดเกมทันที**

Required:

- สัญญาณเตือน
- Causal Feedback
- Recovery Path
- โอกาสเปลี่ยนกลยุทธ์
- ความเสียหายสัมพันธ์กับการตัดสินใจ
- ทรัพยากรหรือความสัมพันธ์เดิมช่วยฟื้นตัวได้

Possible recovery:

- กลับไปรับงานเล็ก
- ขายทรัพย์สิน
- ลดขนาดกิจการ
- ปรับโครงสร้างหนี้
- เจรจากับ Supplier หรือลูกค้า
- ขอความช่วยเหลือจาก NPC
- เปลี่ยนสินค้าและตลาด

> ความล้มเหลวควรตอบว่า “ฉันได้เรียนรู้อะไร” ไม่ใช่เพียง “ฉันเสียอะไร”

---

## Article 5 — Respect the Starting Struggle

> **ความจน การตกงาน และการไม่มีที่พักต้องถูกนำเสนออย่างเคารพ ไม่ใช่เป็นมุกหรือ Gimmick**

Hopeful Struggle requires:

- ผู้เล่นยังมี Agency
- มีทางเลือกมากกว่าหนึ่งทาง
- มีโอกาสสร้างความสัมพันธ์
- มีพื้นที่ให้เรียนรู้
- มีทางฟื้นตัว
- NPC ไม่ลดทอนศักดิ์ศรีโดยไม่มีเหตุผล
- ความสำเร็จมาจากความพยายามและการตัดสินใจ

Prohibited:

- เยาะเย้ยคนจนหรือคนตกงาน
- ทำให้การไม่มีบ้านเป็นมุกซ้ำ
- บังคับให้ทนทุกข์โดยไม่มีทางเลือก
- ใช้ความลำบากเพื่อ Shock Value เท่านั้น

---

# B. System Design Rules

## Article 6 — Core Loop Before Feature Growth

> **Feature ใหม่ต้องสนับสนุน Core Gameplay Loop หรือ Player Transformation อย่างชัดเจน**

### Current Vision-Level Loop Hypothesis

> หาโอกาส → สร้างมูลค่า → ส่งมอบหรือขาย → วิเคราะห์ผล → ลงทุนกลับ → ปรับระบบ → เติบโต

Loop นี้ยังเป็น **สมมติฐานระดับ Vision** และยังไม่ถือเป็น Approved Gameplay Loop จนกว่าจะผ่าน:

1. Core Gameplay Loop Workshop
2. Prototype Design
3. MVP-A Validation
4. Product Owner Approval

Feature ต้องช่วยอย่างน้อยหนึ่งด้าน:

- เพิ่ม Meaningful Decision
- เพิ่มความพึงพอใจจากการสร้าง
- เพิ่มความลึกด้านธุรกิจหรือการผลิต
- สนับสนุน Survivor → Architect
- เพิ่ม Replayability จากระบบ
- ลดงานซ้ำอย่างมีเหตุผล

Feature ที่ไม่สนับสนุนต้องเป็น:

> **Deferred — Future Exploration**

---

## Article 7 — Systems Must Create Meaningful Trade-offs

> **การตัดสินใจสำคัญต้องมีข้อดี ข้อเสีย และผลกระทบต่างช่วงเวลา**

Examples:

- ซื้อเครื่องใหม่ → Capacity เพิ่ม แต่ Cash Flow ตึง
- ลดราคา → Demand เพิ่ม แต่ Margin ลด
- ลด Inspection → เร็วขึ้น แต่ Quality Risk สูง
- ใช้วัตถุดิบถูก → ต้นทุนลด แต่ Defect อาจเพิ่ม
- กู้เงินขยายกิจการ → โตเร็วแต่เปราะบาง
- ใช้ AI มากขึ้น → ลดงานซ้ำแต่เพิ่มความเสี่ยงด้านการควบคุม

Trade-off ต้องขึ้นกับบริบทของกิจการ ตลาด เงินสด Skill Reputation ลูกค้า คู่แข่ง เทคโนโลยี และเหตุการณ์ในโลก

---

## Article 8 — Optimization Must Be Visible

> **การปรับปรุงระบบต้องเห็นผล เข้าใจผล และรู้สึกพึงพอใจ**

Visible feedback:

- งานค้างลด
- Cycle Time สั้นลง
- Flow ลื่นขึ้น
- เครื่องจักรหยุดน้อยลง
- ของเสียลด
- พื้นที่เป็นระเบียบ
- ภาระงานสมดุล
- ลูกค้ารอน้อยลง
- เสียงและภาพของระบบดีขึ้น
- กำไรต่อเวลาเพิ่ม

> ผู้เล่นต้องรู้สึกว่า “ฉันเข้าใจและแก้ระบบได้ดีขึ้น” ไม่ใช่แค่ “ฉันซื้อของแพงขึ้น”

---

## Article 9 — Freedom With Consequences

> **ผู้เล่นมีอิสระเลือกเส้นทาง แต่โลกต้องตอบสนองอย่างมีเหตุผล**

เส้นทางอาจเป็นช่างฝีมือคุณภาพสูง โรงงานต้นทุนต่ำ บริษัทเทคโนโลยี ธุรกิจสีเขียว องค์กรดูแลพนักงาน ธุรกิจหนี้สูงที่โตเร็ว บริษัทที่ใช้ AI หนัก หรือเวิร์กช็อปเล็กที่ไม่ขยาย

ผลสะท้อนต้องมีเหตุผล เช่น:

- ค่าแรงต่ำ → ต้นทุนลด แต่ Turnover สูง
- เน้นคุณภาพ → ต้นทุนสูง แต่ Reputation ดี
- กำไรระยะสั้น → เงินสดดี แต่ความเสี่ยงเพิ่ม
- Automation สูง → Productivity ดี แต่ Dependency เพิ่ม

---

# C. AI Design Rules

## Article 10 — AI Must Add Gameplay Value

> **AI ต้องเปลี่ยนการตัดสินใจ ความสัมพันธ์ หรือสถานการณ์ ไม่ใช่มีไว้เพราะเทคโนโลยีน่าสนใจ**

AI use case ต้องตอบได้:

1. ถ้าเอา AI ออก เกมเสียอะไร
2. AI สร้างสถานการณ์ใหม่อย่างไร
3. Deterministic System เหมาะกว่าหรือไม่
4. ขอบเขตชัดเจนหรือไม่
5. มี Fallback หรือไม่
6. ผู้เล่นยังถือ Meaningful Decision หรือไม่
7. Latency, Reliability และ Testing คุ้มค่าหรือไม่

Suitable roles:

- Requirement Interpretation
- Dialogue
- Bounded Negotiation
- Advice
- Personality
- Memory Summary
- Scenario Variation
- Competitor Intent
- Behavioral Variation

Insufficient uses:

- NPC พูดยาวขึ้นแต่ไม่มีผล
- AI ทำงานที่ UI ปกติทำได้ดีกว่า
- ใส่ AI เพื่อการตลาดเท่านั้น
- ใช้ AI แทนระบบกฎที่ต้องแม่นและทดสอบได้

---

## Article 11 — Bounded AI, Player Decides

> **AI มี simulated agency ภายในระบบที่จำกัด ขณะที่ผู้เล่นยังเป็นเจ้าของการตัดสินใจสำคัญ**

Deterministic core controls:

- เงินและ Ledger
- Inventory
- Production State
- Quality Result
- Time
- Resource Consumption
- Contract State
- Unlock Conditions
- Success/Failure Conditions
- Game Rules

AI may influence:

- คำแนะนำ
- การเจรจา
- Priority
- บุคลิก
- การตีความข้อมูล
- การเสนอแผน
- Memory
- Scenario Variation

AI must not:

- เปลี่ยน Ledger โดยไม่ Validation
- สร้างทรัพยากรจากศูนย์
- เปลี่ยนกฎหลัก
- Override Product Logic
- ตัดสินใจเชิงกลยุทธ์แทนผู้เล่นโดยไม่มีสิทธิ์
- ทำให้ Save State ไม่อธิบายหรือกู้คืนไม่ได้

Player-owned decisions:

- กลยุทธ์ธุรกิจ
- การลงทุนสำคัญ
- การยอมรับความเสี่ยง
- การจัดสรรอำนาจให้ AI
- การจ้างและเลิกจ้าง
- นโยบายคุณภาพ
- จริยธรรมและผลกระทบ
- ทิศทางองค์กร

---

# D. Scope and Prototype Rules

## Article 12 — Prototype Before Expansion

> **ระบบเสี่ยงต้องผ่าน Prototype และ Validation ก่อนเข้าสู่ Production Scope**

Article นี้ควบคุม **ลำดับและกระบวนการพัฒนา** ขณะที่ Article 1 ควบคุม **ลำดับความสำคัญของประสบการณ์ผู้เล่น**

Development sequence:

1. **MVP-A — Core Loop Validation** — ไม่มี Generative AI เป็นส่วนจำเป็น
2. **MVP-B — AI Value Validation** — เพิ่ม AI หนึ่ง Bounded Use Case หลัง MVP-A ผ่าน
3. **Content & Depth** — เพิ่มสิ่งที่สนับสนุน Loop เดิม
4. **World Expansion** — ขยายหลังแกนเกมแข็งแรง

Prototype principles:

- Prototype สามารถทิ้งได้
- Prototype Code ไม่จำเป็นต้องเป็น Production Code
- ต้องมี Validation Question
- ผลลัพธ์เชิงลบถือเป็นความรู้
- การทำ Prototype ไม่ได้อนุมัติ Production Scope โดยอัตโนมัติ

---

## Article 13 — Scope Requires Evidence

> **การเพิ่ม Scope ต้องมีหลักฐาน ไม่ใช่ความตื่นเต้นหรือความคิดเห็นเพียงอย่างเดียว**

### Evidence Quality Hierarchy

1. **Observed Playtest Behavior**
2. **Prototype Result**
3. **Direct Player Feedback**
4. **Technical Measurement**
5. **Research or Reference**
6. **Team Opinion**

Interpretation:

- พฤติกรรมจริงของผู้เล่นมีน้ำหนักมากกว่าคำพูดที่ไม่มีพฤติกรรมรองรับ
- Prototype มีน้ำหนักมากกว่าสมมติฐานบนกระดาษ
- Feedback หนึ่งคนไม่ถือเป็นข้อสรุปแทนผู้เล่นทั้งหมด
- Research ต้องเกี่ยวข้องกับบริบทของเกม
- Technical Measurement ใช้ตอบเรื่อง Feasibility และ Reliability
- Team Opinion มีประโยชน์ในการสร้างสมมติฐาน แต่ไม่ใช่หลักฐานยืนยัน

> **ความคิดเห็นของทีมคือ Hypothesis ไม่ใช่ Confirmation**

ข้อเสนอขยาย Scope ต้องระบุ:

- ปัญหาที่แก้
- Player Value
- ผลต่อ Core Loop
- ต้นทุน
- ความเสี่ยง
- Validation Plan
- Definition of Done

---

## Article 14 — World Density Before World Size

> **พื้นที่เล็กที่สัมพันธ์กับระบบเกมมีคุณค่ากว่าโลกใหญ่ที่ว่างเปล่า**

พื้นที่ใหม่ต้องเพิ่มอย่างน้อยหนึ่งอย่าง:

- Gameplay
- โอกาสธุรกิจ
- ทรัพยากร
- Relationship
- Progression
- ปัญหาระบบ
- Theme หรือ World Meaning

Open-world หมายถึง:

- อิสระในการเลือก
- Non-linear progression
- ระบบตอบสนองกัน
- การสำรวจมีรางวัล
- ธุรกิจหลายแนวทาง

ไม่เท่ากับแผนที่ใหญ่ตั้งแต่ต้น

---

# E. Governance and Portfolio Rules

## Rule 1 — No Silent Design Changes

Scope, Feature, Engine, Architecture, AI Strategy และการตัดสินใจที่ใช้วิจารณญาณ ต้องได้รับ Product Owner Approval

Codex สามารถ Inspect, Analyze, Identify Risk, Propose Options, Recommend และ Implement Approved Work

Codex ห้าม:

- ขยาย Scope เอง
- เลือก Engine เอง
- เพิ่ม Feature เอง
- เปลี่ยน Architecture แบบเงียบ
- Stage, Commit หรือ Push โดยไม่มีสิทธิ์
- เปลี่ยน Product Direction โดยพลการ

---

## Rule 2 — Documentation Is a Product Asset

Decision สำคัญต้องบันทึก Decision, Rationale, Status, Date, Scope Impact, Evidence, Approved By และ Superseded Decision

GitHub ทำหน้าที่เป็น Source of Truth, Decision History, Portfolio Evidence, Engineering Record และ Learning Record

---

## Rule 3 — Portfolio Must Reflect Reality

ห้ามอ้างว่า:

- มีเกมเล่นได้ หากยังไม่มี Prototype
- ใช้ AI ขั้นสูง หากยังเป็น Vision
- เลือก Engine แล้ว หากยัง Deferred
- ระบบผ่าน Validation หากยังไม่ Playtest
- Future Exploration เป็น Approved Scope

> Portfolio ที่ตรงกับความจริงน่าเชื่อถือกว่าการนำเสนอเกินสถานะ

---

## Rule 4 — Constitution Governance Strength

Constitution นี้มีสถานะ:

> **Approved Decision Framework**

ทุกข้อเสนอและงาน Implementation ต้องใช้อ้างอิง

แก้ได้ แต่ห้ามละเลยโดยไม่มีเหตุผลและ Approval

---

## Rule 5 — Amendment Rule

Constitution แก้ได้เมื่อมี:

1. Reason
2. Evidence
3. Impact Assessment
4. Product Owner Approval
5. Decision Log Entry
6. Version Update

การเปลี่ยนที่ไม่ผ่านขั้นตอนนี้ไม่มีผลต่อ Approved Baseline

---

# Constitution Priority Order

1. **Player Experience and Fun**
2. **Core Loop Integrity**
3. **Meaningful Choice and Feedback**
4. **Scope and Feasibility**
5. **System Clarity and Testability**
6. **AI Value**
7. **Realism**
8. **Content Volume**
9. **Visual Ambition**

---

# Feature Admission Test

## Layer 1 — Design Qualification Gates

ต้องผ่านทั้งหมด:

| Gate | Required Question |
|---|---|
| Vision Fit | สนับสนุน Vision, Core Fantasy หรือ Theme หรือไม่ |
| Loop Fit | ทำให้ Core Loop หรือ Player Transformation ดีขึ้นหรือไม่ |
| Player Value | เพิ่มความสนุก การตัดสินใจ หรือคุณค่าชัดเจนหรือไม่ |
| Scope Fit | เหมาะกับ Phase และสามารถสร้าง ทดสอบ และดูแลได้หรือไม่ |

Feature ที่ไม่ผ่านแม้หนึ่งข้อ:

> **ยังไม่ Qualified for Scope**

## Layer 2 — Authorization Gate

| Gate | Required Question |
|---|---|
| Product Owner Approval | Product Owner อนุมัติให้เข้าสู่ Scope หรือไม่ |

Feature ต้องมีสถานะ:

> **Qualified by Design + Authorized by Product Owner**

PO Approval ไม่สามารถชดเชย Design Gate ที่ไม่ผ่านโดยไม่ระบุข้อยกเว้น เหตุผล และผลกระทบ

Design Qualification ที่ผ่านก็ไม่ทำให้ Feature เข้าสู่ Scope โดยอัตโนมัติ หากยังไม่ได้รับ PO Approval

## Supporting Review

| Review | Question |
|---|---|
| System Fit | เชื่อมกับระบบเดิมหรือสร้างความซ้ำซ้อน |
| AI Necessity | จำเป็นต้องใช้ AI หรือไม่ |
| Evidence | มีหลักฐานระดับใด |
| Ownership | ใครรับผิดชอบ |
| Risk | ความเสี่ยงด้าน Scope, UX, Reliability และ Content |
| Exit Criteria | เมื่อใดถือว่าผ่าน ล้มเหลว หรือควรถอด |

### Possible Outcomes

- **Approved for Scope**
- **Approved for Prototype Only**
- **Under Review**
- **Deferred — Future Exploration**
- **Rejected**
- **Superseded**

---

# Decision Escalation Rule

เมื่อทีมเห็นต่าง:

1. ตรวจ Game Vision
2. ตรวจ Governing Document Hierarchy
3. ตรวจ Priority Order
4. ใช้ Feature Admission Test
5. ระบุ Trade-off
6. หา Evidence หรือสร้าง Prototype
7. ให้ Product Owner ตัดสิน
8. บันทึก Decision Log

ห้าม Implement ก่อนแล้วขออนุมัติย้อนหลัง

---

# Non-Goals of This Constitution

เอกสารนี้ไม่กำหนด:

- Core Gameplay Loop ฉบับ Approved
- Economy Formula
- Skill Tree
- Machine List
- Content List
- Final Art Style
- Engine
- Programming Language
- AI Architecture เชิงเทคนิค
- Database
- Save System
- Production Roadmap
- Release Plan

---

# Approval Record

| Version | Status | Reviewer | Date | Notes |
|---|---|---|---|---|
| v0.1-draft | Superseded | Bee | 2026-08-02 | Initial constitution draft |
| v0.1-draft-r1 | Superseded | Bee | 2026-08-02 | Six approved revisions applied |
| v0.1-draft-r2 | Superseded | Bee | 2026-08-02 | Final wording and governance separation applied |
| v0.1 | Approved | Bee | 2026-08-02 | Approved Decision Framework for the next design phase |

---

# Definition of Done — Content

Game Design Constitution v0.1 ผ่าน Content Definition of Done เมื่อ:

1. Articles ทั้ง 14 ข้อไม่ซ้ำซ้อนอย่างมีนัยสำคัญ
2. Player Experience, System Design, AI Design และ Scope Rules แยกหมวดชัดเจน
3. Design Rules แยกจาก Governance and Portfolio Rules
4. Game Vision v0.1 มีลำดับสูงกว่า Constitution
5. Priority Order มีความชัดเจนและใช้ตัดสินข้อขัดแย้งได้
6. Core Loop ถูกระบุเป็น Vision-Level Hypothesis ไม่ใช่ Approved Gameplay Loop
7. Design Qualification แยกจาก Product Owner Authorization
8. Evidence Quality Hierarchy ใช้ประกอบการตัดสินใจได้จริง
9. AI Boundaries สอดคล้องกับ Game Vision v0.1
10. MVP-A และ MVP-B ไม่ถูกผสมเป็น Validation เดียวกัน
11. Amendment Rule ระบุ Reason, Evidence, Impact, Approval และ Version Control ครบ
12. Non-Goals ป้องกันไม่ให้ Constitution กลายเป็น GDD หรือ Technical Specification
13. Product Owner ตรวจทานและอนุมัติเนื้อหา

> **Content Approval Gate: Passed**

การผ่าน Content Approval ยังไม่อนุญาตให้แก้ Repository, Commit หรือ Push โดยอัตโนมัติ

---

# Post-Approval Repository Gates

## Repository Gate 1 — Write Authorization

Product Owner อนุมัติตำแหน่งไฟล์ Constitution การแก้ README หากจำเป็น การอัปเดต Decision Log และรายชื่อไฟล์ที่ Codex มีสิทธิ์แก้

## Repository Gate 2 — Repository Write

Codex เขียนเฉพาะไฟล์ที่อนุมัติ รักษา UTF-8 ใช้ LF และไม่ Stage, Commit หรือ Push

## Repository Gate 3 — Diff and Integrity Review

ตรวจ Repository tree, Git status, File diff, Canonical content comparison, UTF-8, BOM, line endings และ Decision Log

## Repository Gate 4 — Commit Approval

Product Owner อนุมัติรายชื่อไฟล์ Staged diff และ Commit message

## Repository Gate 5 — Push Approval

หลังตรวจ Local Commit แล้ว Product Owner จึงอนุมัติ Push และตรวจ Local HEAD กับ `origin/main`

---

# Revision History Summary

## Revision 1

1. เปลี่ยน Baseline naming
2. แยกบทบาท Article 1 และ Article 12
3. ระบุ Core Loop เป็น Vision-Level Hypothesis
4. แยก Design Qualification จาก PO Authorization
5. เพิ่ม Evidence Quality Hierarchy
6. เพิ่ม Game Vision-over-Constitution conflict rule

## Revision 2

1. ปรับ Approval Record ให้ใช้สถานะ Superseded แบบมาตรฐาน
2. เปลี่ยน Feature Outcome จาก Approved เป็น Approved for Scope
3. แยก Content Definition of Done ออกจาก Post-Approval Repository Gates

ไม่มีการเพิ่ม Feature, Engine, Architecture, Economy Formula หรือ Implementation Scope

---

## Current Status

> **Game Design Constitution v0.1 Content Approved by Product Owner Bee**
