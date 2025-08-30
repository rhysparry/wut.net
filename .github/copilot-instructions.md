# Wut.Net - .NET Behavior Test Suite

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Prerequisites and Installation
Before building the code, install the required tools and SDKs:

1. **Install .NET 9 SDK**:
   ```bash
   curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 9.0.203
   ```

2. **Install .NET 9 Runtime** (if tests fail with runtime not found):
   ```bash
   curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --runtime dotnet --version 9.0.4
   ```

3. **Install just task runner**:
   ```bash
   wget -qO- https://github.com/casey/just/releases/download/1.42.0/just-1.42.0-x86_64-unknown-linux-musl.tar.gz | tar xvz -C /tmp && sudo mv /tmp/just /usr/local/bin/
   ```

4. **Install pre-commit** (for linting):
   ```bash
   pip install pre-commit
   ```

5. **Set environment variables**:
   ```bash
   export DOTNET_ROOT="$HOME/.dotnet"
   export PATH="$HOME/.dotnet:$PATH"
   ```

### Build and Test Commands

**Primary workflow using just (RECOMMENDED)**:
- `just test` -- Full build and test pipeline. Takes ~6 seconds. NEVER CANCEL. Set timeout to 30+ seconds.

**Individual dotnet commands**:
- `dotnet restore` -- Takes ~8 seconds first time, ~1 second cached. NEVER CANCEL. Set timeout to 60+ seconds.
- `dotnet build --no-restore` -- Takes ~4.5 seconds. NEVER CANCEL. Set timeout to 30+ seconds.
- `dotnet test --no-build --verbosity detailed` -- Takes ~3 seconds. NEVER CANCEL. Set timeout to 30+ seconds.

**Linting and formatting**:
- `just lint` -- Takes ~33 seconds first time (environment setup), ~1 second cached. NEVER CANCEL. Set timeout to 120+ seconds.

## Validation

### Testing Strategy
- Always run the complete test suite when making changes using `just test`.
- The repository contains 5 tests total: 1 in Wut.Net.Tests, 4 in Wut.Net.Newtonsoft.Json.Tests.
- All tests are focused unit tests that validate specific .NET and library behaviors.
- Tests use xUnit v3 framework with descriptive method names and summary comments.

### Manual Validation Scenarios
After making changes, validate that:
1. **Build succeeds**: `just test` completes successfully
2. **All tests pass**: Test summary shows "failed: 0, succeeded: 5"
3. **Code formatting**: `just lint` passes without changes
4. **No new test failures**: Only your intentional test changes should affect test results

### CI/CD Requirements
Always run these commands before committing changes to ensure CI passes:
- `just test` -- Ensures build and tests work
- `just lint` -- Ensures code formatting and pre-commit hooks pass

## Project Structure

### Solution Overview
- **Wut.Net.sln**: Main solution file containing two test projects
- **global.json**: Specifies .NET 9.0.203 SDK requirement
- **justfile**: Task runner configuration (similar to Makefile)

### Test Projects
1. **Wut.Net.Tests**: Core .NET behavior validation tests
   - Target framework: net9.0
   - Tests basic .NET language features and behaviors
   - Example: Collection null-coalescing precedence

2. **Wut.Net.Newtonsoft.Json.Tests**: Newtonsoft.Json library behavior tests
   - Target framework: net9.0
   - Includes Newtonsoft.Json 13.0.3 package reference
   - Tests JSON serialization behaviors, immutable types, required properties

### Configuration Files
- **dotnet-tools.json**: Defines csharpier 1.0.2 for code formatting
- **.pre-commit-config.yaml**: Pre-commit hooks for code quality
- **.github/workflows/**: CI/CD pipelines (dotnet-ci-build.yml, pre-commit.yml)

## Common Tasks

### Adding New Tests
When adding new test cases:
1. Place .NET core behavior tests in `Wut.Net.Tests/`
2. Place third-party library tests in appropriately named projects (e.g., `Wut.Net.Newtonsoft.Json.Tests/`)
3. Use descriptive test method names starting with "When" and "Then"
4. Include XML summary comments explaining the test purpose
5. Follow existing code style and patterns

### Key File Locations
The following are outputs from frequently run commands:

**Repository root contents**:
```
.git/
.github/
.gitignore
.idea/
.pre-commit-config.yaml
CONTRIBUTING.md
LICENSE
README.md
Wut.Net.Newtonsoft.Json.Tests/
Wut.Net.Tests/
Wut.Net.sln
dotnet-tools.json
global.json
justfile
```

**Package references used**:
- xunit.v3 3.0.1
- xunit.runner.visualstudio 3.1.4
- Microsoft.NET.Test.Sdk 17.14.1
- coverlet.collector 6.0.4
- Newtonsoft.Json 13.0.3 (Newtonsoft.Json.Tests project only)

## Important Notes

### Timing and Cancellation Warnings
- **NEVER CANCEL build or test commands** - Builds may take up to 8 seconds, tests may take up to 6 seconds total
- **NEVER CANCEL lint commands** - Initial pre-commit setup takes up to 33 seconds, subsequent runs take ~1 second
- Always set timeouts of 60+ seconds for restore commands and 30+ seconds for build/test commands
- First-time pre-commit setup requires 120+ second timeout

### Environment Dependencies
- The repository REQUIRES .NET 9 - it will not build with .NET 8 or earlier
- Tests require both .NET 9 SDK and runtime to be properly installed
- Use the specific dotnet installation path (`$HOME/.dotnet/dotnet`) if system dotnet conflicts
- Set `DOTNET_ROOT` environment variable to avoid runtime location issues

### Code Quality
- C# code formatting is handled automatically by csharpier during pre-commit
- All changes must pass pre-commit hooks to merge
- Follow existing test patterns: descriptive names, XML summaries, focused assertions
- Keep tests small, focused, and deterministic