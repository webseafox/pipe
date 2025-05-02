FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PAT

WORKDIR /home/app

COPY ./*.sln ./
COPY ./src/*/*.csproj ./
COPY ./tests/*/*.csproj ./
COPY ./nuget.config ./

RUN for file in $(ls *Tests.csproj); do mkdir -p ./tests/${file%.*}/ && mv $file ./tests/${file%.*}/; done
RUN for file in $(ls *.csproj); do mkdir -p ./src/${file%.*}/ && mv $file ./src/${file%.*}/; done

ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS '{"endpointCredentials":[{"endpoint":"https://miraeinvest.pkgs.visualstudio.com/d52b99cc-a1aa-4465-8914-748bed96d8d8/_packaging/MiraeDigital/nuget/v3/index.json","username":"artifacts","password":"'${PAT}'"}]}'

# Get and install the Artifact Credential provider
RUN wget -O - https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash


RUN dotnet restore --configfile nuget.config --no-cache --force

COPY ./ .

RUN dotnet build -c Release --no-restore && \
	dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0

RUN apt-get update  \
    && apt-get install -y wget libfontconfig1 libxrender1 libgdiplus \
    && wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox_0.12.6-1.buster_amd64.deb \
    && dpkg -i ./wkhtmltox_0.12.6-1.buster_amd64.deb \
    ; apt-get -f install -y \
    && rm wkhtmltox_0.12.6-1.buster_amd64.deb \
    && rm -rf /var/cache/apt/lists/* \
	; apt-get install -y fonts-roboto
RUN apt-get update && \
    apt-get install -yq tzdata && \
    ln -fs /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata

WORKDIR /app

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "MiraeDigital.BffMobile.API.dll"]