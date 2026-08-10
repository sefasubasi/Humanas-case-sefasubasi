# Take-Home Exercise: Shareable Profile Link

Build a small application — a CLI, a script, or a minimal API, your
choice, in whatever language you're most comfortable with — that can:

1. Create a profile with at least: a **name**, a **headline**, and a
   **list of skills** — and generate a **shareable link or ID** for it.
2. Given that link/ID, **retrieve and display** the profile in a
   human-readable format (console output, HTML, JSON — your call),
   including a sensible response when the ID **doesn't exist**.

That's the whole core ask, deliberately small — it shouldn't take long.
Data can live in memory or a local file (e.g. JSON). You do not need a
real database.

## Getting started

Please don't fork this repo — forks are public and visible to everyone,
including other candidates. Your work should stay private to you and us.

The quickest way:

1. Click **Use this template → Create a new repository**, and set the
   visibility to **Private**.
2. Add the GitHub users listed in your invitation email as
   collaborators, so we can review your work.

Or, if you prefer to do it by hand:

1. Clone this repo locally.
2. Create a new, empty **private** repository under your own GitHub
   account.
3. Push this code to that new repository.
4. Add the GitHub users listed in your invitation email as
   collaborators.

Either way, include the link to your repository in your submission (see
Deliverables below).

## Going further (optional)

The core above is intentionally minimal. If you finish it and see
something worth doing — better validation, persistence, tests, edge
cases we didn't call out, a nicer output format, whatever you think adds
real value — feel free to keep going. There's no expectation that you
do, and no penalty if you stop at the core. We're just as interested in
what you chose to add, if anything, and why.

## What we're intentionally not specifying

A few things are left open on purpose:

- What counts as a **required vs. optional** field on a profile, and what
  should happen when optional data is missing.
- Whether generating a link **twice for the same profile** should reuse
  the existing link or issue a new one each time.
- What the **shareable ID itself** should look like — format, length, how
  guessable it should or shouldn't be.

We want to see how you fill in gaps like this, not whether you can guess
what we had in mind. Make a reasonable call and explain your reasoning in
the write-up — we're evaluating the decision-making as much as the code.

## Explicitly out of scope

Please don't spend time on: authentication/login, a real database, a
frontend framework, deployment, or a full test suite. A couple of tests
are welcome if you want them, but they're not required or expected.

## AI tools

Use whatever you'd normally use — Copilot, ChatGPT, Claude, or nothing at
all. We're not scoring *whether* you used AI. We're scoring whether you
understood and can defend what you shipped, so be ready to explain any
part of it and why you didn't take the alternatives.

## Deliverables

1. Your code, pushed to your own private GitHub repo with us added as
   collaborators (see "Getting started" above) — include the link to it
   in your submission.
2. A filled-in copy of [`docs/SUBMISSION_TEMPLATE.md`](docs/SUBMISSION_TEMPLATE.md).
3. A one-line note on how to run it, if it isn't obvious.
