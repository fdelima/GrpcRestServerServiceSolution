start powershell.exe -Command "docker compose up webapirest"
TIMEOUT /T 5
start powershell.exe -Command "docker compose -f ..\GrpcRestClientServiceSolution\docker-compose.yml up restserviceclient"