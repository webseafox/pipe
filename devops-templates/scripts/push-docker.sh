#!/bin/bash

IMAGE=$1
TAG=$2
REGION=$3
IMAGENAME=$IMAGE:$TAG


echo "[$IMAGENAME]"

ACCOUNT=$(aws sts get-caller-identity --query Account --output text)
echo "[AWS Account: $ACCOUNT]"


aws ecr create-repository --repository-name $IMAGE --image-scanning-configuration scanOnPush=true || true

echo "[send to AWS Elastic Container Registry]"
aws ecr get-login-password --region $REGION | docker login --username AWS --password-stdin $ACCOUNT.dkr.ecr.$REGION.amazonaws.com
docker tag $IMAGENAME $ACCOUNT.dkr.ecr.$REGION.amazonaws.com/$IMAGENAME
docker push $ACCOUNT.dkr.ecr.$REGION.amazonaws.com/$IMAGENAME