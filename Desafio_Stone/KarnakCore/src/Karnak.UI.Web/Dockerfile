FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Karnak.UI.Web/Karnak.UI.Web.csproj", "Karnak.UI.Web/"]
RUN dotnet restore "Karnak.UI.Web/Karnak.UI.Web.csproj"
COPY . .
WORKDIR "/src/Karnak.UI.Web"
RUN dotnet build "Karnak.UI.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Karnak.UI.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Karnak.UI.Web.dll"]