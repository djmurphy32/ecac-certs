#!/bin/bash

dotnet publish -c Release -o release

docker context use home-lab
docker build --build-arg ARTIFACTS_DIR=release -t ecac.api:latest .

docker run -d -p 4000:8080 ecac.api:latest
