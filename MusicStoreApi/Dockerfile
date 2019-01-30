FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MusicStoreApi/MusicStoreApi.csproj", "MusicStoreApi/"]
RUN dotnet restore "MusicStoreApi/MusicStoreApi.csproj"
COPY . .
WORKDIR "/src/MusicStoreApi"
RUN dotnet build "MusicStoreApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MusicStoreApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MusicStoreApi.dll"]