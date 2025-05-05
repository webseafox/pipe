#!/bin/bash

IMAGE=$1
TAG=$2
ENV=$3
CLUSTER=$4
REGION=$5


NAMESPACE=${ENV,,}-$IMAGE
PREFIX="/${ENV,,}/$IMAGE/"
echo "PREFIX=${PREFIX}"

ACCOUNT=$(aws sts get-caller-identity --query Account --output text)

IMAGENAME=$ACCOUNT.dkr.ecr.$REGION.amazonaws.com/$IMAGE

aws eks update-kubeconfig --name $CLUSTER --region $REGION

kubectl create namespace $NAMESPACE || true
kubectl label namespace $NAMESPACE istio-injection=enabled || true

sed -i "s@##ChartName##@${IMAGE}@g" ./Image/helm/Chart.yaml
sed -i "s@##ChartVersion##@${TAG}@g" ./Image/helm/Chart.yaml

cat ./Image/helm/Chart.yaml

helm upgrade --atomic $IMAGE ./Image/helm \
-f ./Image/helm/values.yaml \
-f ./Image/helm/$ENV/values.yaml \
--install \
--namespace=$NAMESPACE \
--set image.repository="$IMAGENAME" \
--set image.tag="$TAG" \
--set ingress.url.prefix="$PREFIX" \
--wait \
--debug \
--timeout 2m \
--reset-values \
--cleanup-on-fail 