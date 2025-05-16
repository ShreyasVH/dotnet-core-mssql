if ! lsof -i :$PORT > /dev/null; then
    echo "Starting"
    dotnet publish/dotnet.dll > server.log 2>&1 &
fi