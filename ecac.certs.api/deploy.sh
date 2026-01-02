#!/bin/bash
set -e 

CONTAINER_NAME=ecac-api
IMAGE=ecac.api:latest

dotnet publish -c Release -o release

docker context use home-lab
docker build --build-arg ARTIFACTS_DIR=release -t $IMAGE .

if docker ps -q -f name=^/${CONTAINER_NAME}$ > /dev/null; then
  echo "Stopping existing container..."
  docker stop $CONTAINER_NAME
fi

if docker ps -aq -f name=^/${CONTAINER_NAME}$ > /dev/null; then
  echo "Removing existing container..."
  docker rm $CONTAINER_NAME
fi

docker run -d \
  --name $CONTAINER_NAME \
  --restart unless-stopped \
  -p 4000:8080 \
  $IMAGE
  
  echo "deploy complete"
