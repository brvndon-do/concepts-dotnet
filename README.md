# Concepts

Discord bot that'll post daily concepts related to computer science.

# Getting started

Sensitive information and API keys are stored in user secrets.

```sh
# navigate to each project directory and initiate user secrets
dotnet user-secrets init

# Concept.Server
dotnet user-secrets set "OpenAi:ApiKey" "API KEY HERE"

# Concept.Bot
dotnet user-secrets set "Discord:Token" "TOKEN HERE"
```
