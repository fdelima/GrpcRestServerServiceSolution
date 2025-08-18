cd src
cd DockerCompose
start powershell.exe -Command "docker compose up grpcserviceserver"
cd ..
cd ..
TIMEOUT /T 5
start powershell.exe -Command "docker compose -f ..\GrpcRestClientServiceSolution\docker-compose.yml up grpcserviceclient"