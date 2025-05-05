#!/bin/bash

DOCKERFILE=$1

IMAGE=$2
TAG=$3
PAT=$4
IMAGENAME=$IMAGE:$TAG

echo "[start docker build] - [$IMAGENAME]"
echo "[Dockerfile] - [$DOCKERFILE]"
echo "[pwd] -" $(pwd)
echo "[ls] -" $(ls)
docker build --build-arg PAT=$PAT -f $DOCKERFILE -t $IMAGENAME .