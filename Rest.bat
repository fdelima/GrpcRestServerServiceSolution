start powershell.exe -Command "docker compose up restserviceserver"
TIMEOUT /T 5
start powershell.exe -Command "docker compose -f ..\GrpcRestClientServiceSolution\docker-compose.yml up restserviceclient"