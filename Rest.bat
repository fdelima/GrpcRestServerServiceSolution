cd src
cd DockerCompose
start powershell.exe -Command "docker compose up restserviceserver"
cd ..
cd ..
TIMEOUT /T 5
start powershell.exe -Command "docker compose -f ..\GrpcRestClientServiceSolution\docker-compose.yml up restserviceclient"