# Mostrar status atual
git status

# Adiciona todas as mudanças
git add .

# Pede a mensagem do commit
Write-Host "Digite a mensagem do commit:"
$mensagem = Read-Host

# Tenta fazer o commit só se houver algo para commitar
$diff = git diff --cached --quiet
if ($LASTEXITCODE -eq 0) {
    Write-Host "Nada para commitar."
} else {
    git commit -m "$mensagem"
}

# Descobre a branch atual
$branch = git rev-parse --abbrev-ref HEAD

# Dá o push para a branch atual
try {
    git push origin $branch
    Write-Host "Atualização enviada para branch $branch"
} catch {
    Write-Host "Erro ao enviar o push. Verifique sua conexão e configurações do Git."
}
