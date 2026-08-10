# Submission write-up

## 1. The gaps you filled in

The brief left three things unspecified: required vs. optional fields, whether repeat links are reused or reissued and the shape of the shareable ID. Walk us through the calls you made on each and why.

_Your answer:_
* **Required vs. Optional:** I made the `Name` field strictly required (with a 50-character limit), as a profile without a primary identifier lacks practical value. `Headline` (limited to 50 characters) and `Skills` are optional. Furthermore, I proactively implemented a robust validation and sanitization layer. The API uses Regex to ensure names contain only valid letters and spaces and utilizes `WebUtility.HtmlEncode` to sanitize all string inputs against potential XSS injections.
* **Repeat Links:** If a user attempts to generate a link with the exact same payload (identical Name, Headline and Skills), the API actively blocks the creation and returns a `409 Conflict` error. I chose this approach to prevent database bloat, avoid spam and maintain strict data integrity, rather than blindly issuing new IDs for identical profiles or performing complex lookups to reuse an old link.
* **Shareable ID Shape:** Instead of using a lengthy, non-user-friendly UUID (e.g., `550e84...`), I implemented a 6-character alphanumeric identifier (a substring of a GUID, e.g., `62afa2`). This structure is significantly more aesthetically pleasing, easier to share and mimics the format used by modern URL shorteners.

## 2. What's next

If you had one more day on this, what's the first thing you'd do and why that thing first?

_Your answer:_
Since I have already taken the initiative to expand the core requirements by adding full CRUD operations (PUT and DELETE endpoints), structured JSON error handling, duplicate prevention and input sanitization, the immediate next step would be writing Unit Tests (using xUnit) and Integration Tests. Establishing a testing safety net is critical before deploying any API to production to ensure the validation logic and file I/O operations behave exactly as expected under various edge cases.

## 3. AI tools

Did you use AI tools while building this (Copilot, ChatGPT, Claude, etc.)? If yes — what did you use them for and was there anything an AI suggested that you changed or rejected and why? If no, that's fine — just say so.

_Your answer:_
Yes, I used Gemini. I utilized it primarily as a brainstorming partner to evaluate whether to use Python or C# for this specific architecture. I also used it to quickly scaffold the baseline boilerplate for the C# Minimal API, the structured JSON error responses and the initial local file Read/Write operations, allowing me to focus on the architectural decisions and business logic.

## 4. Optional — skip freely, no penalty either way

If Humanas had to onboard 10,000 new profiles overnight and only had capacity to manually review 1% of them, how would you decide which 1%? There's no single right answer here — we're curious how you think, not what you conclude.

_Your answer:_
I would prioritize the 1% (100 profiles) by programming the system to flag anomalies and high-value signals. First, I would automatically filter out profiles with empty optional fields or default/generic headlines. Next, I would prioritize profiles that either have an unusually high number of listed skills (to investigate potential spam or keyword stuffing) or contain highly specific, rare niche skills that are currently in high demand in the market. This ensures the manual review effort is strategically spent on the most interesting and impactful edge cases.