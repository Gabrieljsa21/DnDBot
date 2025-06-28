# Mostrar status atual
git status

# Adiciona todas as mudan�as
git add .

# Pede a mensagem do commit
Write-Host "Digite a mensagem do commit:"
$mensagem = Read-Host

# Tenta fazer o commit s� se houver algo para commitar
$diff = git diff --cached --quiet
if ($LASTEXITCODE -eq 0) {
    Write-Host "Nada para commitar."
} else {
    git commit -m "$mensagem"
}

# Descobre a branch atual
$branch = git rev-parse --abbrev-ref HEAD

# D� o push para a branch atual
try {
    git push origin $branch
    Write-Host "Atualiza��o enviada para branch $branch"
} catch {
    Write-Host "Erro ao enviar o push. Verifique sua conex�o e configura��es do Git."
}
