Contributing to Wut.Net

Thanks for your interest in contributing! This repository contains focused tests that validate .NET and third-party library behavior. The goal is to keep contributions small, well-tested, and self-contained.

Getting started

- Fork the repo and create a branch for your change (feature/bugfix):
  git checkout -b feature/my-change

- Restore and build locally:
  dotnet restore
  dotnet build

- Run tests locally (preferred):
  dotnet test
  or, if you use just:
  just test

What to contribute

- New tests that demonstrate behavior differences or confirm bug fixes.
- Small, focused changes to test code or project metadata that improve clarity or reproducibility.
- Documentation improvements (README, examples, CI notes).

Guidelines

- Keep PRs small and focused on a single logical change.
- Include a brief description and rationale in the PR body; link to any relevant issues.
- Tests must pass locally and in CI.
- Tests should contain a summary comment explaining their purpose.
- Follow existing code style and keep tests deterministic.
- Do not commit large binary files or build artifacts.

Issues and pull requests

- Open an issue first for discussions about larger changes if you're unsure.
- Name branches clearly: feature/..., fix/..., docs/...
- Rebase or merge the latest main before opening a PR to avoid unnecessary merge conflicts.

License and Code of Conduct

- By contributing, you agree that your contributions will be licensed under the project's MIT license.
- Please be respectful and follow the project's Code of Conduct (if provided).

Maintainers

- Maintainers will review PRs and provide feedback. Expect at least one reviewer to request changes for non-trivial updates.
