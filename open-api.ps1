docker compose up -d --build

Write-Host "Waiting for API..."

do {
    Start-Sleep -Seconds 1

    try {
        $response = Invoke-WebRequest `
            -Uri "http://localhost:8080/swagger/index.html" `
            -UseBasicParsing `
            -TimeoutSec 2

        $ready = $response.StatusCode -eq 200
    }
    catch {
        $ready = $false
    }
} while (-not $ready)

Write-Host "API is ready. Opening Swagger..."

Start-Process "http://localhost:8080/swagger"