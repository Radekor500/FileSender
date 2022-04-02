FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5033

ENV ASPNETCORE_URLS=http://+:5033

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
COPY ["FileSender/", "FileSender/Api/"]
COPY ["FileSenderClient/", "FileSender/Client/"]
# COPY ["FileSenderClient/package-lock.json", "FileSender/Client/"]
RUN dotnet restore "FileSender/Api/FileSender.csproj"
RUN npm install FileSender/Client/
RUN npm install -g @angular/cli


WORKDIR "/src/FileSender/Api"
RUN dotnet build "FileSender.csproj" -c Release -o /app/build

WORKDIR "/src/FileSender/Client/src"
RUN ls
RUN ng serve --configuration production

FROM build AS publish
RUN dotnet publish "FileSender.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileSender.dll"]
