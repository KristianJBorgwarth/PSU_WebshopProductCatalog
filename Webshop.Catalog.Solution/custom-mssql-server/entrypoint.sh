#!/bin/bash

# Ensure proper permissions on the data directory
mkdir -p /var/opt/mssql/data
chmod -R 777 /var/opt/mssql/data

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Get the PID of the SQL Server process
SQL_PID=$!

# Wait for SQL Server to start up
echo "Waiting for SQL Server to start..."
sleep 15s

# Check if SQL Server is ready to accept connections
echo "Checking if SQL Server is ready..."
RETRIES=30
for i in $(seq 1 $RETRIES); do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "SELECT 1" &> /dev/null
    if [ $? -eq 0 ]; then
        echo "SQL Server is ready!"
        break
    fi
    echo "SQL Server is not ready yet. Waiting..."
    sleep 1
done

if [ $i -eq $RETRIES ]; then
    echo "SQL Server did not start in time. Exiting."
    exit 1
fi

# Run the setup script
echo "Running the setup script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /usr/src/app/setup.sql

# Wait for SQL Server process to exit
wait $SQL_PID
