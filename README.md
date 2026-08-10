# Humanas - Profile Sharing API

A lightweight, robust and secure RESTful Minimal API built with **C# .NET 8**, designed to generate short, shareable profile links.

## 🚀 Features & Enhancements

Beyond the core requirements, this project implements several enterprise-level features to ensure data integrity, security and scalability:

* **Full CRUD Operations:** Supports creating (POST), reading (GET), updating (PUT) and deleting (DELETE) profiles.
* **Short, Shareable IDs:** Generates user-friendly 6-character alphanumeric identifiers (e.g., `62afa2`) instead of cumbersome UUIDs.
* **Strict Data Validation:** 
  * `Name` is strictly required and limited to 50 characters.
  * Regex pattern applied to ensure names contain only valid letters and spaces.
  * `Headline` is limited to 100 characters.
* **XSS Protection:** Implemented `WebUtility.HtmlEncode` to sanitize all string inputs, preventing malicious script injections.
* **Smart Data Cleaning:** The `Skills` array automatically removes empty strings, sanitizes inputs and drops duplicate entries.
* **Duplicate Prevention (409 Conflict):** Actively blocks the creation of exact duplicate profiles to prevent database bloat and spam.
* **Structured Error Handling:** Returns standardized JSON error responses with clear HTTP status codes (400, 404, 409).
* **Local Persistence:** Data is securely read from and written to a local `profiles.json` file to maintain state across restarts without the overhead of a full database.

## 📂 Project Structure

Following industry standards, the source code is cleanly separated from documentation:

```text
/
├── docs/                   # Submission write-ups and templates
├── src/
│   └── ProfileLinkApi/     # The main C# .NET Minimal API project
├── CANDIDATE_BRIEF.md
└── README.md