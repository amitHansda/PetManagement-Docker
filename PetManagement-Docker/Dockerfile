FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
RUN mkdir /source
WORKDIR /source


COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build

FROM build as publish
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
RUN mkdir /app
WORKDIR /app
COPY --from=publish /app .
## Install dockerize in the image
RUN apt-get update && apt-get install -y wget
ENV DOCKERIZE_VERSION v0.6.1
RUN wget https://github.com/jwilder/dockerize/releases/download/$DOCKERIZE_VERSION/dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && rm dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz
