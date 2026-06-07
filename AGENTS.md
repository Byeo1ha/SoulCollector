## Codex Sandbox Note

In this workspace, normal Windows sandbox execution may fail with:

```text
windows sandbox: spawn setup refresh
```

If that occurs, avoid repeated sandbox retries. For read-only or low-risk project commands, retry with escalated execution and a short justification. Keep reports concise, and only explain the sandbox issue when relevant.

Never escalate destructive commands or broad system changes without explicit user intent.
