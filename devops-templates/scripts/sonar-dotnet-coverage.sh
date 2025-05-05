#!/bin/bash

PAT=$1

export NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED=true
export VSS_NUGET_EXTERNAL_FEED_ENDPOINTS='{"endpointCredentials":[{"endpoint":"https://miraeinvest.pkgs.visualstudio.com/d52b99cc-a1aa-4465-8914-748bed96d8d8/_packaging/MiraeDigital/nuget/v3/index.json","username":"artifacts","password":"'${PAT}'"}]}'

wget -O - https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

ProjectName=$2

Solution=$3

SonarUrl="http://sonarqube.miraeinvest.com.br/"

echo 'Setup - dotnet tool install --global dotnet-sonarscanner --version 5.2.2'

dotnet restore --configfile nuget.config --no-cache --force

dotnet tool install --configfile nuget.config --global dotnet-sonarscanner --version 5.2.2


echo '1º Cobertura'


dotnet test ${Solution} --collect:"XPlat Code Coverage" \
   "/p:CollectCoverage=true" \
   "/p:CoverletOutput=./TestResults/" \
   "/p:CoverletOutputFormat=\"opencover\"" \
   "/p:MergeWith=./TestResults/coverage.json"


echo '2º Preparar Análise Sonar'

dotnet sonarscanner begin /k:${ProjectName} /d:sonar.host.url=$SonarUrl /d:sonar.login="e09949f1cda556ca98bc947b927e039808dd74d8" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"

echo '3º Build Solution'

dotnet build --configfile nuget.config ${Solution}

echo '4º Finalizar Análise Sonar'

dotnet sonarscanner end /d:sonar.login="e09949f1cda556ca98bc947b927e039808dd74d8"