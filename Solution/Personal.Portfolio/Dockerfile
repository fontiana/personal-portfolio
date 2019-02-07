FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 30874
EXPOSE 44345

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Personal.Portfolio/Personal.Portfolio.csproj Personal.Portfolio/
RUN dotnet restore Personal.Portfolio/Personal.Portfolio.csproj
COPY . .
WORKDIR /src/Personal.Portfolio
RUN dotnet build Personal.Portfolio.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Personal.Portfolio.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Personal.Portfolio.dll"]
