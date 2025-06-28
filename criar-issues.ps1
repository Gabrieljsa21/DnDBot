# Script: criar-issues.ps1
# Requer: GitHub CLI (gh) autenticado

$repo = "Gabrieljsa21/DnDBot"
$todoPath = ".\TODO.md"

if (-not (Test-Path $todoPath)) {
    Write-Host "Arquivo TODO.md não encontrado."
    exit
}

# Lê todas as linhas do TODO.md
$lines = Get-Content $todoPath

foreach ($line in $lines) {
    if ($line -match "^- \[ \] (.+)$") {
        $title = $matches[1].Trim()
        Write-Host "Criando issue: $title"
        gh issue create --repo $repo --title "$title" --body "Tarefa vinda do roadmap em TODO.md" --label "enhancement"
        Start-Sleep -Milliseconds 500
    }
}
