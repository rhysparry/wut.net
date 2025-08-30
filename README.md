Wut.Net — test suite for .NET behaviors and third‑party libraries

[![.NET CI Build](https://github.com/rhysparry/wut.net/actions/workflows/dotnet-ci-build.yml/badge.svg?branch=main)](https://github.com/rhysparry/wut.net/actions/workflows/dotnet-ci-build.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

This repository contains a collection of tests that validate behavior across .NET and third-party libraries.

Quick start

- Restore packages and build:
  ```
  dotnet restore
  dotnet build
  ```

- Run tests:
  ```
  dotnet test
  ```

- Or (if you have just installed) run the project's test task used by CI:
  ```
  just test
  ```

- Status: CI workflow has been updated to use .NET 9 and the test suite builds and runs successfully locally.

License

This project is licensed under the MIT License — see the LICENSE file for details.
