FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Karnak.Services.Api/Karnak.Services.Api.csproj", "Karnak.Services.Api/"]
RUN dotnet restore "Karnak.Services.Api/Karnak.Services.Api.csproj"
COPY . .
WORKDIR "/src/Karnak.Services.Api"
RUN dotnet build "Karnak.Services.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Karnak.Services.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Karnak.Services.Api.dll"]