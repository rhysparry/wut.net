restore:
    dotnet restore

build: restore
    dotnet build --no-restore

test: build
    dotnet test --no-build --verbosity detailed

[private]
restore-tools:
    dotnet tool restore

lint: restore-tools
    pre-commit run --all-files
