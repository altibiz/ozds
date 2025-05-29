# OZDS Documentation Guidelines

This document outlines how to write and maintain documentation for the OZDS
project. We maintain three types of documentation, each serving different
audiences and purposes.

## Documentation Types

### 1. Dev Documentation

**Audience:** Developers working on OZDS  
**Purpose:** Explain project structure and development processes  
**Location:** `/docs/dev/` using mdbook and markdown files

**Required Files:**

- `getting-started.md` - How to get the system working and start making changes
- `architecture.md` - High-level overview of system components and environments
- `release-pipeline.md` - How code moves from development to production
- `documentation.md` - How to write all documentation types (this document)
- `testing.md` - How to write tests
- `style-guide.md` - Source code conventions and coding standards

**Standards:**

- Keep highly succinct - reduce boilerplate, focus on understanding
- Each file can follow its own format optimized for its content
- Target: tight and understandable for all developers
- Setup instructions should be detailed - if unclear, the developer who finds it
  unclear should improve it

### 2. Code Documentation

**Audience:** Developers using interfaces and services  
**Purpose:** Enable developers to use each other's code effectively  
**Location:** Inline C# documentation comments  
**Generation:** CodeFX pipeline generates webpage from C# docs

**Interface Documentation Requirements:**

- **Responsibility:** Single responsibility principle - what does this interface
  do?
- **Implementation Guidelines:** Specific patterns, conventions, or restrictions
  - Example: "Use this convention to implement" or "Don't use DB calls"
  - Include directly in interface docs for LSP integration
- **Member Documentation:** For each method/property:
  - **Responsibility:** What does this member do?
  - **Cost:** What kind of DB/service calls it makes and how many
  - **Known Exceptions:** What can go wrong?
  - **Argument Assumptions:** Constraints not covered by type system
  - **Return Value Assumptions:** Guarantees not covered by type system
- **Usage Examples:** Simple code snippets showing inputs/outputs
  - Use generic examples (MyModel instead of NetworkUserModel) to resist changes
  - Base on actual codebase usage but anonymize to prevent coupling

**Special Documentation for Critical Sections:**

- **Logic-Critical Areas:** Time handling, electrical measures, financial
  calculations
  - Document business rules and regulatory requirements
  - Include strict compliance requirements
- **Performance-Critical Areas:** Measurement ingest
  - Document performance expectations and constraints

**Experimental/Temporary Code:**

- Add C# docs with clear banner: "EXPERIMENTAL" or "TEMPORARY"
- Create GitHub issue explaining the permanent solution needed
- Ensure LSP shows the experimental/temporary status on hover

### 3. User Documentation

**Audience:** End users of the OZDS system  
**Purpose:** Guide users through frontend functionality  
**Location:** `/docs/user/` using mdbook and markdown files  
**Languages:** English and Croatian (en/hr sections)

**User Roles Covered:**

- Operators
- Locations
- Network Users
- Shared functionality

**Content Standards:**

- **Text and Screenshots:** Primary documentation method
- **Translation Process:** Write in English first, translate to Croatian using
  LLM, then refine
- **Screenshot Standards:**
  - Include address bar for full screenshots
  - Avoid other windows in frame
  - Take screenshots for both languages
  - Every page should have a screenshot
  - Complex components (charts) get additional screenshots
  - Components with specific data representations need screenshots with
    explanations

**Content Organization:**

- Cross-role features go in shared section
- Role-specific sections reference shared content when needed
- Document what UI components represent, not underlying business logic
- Tooltips should match documentation semantically (but don't need to be
  identical)

## Documentation Maintenance

**Responsibility:** The developer making code changes must update corresponding
documentation

**Review Process:** Documented in `release-pipeline.md`

**Updates Required:**

- Source code changes → Update code docs, tests, changelog, and user docs if UI
  changed
- All documentation should be kept current with code changes

**Living Document Philosophy:**

- Documentation should evolve based on developer feedback
- If something is unclear, the person who found it unclear should improve it
- Add examples when developers request them for clarity
- Trust developers and reviewers to follow guidelines appropriately

## Technical Implementation

**Dev Documentation:** mdbook + markdown files  
**Code Documentation:** C# XML docs + CodeFX generation  
**User Documentation:** mdbook + markdown files with en/hr localization

**Integration:** Code documentation designed for LSP integration to provide
immediate help during development.
